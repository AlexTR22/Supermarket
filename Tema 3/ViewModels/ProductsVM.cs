using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Tema_3.Commands;
using Tema_3.Model.BusinessLogicLayer;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;
using Tema_3.Views;

namespace Tema_3.ViewModels
{
    public class ProductsVM:BasePropertyChange
    {
        ProductsBLL productsBLL = new ProductsBLL();

        public ObservableCollection<Products> ProductList
        {
            get => productsBLL.Products;
            set => productsBLL.Products = value;
        }

        public ProductsVM() 
        {
            ProductList= productsBLL.GetAllProducts();
            Product = new Products();
        }

        private Products _product;
        public Products Product
        {
            get => _product;
            set 
            {
                _product = value;
                NotifyPropertyChanged(nameof(Product));
            }
        }





        private ICommand _addProduct;
        public ICommand AddProduct
        {
            get
            {
                if (_addProduct == null)
                {
                    _addProduct = new RelayCommand(AddProductInDB);
                }
                return _addProduct;
            }
        }

        private ICommand _modifyProduct;
        public ICommand ModifyProduct
        {
            get
            {
                if (_modifyProduct == null)
                {
                    _modifyProduct = new RelayCommand(ModifyProductInDB);
                }
                return _modifyProduct;
            }
        }

        private ICommand _deleteProduct;
        public ICommand DeleteProduct
        {
            get
            {
                if (_deleteProduct == null)
                {
                    _deleteProduct = new RelayCommand(DeleteProductInDB);
                }
                return _deleteProduct;
            }
        }

        private ICommand _goBack;
        public ICommand GoBack
        {
            get
            {
                if (_goBack == null)
                {
                    _goBack = new RelayCommand(GoBackCommand);
                }
                return _goBack;
            }
        }



        public void GoBackCommand()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "ProductsWindow")
                    {
                        AdministratorPage administratorPage = new AdministratorPage();
                        administratorPage.Show();
                        window.Close();
                        break;
                    }
                }
            }
        }

        public void AddProductInDB()
        {
            if (Product.NameProduct!=null && Product.Barcode!=null)
            {
                CategoriesBLL categoriesBLL = new CategoriesBLL();
                if (categoriesBLL.VerifyCategoryExistanceInDB(Product.CategoryProduct) > 0)
                {
                    ProducersBLL producersBLL = new ProducersBLL();
                    if (producersBLL.VerifyProducerExistanceInDB(Product.ProducerProduct) > 0)
                    {
                        if (productsBLL.VerifyProductExistanceInDB(Product.NameProduct, Product.Barcode) == 0)
                        {
                            productsBLL.AddProductInDB(Product);
                        }
                        else
                        {
                            MessageBox.Show("Product already existent");
                            return;
                        }
                    }
                    else
                    {
                         MessageBox.Show("Category non existent");
                    return;
                    }
                }
                else
                {
                    MessageBox.Show("Category non existent");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }

        private void ModifyProductInDB()
        {
            if (Product.NameProduct != null && Product.Barcode != null)
            {
                CategoriesBLL categoriesBLL = new CategoriesBLL();
                if (categoriesBLL.VerifyCategoryExistanceInDB(Product.CategoryProduct) > 0)
                {
                    ProducersBLL producersBLL = new ProducersBLL();
                    if (producersBLL.VerifyProducerExistanceInDB(Product.ProducerProduct) > 0)
                    {
                        if (productsBLL.VerifyProductExistanceInDBWithId(Product) == 0)
                        {
                            productsBLL.ModifyProductInDB(Product);
                        }
                        else
                        {
                            MessageBox.Show("Product already existent");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Category non existent");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Category non existent");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }



        }

        private void DeleteProductInDB()
        {
            if (Product.NameProduct != null && Product.Barcode != null)
            {
                if (productsBLL.VerifyProductExistanceInDB(Product.NameProduct, Product.Barcode) > 0)
                {
                    productsBLL.DeleteProductInDB(Product);
                }
                else
                {
                    MessageBox.Show("Product already deleted");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }

    }
}

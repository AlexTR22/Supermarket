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
    public class ProductStocksVM:BasePropertyChange
    {
        ProductStocksBLL productStocksBLL = new ProductStocksBLL();

        public ObservableCollection<ProductStocks> ProductStocksList
        {
            get => productStocksBLL.ProductStocks;
            set => productStocksBLL.ProductStocks = value;
        }
        private ProductStocks _productStock;
        public ProductStocks ProductStock
        {
            get => _productStock;
            set
            {
                _productStock = value;
                NotifyPropertyChanged(nameof(ProductStock));
            }
        }

        
        public ProductStocksVM()
        {
            ProductStocksList = productStocksBLL.GetAllProductStocks();
            ProductStock = new ProductStocks();
        }



        private ICommand _addStock;
        public ICommand AddStock
        {
            get
            {
                if (_addStock == null)
                {
                    _addStock = new RelayCommand(AddStockInDB);
                }
                return _addStock;
            }
        }

        private ICommand _modifyStock;
        public ICommand ModifyStock
        {
            get
            {
                if (_modifyStock == null)
                {
                    _modifyStock = new RelayCommand(ModifyStockInDB);
                }
                return _modifyStock;
            }
        }

        private ICommand _deleteStock;
        public ICommand DeleteStock
        {
            get
            {
                if (_deleteStock == null)
                {
                    _deleteStock = new RelayCommand(DeleteStockInDB);
                }
                return _deleteStock;
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
                    if (window.Title == "ProductStocksWindow")
                    {
                        AdministratorPage administratorPage = new AdministratorPage();
                        administratorPage.Show();
                        window.Close();
                        break;
                    }
                }
            }
        }

        public void AddStockInDB()
        {
            if (ProductStock != null)
            {
                ProductsBLL productsBLL = new ProductsBLL();
                if (productsBLL.VerifyProductByNameInDB(ProductStock.ProductName) > 0)
                {
                    productStocksBLL.AddStockInDB(ProductStock);
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

        private void ModifyStockInDB()
        {
            if (ProductStock != null)
            {
                ProductsBLL productsBLL = new ProductsBLL();
                if (productsBLL.VerifyProductByNameInDB(ProductStock.ProductName) > 0)
                {
                    productStocksBLL.ModifyStockInDB(ProductStock);
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

        private void DeleteStockInDB()
        {
            if (ProductStock!=null)
            {
                productStocksBLL.DeleteStockInDB(ProductStock);
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;

namespace Tema_3.Model.BusinessLogicLayer
{
    public class ProductsBLL
    {
        public ObservableCollection<Products> Products {  get; set; }

        ProductsDAL productsDAL = new ProductsDAL();

        public ObservableCollection<Products> GetAllProducts()
        {
            return productsDAL.GetAllProducts();
        }

        public int VerifyProductExistanceInDB(string nameProduct, string barcode)
        {
            return productsDAL.VerifyProductExistanceInDB(nameProduct, barcode);
        }

        public int VerifyProductExistanceInDBWithId(Products product)
        {
            return productsDAL.VerifyProductExistanceInDBWithId(product);
        }

        public void AddProductInDB(Products product)
        {
            productsDAL.AddProductInDB(product);
            Products.Add(product);
        }

        public void ModifyProductInDB(Products product)
        {
            productsDAL.ModifyProductInDB(product);
        }
     
        public void DeleteProductInDB(Products product)
        {
            productsDAL.DeleteProductInDB(product);
        }

        public int VerifyProductByNameInDB(string? productName)
        {
            return productsDAL.VerifyProductByNameInDB(productName);
        }
    }
}

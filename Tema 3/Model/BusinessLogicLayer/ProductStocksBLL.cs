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
    internal class ProductStocksBLL
    {
        public ObservableCollection<ProductStocks> ProductStocks { get; set; }

        ProductStocksDAL productStocksDAL= new ProductStocksDAL();

        public ObservableCollection<ProductStocks> GetAllProductStocks()
        {
            return productStocksDAL.GetAllProducts();
        }

        public ObservableCollection<ProductStocks> GetAllProductStocks(string nameBarcode)
        {
            return productStocksDAL.GetAllProducts(nameBarcode);
        }

        public void AddStockInDB(ProductStocks productStock)
        {
            productStocksDAL.AddStockInDB(productStock);
            ProductStocks.Add(productStock);
        }

        public void ModifyStockInDB(ProductStocks productStock)
        {
            productStocksDAL.ModifyStockInDB(productStock);
        }

        public void DeleteStockInDB(ProductStocks productStock)
        {
            productStocksDAL.DeleteStockInDB(productStock);
        }

        public void ReduceQuantityOfStock(int? idProductStocks, int? quantity)
        {
            productStocksDAL.ReduceQuantityOfStock(idProductStocks, quantity);
        }

        public bool IsQuantityGreatherThenStock(int? idProductStocks, int? quantity)
        {
            return productStocksDAL.IsQuantityGreatherThenStock(idProductStocks, quantity);
        }
    }
}

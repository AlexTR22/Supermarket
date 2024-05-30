using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.EntityLayer;
using System.Configuration;
using System.Windows;

namespace Tema_3.Model.DataAccessLayer
{
    public class ProductStocksDAL
    {
        public ObservableCollection<ProductStocks> GetAllProducts()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProductStocks", con);
                ObservableCollection<ProductStocks> result = new ObservableCollection<ProductStocks>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductStocks product = new ProductStocks();
                    product.IdProductStocks=reader.GetInt32(0);
                    product.Quantity=reader.GetInt32(1);
                    product.UnitMeasure = reader.GetString(2);
                    product.SupplyDate = reader.GetDateTime(3);
                    product.ExpirationDate = reader.GetDateTime(4);
                    product.PurchasePrice= reader.GetDouble(5);
                    product.SalePrice= reader.GetDouble(6);
                    product.ProductName= reader.GetString(7);
                    string isDeleted=reader.GetString(8);
                    if (isDeleted != null)
                    {
                        if(isDeleted=="true")
                        {
                            product.IsDeletedProductStock = true;
                        }
                        else if (isDeleted=="false")
                        {
                            product.IsDeletedProductStock= false;
                        } 
                    }
                    result.Add(product);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public int GetProductId(string name)
        {
            int idProduct=-1;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetProductId", con);
                ObservableCollection<ProductStocks> result = new ObservableCollection<ProductStocks>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter("@name", name);
                cmd.Parameters.Add(nameParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idProduct=reader.GetInt32(0);
                }
                return idProduct;
            }
            finally
            {
                con.Close();
            }

        }

        public void AddStockInDB(ProductStocks productStock)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddStockInDB", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter quantityParam = new SqlParameter("@quantity", productStock.Quantity);
                SqlParameter unitMeasureParam = new SqlParameter("@unitMeasure", productStock.UnitMeasure);
                SqlParameter purchasePriceParam = new SqlParameter("@purchasePrice", productStock.PurchasePrice);
                SqlParameter supplyDateParam = new SqlParameter("@supplyDate", productStock.SupplyDate);
                SqlParameter expirationDateParam = new SqlParameter("@expirationDate", productStock.ExpirationDate);
                if(GetProductId(productStock.ProductName)==-1)
                {
                    MessageBox.Show("Product Not Found");
                    return;
                }
                else
                {
                    SqlParameter productIdParam = new SqlParameter("@productId", GetProductId(productStock.ProductName));
                    cmd.Parameters.Add(productIdParam);
                }
                if (productStock.SalePrice==null) 
                {
                    string value = ConfigurationManager.AppSettings["AdaosComercial"];
                    int val;
                    if (value != null)
                    {
                        val = int.Parse(value);
                    }
                    else 
                    {
                        MessageBox.Show("Unexpected error occured");
                        return;
                    }
                    SqlParameter salePriceParam = new SqlParameter("@salePrice", productStock.PurchasePrice+productStock.PurchasePrice*val/100);
                    cmd.Parameters.Add(salePriceParam);
                }
                else
                {
                    SqlParameter salePriceParam = new SqlParameter("@salePrice", productStock.SalePrice);
                    cmd.Parameters.Add(salePriceParam);
                }
                if (productStock.IsDeletedProductStock == null || productStock.IsDeletedProductStock == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (productStock.IsDeletedProductStock == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                paramId.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(quantityParam);
                cmd.Parameters.Add(unitMeasureParam);
                cmd.Parameters.Add(purchasePriceParam);
                cmd.Parameters.Add(supplyDateParam);
                cmd.Parameters.Add(expirationDateParam);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
                productStock.IdProductStocks= paramId.Value as int?;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyStockInDB(ProductStocks productStock)
        {
            //de facut procedura stocata
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyStockInDB", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", productStock.IdProductStocks);
                SqlParameter quantityParam = new SqlParameter("@quantity", productStock.Quantity);
                SqlParameter salePriceParam;
                if (productStock.SalePrice > productStock.PurchasePrice)
                {
                    salePriceParam = new SqlParameter("@salePrice", productStock.SalePrice);
                }
                else
                {
                    MessageBox.Show("Sale price can't be smaller then purchase price");
                    return;
                }
               
                if (productStock.IsDeletedProductStock == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (productStock.IsDeletedProductStock == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(quantityParam);
                cmd.Parameters.Add(salePriceParam);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteStockInDB(ProductStocks productStock)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteProductStockInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", productStock.IdProductStocks);
                cmd.Parameters.Add(idParam);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public ObservableCollection<ProductStocks> GetAllProducts(string nameBarcode)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                if (nameBarcode.All(char.IsDigit))
                {
                    SqlCommand cmd = new SqlCommand("GetAllProductStocksByBarcode", con);
                    ObservableCollection<ProductStocks> result = new ObservableCollection<ProductStocks>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter barcodeParam = new SqlParameter("@barcode", nameBarcode);
                    cmd.Parameters.Add(barcodeParam);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductStocks product = new ProductStocks();
                        product.IdProductStocks = reader.GetInt32(0);
                        product.Quantity = reader.GetInt32(1);
                        product.UnitMeasure = reader.GetString(2);
                        product.SupplyDate = reader.GetDateTime(3);
                        product.ExpirationDate = reader.GetDateTime(4);
                        product.PurchasePrice = reader.GetDouble(5);
                        product.SalePrice = reader.GetDouble(6);
                        product.ProductName = reader.GetString(7);
                        string isDeleted = reader.GetString(8);
                        if (isDeleted != null)
                        {
                            if (isDeleted == "true")
                            {
                                product.IsDeletedProductStock = true;
                            }
                            else if (isDeleted == "false")
                            {
                                product.IsDeletedProductStock = false;
                            }
                        }
                        result.Add(product);
                    }
                    reader.Close();
                    return result;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("GetAllProductStocksByName", con);
                    ObservableCollection<ProductStocks> result = new ObservableCollection<ProductStocks>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter nameParan = new SqlParameter("@name", nameBarcode);
                    cmd.Parameters.Add(nameParan);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductStocks product = new ProductStocks();
                        product.IdProductStocks = reader.GetInt32(0);
                        product.Quantity = reader.GetInt32(1);
                        product.UnitMeasure = reader.GetString(2);
                        product.SupplyDate = reader.GetDateTime(3);
                        product.ExpirationDate = reader.GetDateTime(4);
                        product.PurchasePrice = reader.GetDouble(5);
                        product.SalePrice = reader.GetDouble(6);
                        product.ProductName = reader.GetString(7);
                        string isDeleted = reader.GetString(8);
                        if (isDeleted != null)
                        {
                            if (isDeleted == "true")
                            {
                                product.IsDeletedProductStock = true;
                            }
                            else if (isDeleted == "false")
                            {
                                product.IsDeletedProductStock = false;
                            }
                        }
                        result.Add(product);
                    }
                    reader.Close();
                    return result;
                }


            }
            finally
            {
                con.Close();
            }
        }

        public void ReduceQuantityOfStock(int? idProductStocks, int? quantity)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ReduceQuantityOfStock", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", idProductStocks);
                SqlParameter quantityParam = new SqlParameter("@quantity", quantity);
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(quantityParam);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public bool IsQuantityGreatherThenStock(int? idProductStocks, int? quantity)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetQuantityOfStock", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", idProductStocks);
                cmd.Parameters.Add(idParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int stockQuantity=0;
                while (reader.Read())
                {
                    stockQuantity = reader.GetInt32(0);
                }
                reader.Close();
                if (stockQuantity>=quantity)
                {
                    return false;
                }
                else
                {
                    return true;
                }
               
            }
            finally
            {
                con.Close();
            }
        }
    }
}

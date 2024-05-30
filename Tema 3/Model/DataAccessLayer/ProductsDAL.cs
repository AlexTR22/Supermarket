using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Tema_3.Model.EntityLayer;

namespace Tema_3.Model.DataAccessLayer
{
    public class ProductsDAL
    {
        public ObservableCollection<Products> GetAllProducts()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProducts",con);
                ObservableCollection<Products> result = new ObservableCollection<Products>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    Products product = new Products();
                    product.IdProduct = (int)reader[0];
                    product.NameProduct = reader.GetString(1);
                    product.Barcode= reader.GetString(2);
                    product.CategoryProduct = reader.GetString(3);
                    product.ProducerProduct = reader.GetString(4);
                    string isDeleted = reader.GetString(5);
                    if(isDeleted!=null)
                    {
                        if(isDeleted=="true")
                        {
                            product.IsDeletedProduct = true;
                        }
                        else if(isDeleted=="false")
                        {
                            product.IsDeletedProduct = false;
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

        public int VerifyProductExistanceInDB(string name, string barcode)
        {
            int isProductsInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyProductExistanceInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter("@name", name);
                SqlParameter barcodeParam = new SqlParameter("@barcode", barcode);
                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(barcodeParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isProductsInDB = reader.GetInt32(0);
                }
                reader.Close();
                return isProductsInDB;
            }
            finally
            {
                con.Close();
            }
        }
        public int VerifyProductExistanceInDBWithId(Products product)
        {
            int isUserInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyProductExistanceInDBWithId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", product.IdProduct);
                SqlParameter nameParam = new SqlParameter("@name", product.NameProduct);
                SqlParameter barcodeParam = new SqlParameter("@barcode", product.Barcode);
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(barcodeParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isUserInDB = reader.GetInt32(0);
                }
                reader.Close();
                return isUserInDB;
            }
            finally
            {
                con.Close();
            }
        }

        public int VerifyProductByNameInDB(string? productName)
        {
            int isProductsInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyProductByNameInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter("@name", productName);
                cmd.Parameters.Add(nameParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isProductsInDB = reader.GetInt32(0);
                }
                reader.Close();
                return isProductsInDB;
            }
            finally
            {
                con.Close();
            }
        }

        public int GetCategoryId(string name)
        {
            int idCategory = -1;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetCategoryId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameCategoryParam = new SqlParameter("@name", name);
                cmd.Parameters.Add(nameCategoryParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idCategory = reader.GetInt32(0);
                }
                con.Close();
                if (idCategory < 0)
                {
                    MessageBox.Show("Category Not Found");
                    return -1;
                }
                return idCategory;
            }
            finally
            {
                con.Close();
            }
        }
        public int GetProducerId(string name)
        {
            int idProducer = -1;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetProducerId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameCategoryParam = new SqlParameter("@name", name);
                cmd.Parameters.Add(nameCategoryParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idProducer = reader.GetInt32(0);
                }
                con.Close();
                if (idProducer < 0)
                {
                    MessageBox.Show("Category Not Found");
                    return -1;
                }
                return idProducer;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddProductInDB(Products product)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddProductInDB", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter("@name", product.NameProduct);
                SqlParameter barcodeParam = new SqlParameter("@barcode", product.Barcode);
                SqlParameter categoryParam = new SqlParameter("@category", GetCategoryId(product.CategoryProduct));
                SqlParameter producerParam = new SqlParameter("@producer", GetProducerId(product.ProducerProduct));
                
                if (product.IsDeletedProduct== null || product.IsDeletedProduct == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (product.IsDeletedProduct == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                paramId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(barcodeParam);
                cmd.Parameters.Add(categoryParam);
                cmd.Parameters.Add(producerParam);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
                product.IdProduct = paramId.Value as int?;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyProductInDB(Products product)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyProductInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", product.IdProduct);
                SqlParameter nameParam = new SqlParameter("@name", product.NameProduct);
                SqlParameter barcodeParam = new SqlParameter("@barcode", product.Barcode);
                SqlParameter categroyParam = new SqlParameter("@category", GetCategoryId(product.CategoryProduct));
                SqlParameter producerParam = new SqlParameter("@producer", GetProducerId(product.ProducerProduct));
                if (product.IsDeletedProduct == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (product.IsDeletedProduct == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(barcodeParam);
                cmd.Parameters.Add(categroyParam);
                cmd.Parameters.Add(producerParam);
            
                con.Open();
                cmd.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteProductInDB(Products product)
        {
            
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteProductInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idUser = new SqlParameter("@id", product.IdProduct);
                cmd.Parameters.Add(idUser);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        
    }
}

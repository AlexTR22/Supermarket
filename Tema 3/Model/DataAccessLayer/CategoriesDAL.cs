using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.EntityLayer;

namespace Tema_3.Model.DataAccessLayer
{
    public class CategoriesDAL
    {
        public ObservableCollection<Categories> GetAllCategories()
        {
            SqlConnection con = DALHelper.Connection;
            try 
            {
                SqlCommand cmd = new SqlCommand("GetAllCategories",con);
                ObservableCollection<Categories> result = new ObservableCollection<Categories>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    Categories category = new Categories();
                    category.IdCategory = (int)reader[0];
                    category.Category = reader.GetString(1);
                    string isDeleted = reader.GetString(2);
                    if (isDeleted != null)
                    {
                        if (isDeleted == "true")
                        {
                            category.IsDeletedCategory = true;
                        }
                        else if (isDeleted == "false")
                        {
                            category.IsDeletedCategory = false;
                        }
                    }
                    result.Add(category);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }


        public int VerifyCategoryExistanceInDB(string category)
        {
            int isCategInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyCategoryInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter categ = new SqlParameter("@Category", category);
                cmd.Parameters.Add(categ);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isCategInDB = reader.GetInt32(0);
                }
                reader.Close();
                return isCategInDB;
            }
            finally
            {
                con.Close();
            }
        }

        public int VerifyCategoryExistanceInDBWithId(Categories category)
        {
            int isCategInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyCategoryExistanceInDBWithId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", category.IdCategory);
                SqlParameter categoryParam = new SqlParameter("@Category", category.Category);
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(categoryParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isCategInDB = reader.GetInt32(0);
                }
                reader.Close();
                return isCategInDB;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddCategoryInDB(Categories category)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddCategoryInDB", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramCategory = new SqlParameter("@category", category.Category);
                SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                paramId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramCategory);
                cmd.Parameters.Add(paramIsDeleted);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
                category.IdCategory = paramId.Value as int?;
            }
            finally
            {
                con.Close();
            }
        }

       
        public void ModifyCategoryInDB(Categories category)
        {
            int? id = category.IdCategory;
            string? categ = category.Category;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyCategoryInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramCateg = new SqlParameter("@category", categ);
                SqlParameter idCategory = new SqlParameter("@id", id);
                //SqlParameter isdeleted = new SqlParameter("@isDeleted", category.IsDeleted);
                if (category.IsDeletedCategory)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (category.IsDeletedCategory == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                cmd.Parameters.Add(idCategory);
                cmd.Parameters.Add(paramCateg);
                //cmd.Parameters.Add(isdeleted);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteCategoryInDB(Categories category)
        {
            int? id = category.IdCategory;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteCategoryInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idCategory = new SqlParameter("@id", id);
                cmd.Parameters.Add(idCategory);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        internal void DeleteProductsInDBInCascadeByCategory(int? id)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteProductsInDBInCascadeByCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idCategory = new SqlParameter("@id", id);
                cmd.Parameters.Add(idCategory);
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

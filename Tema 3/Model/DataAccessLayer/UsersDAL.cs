using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.EntityLayer;

namespace Tema_3.Model.DataAccessLayer
{
    public class UsersDAL
    {
        public ObservableCollection<Users> GetAllUsers()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllUsers", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Users user = new Users();
                    user.Id = (int)reader[0];
                    user.Name = reader.GetString(1);
                    user.Password = reader.GetString(2);
                    user.Type=reader.GetString(3);
                    string isDeleted=reader.GetString(4);
                    if(isDeleted != null)
                    {
                        if(isDeleted == "true")
                        {
                            user.IsDeleted = true;
                        }
                        else if(isDeleted == "false") 
                        {
                            user.IsDeleted = false;
                        }
                    }
                    result.Add(user);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public int VerifyUserExistanceInDB(string userName, string password)
        {
            int isUserInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyUserExistanceInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter user = new SqlParameter("@UserName", userName);
                SqlParameter pass = new SqlParameter("@Password", password);
                cmd.Parameters.Add(user);
                cmd.Parameters.Add(pass);
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

        public int VerifyUserExistanceInDBWithId(Users user)
        {
            int isUserInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyUserExistanceInDBWithId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", user.Id);
                SqlParameter userParam = new SqlParameter("@UserName", user.Name);
                SqlParameter passwordParam = new SqlParameter("@Password", user.Password);
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(userParam);
                cmd.Parameters.Add(passwordParam);
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

        public void AddUserInDB(Users user)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddUserInDB", con);
               //ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@name", user.Name);
                SqlParameter paramPassword = new SqlParameter("@password", user.Password);
                
                if (user.IsDeleted==null || user.IsDeleted == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if(user.IsDeleted==true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                SqlParameter paramType;
                if (user.Type != null)
                {
                    paramType = new SqlParameter("@type", user.Type);
                }
                else 
                {
                    paramType = new SqlParameter("@type", "cachier");
                }
                SqlParameter paramId= new SqlParameter("@id", SqlDbType.Int);
                paramId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramPassword);
                cmd.Parameters.Add(paramType);
                
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
                user.Id = paramId.Value as int?;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyUserInDB(Users user)
        {
            int? id = user.Id;
            string? name = user.Name;
            string? password = user.Password;

            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyUserInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter username = new SqlParameter("@name", name);
                SqlParameter pass = new SqlParameter("@password", password);
                SqlParameter idUser = new SqlParameter("@id", id);
                if (user.IsDeleted == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (user.IsDeleted == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                //SqlParameter idDeleted = new SqlParameter("@isDeleted", user.IsDeleted);
                cmd.Parameters.Add(idUser);
                cmd.Parameters.Add(username);
                cmd.Parameters.Add(pass);
                //cmd.Parameters.Add(idDeleted);
                con.Open();
                cmd.ExecuteNonQuery();
               
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteUserInDB(Users user)
        {
            int? id = user.Id;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteUserInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idUser = new SqlParameter("@id", id);
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

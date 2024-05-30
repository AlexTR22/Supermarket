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
    public class LoginDAL
    {
        public int VerifyUserExistanceInDB(string userName,string password)
        {
            int isUserInDB=0;
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

        public string GetUserType(string userName, string password)
        {
            
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetUserTypeAfterLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter user = new SqlParameter("@UserName", userName);
                SqlParameter pass = new SqlParameter("@Password", password);
                cmd.Parameters.Add(user);
                cmd.Parameters.Add(pass);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string userType ="-";
                while (reader.Read())
                {
                    userType = reader.GetString(0);
                }
                reader.Close();
                return userType;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.EntityLayer;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Tema_3.Model.DataAccessLayer
{
    public class ProducersDAL
    {
        public ObservableCollection<Producers> GetAllProducers()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProducers", con);
                ObservableCollection<Producers> result = new ObservableCollection<Producers>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producers producer = new Producers();
                    producer.IdProducer = (int)reader[0];
                    producer.NameProducer = reader.GetString(1);
                    producer.Country= reader.GetString(2);
                    string isDeleted = reader.GetString(3);
                    if (isDeleted != null)
                    {
                        if (isDeleted == "true")
                        {
                            producer.IsDeletedProducer = true;
                        }
                        else if (isDeleted == "false")
                        {
                            producer.IsDeletedProducer = false;
                        }
                    }
                    result.Add(producer);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        internal int VerifyProducerExistanceInDB(string name)
        {
            int isUserInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyProducerByNameInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter("@Name", name);
                cmd.Parameters.Add(nameParam);
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

        public int VerifyProducerExistanceInDB(string name, string country)
        {
            int isUserInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyProducerInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter("@Name", name);
                SqlParameter countryParam = new SqlParameter("@Country", country);
                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(countryParam);
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

        public int VerifyProducerExistanceInDBWithId(Producers producer)
        {
            int isUserInDB = 0;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("VerifyProducerExistanceInDBWithId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter("@id", producer.IdProducer);
                SqlParameter nameParam = new SqlParameter("@name", producer.NameProducer);
                SqlParameter countryParam = new SqlParameter("@country", producer.Country);
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(countryParam);
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

        public void AddProducerInDB(Producers producer)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddProducerInDB", con);
                ObservableCollection<Users> result = new ObservableCollection<Users>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@name", producer.NameProducer);
                SqlParameter paramCountry = new SqlParameter("@country", producer.Country);

                if (producer.IsDeletedProducer == null || producer.IsDeletedProducer == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (producer.IsDeletedProducer == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                paramId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramCountry);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
                producer.IdProducer = paramId.Value as int?;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyProducerInDB(Producers producer)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyProducerInDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idUser = new SqlParameter("@id", producer.IdProducer);
                SqlParameter username = new SqlParameter("@name", producer.NameProducer);
                SqlParameter pass = new SqlParameter("@country", producer.Country);
                //SqlParameter isDeleted = new SqlParameter("@isDeleted", producer.IsDeleted);
                if (producer.IsDeletedProducer == false)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "false");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                else if (producer.IsDeletedProducer == true)
                {
                    SqlParameter paramIsDeleted = new SqlParameter("@isDeleted", "true");
                    cmd.Parameters.Add(paramIsDeleted);
                }
                cmd.Parameters.Add(idUser);
                cmd.Parameters.Add(username);
                cmd.Parameters.Add(pass);
                //cmd.Parameters.Add(isDeleted);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteProducerInDB(Producers producer)
        {
            int? id = producer.IdProducer;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteProducerInDB", con);
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

        public void DeleteProductsInCascadeByProducer(int? idProducer)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteProductsInCascadeByProducer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idProducerParam= new SqlParameter("@id", idProducer);
                cmd.Parameters.Add(idProducerParam);
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

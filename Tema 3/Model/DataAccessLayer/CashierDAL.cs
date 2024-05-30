using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.EntityLayer;
using System.Collections.ObjectModel;

namespace Tema_3.Model.DataAccessLayer
{
    public class CashierDAL
    {
        public int? CreateNewBill(float? total, DateTime releaseDate)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("CreateNewBill", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter totalParam = new SqlParameter("@total", total);
                SqlParameter releaseDateParam = new SqlParameter("@releaseDate", releaseDate);
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);

                paramId.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(totalParam);
                cmd.Parameters.Add(releaseDateParam);
                cmd.Parameters.Add(paramId);

                con.Open();
                cmd.ExecuteNonQuery();
                return paramId.Value as int?;
            }
            finally
            {
                con.Close();
            }
        }

        public int GetProductId(string name)
        {
            int idProduct = -1;
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetProductId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter("@name", name);
                cmd.Parameters.Add(nameParam);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idProduct = reader.GetInt32(0);
                }
                return idProduct;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddBillToDB(ProductOnBill prodOnBill)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddBillToDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idProductParam = new SqlParameter("@idProduct", GetProductId(prodOnBill.NameProductOnBill));
                SqlParameter idBillParam = new SqlParameter("@idBill", prodOnBill.IdBill);
                SqlParameter quantityParam = new SqlParameter("@quantity", prodOnBill.QuantityProductOnBill);
                SqlParameter subtotalParam = new SqlParameter("@subTotal", prodOnBill.SubTotal);
                SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);

                paramId.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(idProductParam);
                cmd.Parameters.Add(idBillParam);
                cmd.Parameters.Add(quantityParam);
                cmd.Parameters.Add(subtotalParam);
                cmd.Parameters.Add(paramId);

                con.Open();
                cmd.ExecuteNonQuery();
                ///return paramId.Value as int?;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

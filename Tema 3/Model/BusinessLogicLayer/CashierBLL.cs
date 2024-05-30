using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;

namespace Tema_3.Model.BusinessLogicLayer
{
    public class CashierBLL
    {
        CashierDAL cashierDAL = new CashierDAL();   

        public int? CreateNewBill(float? total, DateTime now)
        {
            return cashierDAL.CreateNewBill(total, now);
        }

        public void AddBillToDB(ProductOnBill prodOnBill)
        {
            cashierDAL.AddBillToDB(prodOnBill);
        }
    }
}

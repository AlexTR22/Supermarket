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
    class LoginBLL
    {
        LoginDAL loginDAL=new LoginDAL();

        public int VerifyUserExistanceInDB(string username,string password)
        {
            return loginDAL.VerifyUserExistanceInDB(username,password);
        }

        public string GetUserType(string username, string password)
        {
            return loginDAL.GetUserType(username, password);
        }
    }
}

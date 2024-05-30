using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema_3.Commands;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;
using static System.Net.Mime.MediaTypeNames;

namespace Tema_3.Model.BusinessLogicLayer
{
    public class UsersBLL
    {

        UsersDAL usersDAL=new UsersDAL();
        public ObservableCollection<Users> Users { get; set; }


        public UsersBLL()
        {
            Users = usersDAL.GetAllUsers();
        }


        public ObservableCollection<Users> GetAllUsers()
        {
            return usersDAL.GetAllUsers();
        }

        public int VerifyUserExistanceInDB(string userName, string password)
        {
            return usersDAL.VerifyUserExistanceInDB(userName, password);
        }

        public void AddUserInDB(Users user)
        {
            usersDAL.AddUserInDB(user);
            Users.Add(user);
        }

        public void ModifyUserInDB(Users user)
        {
            usersDAL.ModifyUserInDB(user);
        }

        public void DeleteUserInDB(Users user)
        {
            usersDAL.DeleteUserInDB(user);
            //Users.Remove(user);
        }

        internal int VerifyUserExistanceInDBWithId(Users user)
        {
            return usersDAL.VerifyUserExistanceInDBWithId(user);
        }
    }
}

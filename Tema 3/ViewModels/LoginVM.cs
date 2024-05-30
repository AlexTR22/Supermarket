using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema_3.Commands;
using Tema_3.Model.BusinessLogicLayer;
using Tema_3.Model.EntityLayer;
using Tema_3.Views;

namespace Tema_3.ViewModels
{
    public class LoginVM : BasePropertyChange
    {
        
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            { _userName = value;  NotifyPropertyChanged("UserNameInput"); }
        }
    
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password= value; NotifyPropertyChanged("PasswordInput"); }
        }

        private string? _userType;
        public string? UserType
        {
            get { return _userType; }
            set { _userType = value; NotifyPropertyChanged("UserTypeLogin"); }
        }

        LoginBLL loginBLL = new LoginBLL();





        private ICommand _login;
        public ICommand Login
        {
            get
            {
                if(_login == null)
                {
                    _login = new RelayCommand(LoginFunction);
                }
                return _login;
            }
        }
        public void LoginFunction()
        {

            string @user = UserName;
            string @pass= Password;
            if(user==null)
            {
                MessageBox.Show("Introduce Username!");
                return;
            }
            if (pass == null)
            {
                MessageBox.Show("Introduce Password!");
                return;
            }
            int isUserInDB = loginBLL.VerifyUserExistanceInDB(@user, @pass);
            UserType= loginBLL.GetUserType(@user,@pass);
            if(UserType == "-")
            {
                MessageBox.Show("Couldn't Login. An error has occured.");
                return;
            }
            foreach (Window window in Application.Current.Windows)
            {
                // Check if the window is currently active or has focus
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "Login")
                    {
                        if(UserType=="Admin")
                        {
                           AdministratorPage administratorPage = new AdministratorPage();
                            administratorPage.Show();
                            //admin page
                        }
                        else if(UserType=="Cashier")
                        {
                            CashierUI cashierUI = new CashierUI();
                            cashierUI.Show();
                        }
                        window.Close();
                        break;
                    }
                }
            }


        }
    }
}

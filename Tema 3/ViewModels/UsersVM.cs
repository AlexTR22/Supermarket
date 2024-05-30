using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema_3.Commands;
using Tema_3.Model.BusinessLogicLayer;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;
using Tema_3.Views;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;

namespace Tema_3.ViewModels
{
    public class UsersVM : BasePropertyChange
    {
        UsersBLL usersBLL=new UsersBLL();

        public UsersVM()
        {
            UserList = usersBLL.GetAllUsers();
            User = new Users();
        }
        private Users _user;
        public Users User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                NotifyPropertyChanged(nameof(User));
            }
        }

        public ObservableCollection<Users> UserList
        {
            get => usersBLL.Users;
            set => usersBLL.Users = value;
        }



   

        private ICommand _addUser;
        public ICommand AddUser
        {
            get
            {
                if(_addUser == null)
                {
                    _addUser = new RelayCommand(AddUserInDB);
                }
                return _addUser;
            }
        }

        private ICommand _modifyUser;
        public ICommand ModifyUser
        {
            get
            {
                if (_modifyUser == null)
                {
                    _modifyUser = new RelayCommand(ModifyUserInDB);
                }
                return _modifyUser;
            }
        }

        private ICommand _deleteUser;
        public ICommand DeleteUser
        {
            get
            {
                if (_deleteUser == null)
                {
                    _deleteUser = new RelayCommand(DeleteUserInDB);
                }
                return _deleteUser;
            }
        }

        private ICommand _goBack;
        public ICommand GoBack
        {
            get
            {
                if (_goBack == null)
                {
                    _goBack = new RelayCommand(GoBackCommand);
                }
                return _goBack;
            }
        }


        
        public void GoBackCommand()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "UsersWindow")
                    {
                        AdministratorPage administratorPage = new AdministratorPage();
                        administratorPage.Show();
                        window.Close();
                        break;
                    }
                }
            }
        }
        
        public void AddUserInDB()
        {
            if(User.Name!=null && User.Password!=null)
            {
                if (usersBLL.VerifyUserExistanceInDB(User.Name, User.Password) == 0)
                {
                    usersBLL.AddUserInDB(User);
                }
                else 
                {
                    MessageBox.Show("User already existent");
                    return; 
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }

        private void ModifyUserInDB()
        {
            if (User.Name != null && User.Password != null)
            {
                if (usersBLL.VerifyUserExistanceInDBWithId(User) == 0)
                {
                    usersBLL.ModifyUserInDB(User);
                }
                else
                {
                    MessageBox.Show("User already existent");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }

        private void DeleteUserInDB()
        {
            if (User.Name != null && User.Password != null)
            {
                if (usersBLL.VerifyUserExistanceInDB(User.Name, User.Password) > 0)
                {
                    usersBLL.DeleteUserInDB(User);
                }
                else
                {
                    MessageBox.Show("User already deleted");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }


    }
}

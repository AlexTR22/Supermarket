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
using Tema_3.Model.EntityLayer;
using Tema_3.Views;

namespace Tema_3.ViewModels
{
    public class CategoriesVM : BasePropertyChange
    {
        CategoriesBLL categoriesBLL= new CategoriesBLL();

        public ObservableCollection<Categories> CategoriesList
        {
            get => categoriesBLL.Categories;
            set => categoriesBLL.Categories = value;
        }

        public Categories _category;
        public Categories Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                NotifyPropertyChanged(nameof(Category));
            }
        }


        public CategoriesVM()
        {
            CategoriesList = categoriesBLL.GetAllCategories();
            Category = new Categories();
        }




        private ICommand _addCategory;
        public ICommand AddCategory
        {
            get
            {
                if (_addCategory == null)
                {
                    _addCategory = new RelayCommand(AddCategoryInDB);
                }
                return _addCategory;
            }
        }

        private ICommand _modifyCategory;
        public ICommand ModifyCategory
        {
            get
            {
                if (_modifyCategory == null)
                {
                    _modifyCategory = new RelayCommand(ModifyCategoryInDB);
                }
                return _modifyCategory;
            }
        }

        private ICommand _deleteCategory;
        public ICommand DeleteCategory
        {
            get
            {
                if (_deleteCategory == null)
                {
                    _deleteCategory = new RelayCommand(DeleteCategoryInDB);
                }
                return _deleteCategory;
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
                    if (window.Title == "CategoryWindow")
                    {
                        AdministratorPage administratorPage = new AdministratorPage();
                        administratorPage.Show();
                        window.Close();
                        break;
                    }
                }
            }
        }

        public void AddCategoryInDB()
        {
            if (Category.Category != null)
            {
                if (categoriesBLL.VerifyCategoryExistanceInDB(Category.Category) == 0)
                {
                    categoriesBLL.AddCategoryInDB(Category);
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

        private void ModifyCategoryInDB()
        {
            if (Category.Category != null)
            {
                if (categoriesBLL.VerifyCategoryExistanceInDBWithId(Category) == 0)
                {
                    categoriesBLL.ModifyCategoryInDB(Category);
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

        private void DeleteCategoryInDB()
        {
            if (Category.Category != null)
            {
                if (categoriesBLL.VerifyCategoryExistanceInDB(Category.Category) != 0)
                {
                    categoriesBLL.DeleteCategoryInDB(Category);
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

    }
}

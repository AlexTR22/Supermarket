using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema_3.Commands;
using Tema_3.Views;

namespace Tema_3.ViewModels
{
    public class AdministratorPageVM
    {
        private ICommand _users;
        public ICommand Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new RelayCommand(UsersWindow);
                }
                return _users;
            }
        }

        private ICommand _categories;
        public ICommand Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new RelayCommand(CategoriesWindow);
                }
                return _categories;
            }
        }

        private ICommand _producers;
        public ICommand Producers
        {
            get
            {
                if (_producers == null)
                {
                    _producers = new RelayCommand(ProducersWindow);
                }
                return _producers;
            }
        }

        private ICommand _products;
        public ICommand Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new RelayCommand(ProductsWindow);
                }
                return _products;
            }
        }

        private ICommand _productStocks;
        public ICommand ProductStocks
        {
            get
            {
                if (_productStocks == null)
                {
                    _productStocks = new RelayCommand(ProductStocksWindow);
                }
                return _productStocks;
            }
        }


        public void ProductStocksWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                // Check if the window is currently active or has focus
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "AdministratorPage")
                    {
                        ProductStocksWindow productStocksWindow = new ProductStocksWindow();
                        productStocksWindow.Show();
                        window.Close();
                        break;

                    }
                }
            }

        }

        public void ProductsWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                // Check if the window is currently active or has focus
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "AdministratorPage")
                    {
                        ProductsWindow productsWindow = new ProductsWindow();
                        productsWindow.Show();
                        window.Close();
                        break;

                    }
                }
            }
        }

        public void UsersWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                // Check if the window is currently active or has focus
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "AdministratorPage")
                    {
                        UsersWindow usersWindow = new UsersWindow();
                        usersWindow.Show();
                        window.Close();
                        break;

                    }
                }
            }
        }

        public void CategoriesWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                // Check if the window is currently active or has focus
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "AdministratorPage")
                    {
                        CategoryWindow categoriesWindow = new CategoryWindow();
                        categoriesWindow.Show();
                        window.Close();
                        break;

                    }
                }
            }
        }

        public void ProducersWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                // Check if the window is currently active or has focus
                if (window.IsActive || window.IsFocused)
                {
                    if (window.Title == "AdministratorPage")
                    {
                        ProducersWindow producersWindow = new ProducersWindow();
                        producersWindow.Show();
                        window.Close();
                        break;

                    }
                }
            }
        }

    }
}

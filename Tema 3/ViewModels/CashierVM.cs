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

namespace Tema_3.ViewModels
{
    public class CashierVM :BasePropertyChange
    {
        ProductStocksBLL stocksBLL=new ProductStocksBLL();
        CashierBLL cashierBLL = new CashierBLL();
        
        private ObservableCollection<ProductStocks> _productListSearch;
        public ObservableCollection<ProductStocks> ProductListSearch
        {
            get
            {
                return _productListSearch;
            }
            set
            {
                _productListSearch = value; NotifyPropertyChanged(nameof(ProductListSearch));
            }
           
        }

        public ObservableCollection<ProductOnBill> ProductListOnBill
        {
            //la fel ca la productStocks
            get; set;
        }

        private ProductStocks _productStocks;
        public ProductStocks ProductStocks
        {
            get {  return _productStocks; }
            set { _productStocks = value; NotifyPropertyChanged(nameof(ProductStocks)); }
        }

        private int? _quantity;
        public int? Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity= value; NotifyPropertyChanged(nameof(Quantity));
            }
        }

        private string _nameBarcode;
        public string NameBarcode
        {
            get
            {
                return _nameBarcode;
            }
            set
            {
                _nameBarcode = value; NotifyPropertyChanged(nameof(NameBarcode));
            }
        }

        private float? _total;
        public float? Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value; NotifyPropertyChanged(nameof(Total));
            }
        }

        public CashierVM()
        {
            ProductListOnBill = new ObservableCollection<ProductOnBill>();
            Total = 0;
        }


        private ICommand _addOnBill;
        public ICommand AddOnBill
        {
            get
            {
                if (_addOnBill == null)
                {
                    _addOnBill = new RelayCommand(AddOnBillCommand);
                }
                return _addOnBill;
            }
        }

        public void AddOnBillCommand()
        {
            ProductOnBill product = new ProductOnBill();
            product.SubTotal = Quantity * (float)ProductStocks.SalePrice;
            product.QuantityProductOnBill = Quantity;
            product.NameProductOnBill = ProductStocks.ProductName;
            ProductListOnBill.Add(product);
            if (stocksBLL.IsQuantityGreatherThenStock(ProductStocks.IdProductStocks, Quantity)==false)
            {
                stocksBLL.ReduceQuantityOfStock(ProductStocks.IdProductStocks, Quantity);
            }
            else
            {
                MessageBox.Show("Not enough products from this stock");
            }
            Total = Total + product.SubTotal;

        }

        private ICommand _search;
        public ICommand Search
        {
            get
            {
                if (_search == null)
                {
                    _search = new RelayCommand(SearchCommmand);
                }
                return _search;
            }
        }

        public void SearchCommmand()
        {
            //de facut in baza de date procedurile stocate pentru cautare
            ProductListSearch = stocksBLL.GetAllProductStocks(NameBarcode);
        }

        private ICommand _printReceipt;
        public ICommand PrintReceipt
        {
            get
            {
                if (_printReceipt == null)
                {
                    _printReceipt = new RelayCommand(PrintReceiptCommand);
                }
                return _printReceipt;
            }
        }

        public void PrintReceiptCommand()
        {
            int? idBill=cashierBLL.CreateNewBill(Total,DateTime.Now);
            //de facut bll, dal, procedurile stocate de adaugat bonul in baza de date
            foreach (ProductOnBill prodOnBill in ProductListOnBill) 
            { 
                prodOnBill.IdBill = idBill;
                cashierBLL.AddBillToDB(prodOnBill);
            }
        }
    }
}

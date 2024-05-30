using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_3.Model.EntityLayer
{
    public class ProductStocks : BasePropertyChange
    {
        private int? _id;
        public int? IdProductStocks
        {
            get { return _id; } 
            set { _id = value; NotifyPropertyChanged(nameof(IdProductStocks)); } 
        }

        private int? _quantity;
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; NotifyPropertyChanged(nameof(Quantity)); }
        }

        private string? _unitMeasure;
        public string? UnitMeasure
        {
            get { return _unitMeasure; }
            set { _unitMeasure = value; NotifyPropertyChanged(nameof(UnitMeasure)); }
        }

        private DateTime? _supplyDate;
        public DateTime? SupplyDate
        {
            get { return _supplyDate; }
            set { _supplyDate = value; NotifyPropertyChanged(nameof(SupplyDate)); }
        }

        private DateTime? _expirationDate;
        public DateTime? ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; NotifyPropertyChanged(nameof(ExpirationDate)); }
        }

        private double? _purchasePrice;
        public double? PurchasePrice
        {
            get { return _purchasePrice; }
            set { _purchasePrice = value; NotifyPropertyChanged(nameof(PurchasePrice)); }
        }

        private double? _salePrice;
        public double? SalePrice
        {
            get { return _salePrice; }
            set { _salePrice = value; NotifyPropertyChanged(nameof(SalePrice)); }
        }

        private string? _productName;
        public string? ProductName
        {
            get { return _productName; }
            set { _productName = value; NotifyPropertyChanged(nameof(ProductName)); }
        }

        private bool? _isDeleted;
        public bool? IsDeletedProductStock
        {
            get { return _isDeleted; }
            set { _isDeleted = value;NotifyPropertyChanged(nameof(IsDeletedProductStock)); }
        }
    }
}

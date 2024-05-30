using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_3.Model.EntityLayer
{
    public class ProductOnBill:BasePropertyChange
    {
        private int? _id;
        public int? IdProductOnBill
        { 
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(nameof(IdProductOnBill)); } 
        }

        private string _nameProduct;
        public string NameProductOnBill
        {
            get { return _nameProduct; }
            set { _nameProduct = value; NotifyPropertyChanged(nameof(NameProductOnBill)); }
        }

        private float? _subTotal;
        public float? SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; NotifyPropertyChanged(nameof(SubTotal)); }
        }

        private int? _quantity;
        public int? QuantityProductOnBill
        {
            get { return _quantity; }
            set { _quantity = value; NotifyPropertyChanged(nameof(QuantityProductOnBill));}
        }

        private int? _idBill;
        public int? IdBill
        {
            get { return _idBill; }
            set { _idBill = value; NotifyPropertyChanged(nameof(IdBill)); }
        }
    }
}

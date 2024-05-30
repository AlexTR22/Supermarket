using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_3.Model.EntityLayer
{
    public class Products:BasePropertyChange
    {
        private int? _id;
        public int? IdProduct
        { 
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(nameof(IdProduct)); }
        }
        
        private string? _name;
        public string? NameProduct
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(NameProduct)); }
        }

        private string? _barcode;
        public string? Barcode
        {
            get { return _barcode; }
            set { _barcode = value; NotifyPropertyChanged(nameof(Barcode)); }
        }

        private string? _category;
        public string? CategoryProduct
        {
            get { return _category; }
            set { _category = value; NotifyPropertyChanged(nameof(CategoryProduct)); }
        }

        private string? _producer;
        public string? ProducerProduct
        {
            get { return _producer; }
            set { _producer = value; NotifyPropertyChanged(nameof(ProducerProduct)); }
        }

        private bool? _isDeleted;
        public bool? IsDeletedProduct
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(nameof(IsDeletedProduct)); }
        }
    }
}

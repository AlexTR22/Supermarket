using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_3.Model.EntityLayer
{
    public class Producers:BasePropertyChange
    {
        private int? _id;
        public int? IdProducer
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(nameof(IdProducer)); }
        }

        private string? _name;
        public string? NameProducer
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(NameProducer)); }
        }

        private string? _country;
        public string? Country
        {
            get { return _country; }
            set { _country = value; NotifyPropertyChanged(nameof(Country)); }
        }

        private bool? _isDeleted;
        public bool? IsDeletedProducer
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(nameof(IsDeletedProducer)); }
        }
    }
}

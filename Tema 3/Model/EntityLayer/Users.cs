using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_3.Model.EntityLayer
{
   
    public class Users:BasePropertyChange
    {
        private int? _id;
        public int? Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(nameof(Id)); }
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            set { _password = value; NotifyPropertyChanged(nameof(Password)); }
        }

        private string? _type;
        public string? Type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(nameof(Type)); }
        }

        private bool? _isDeleted;
        public bool? IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(nameof(IsDeleted)); }
        }
    }
}

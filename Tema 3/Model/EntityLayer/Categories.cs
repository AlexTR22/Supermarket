using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_3.Model.EntityLayer
{
    public class Categories:BasePropertyChange
    {
        private int? _id;
        public int? IdCategory
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(nameof(IdCategory)); }
        }

        private string? _catedory;
        public string? Category
        {
            get { return _catedory; }
            set { _catedory = value; NotifyPropertyChanged(nameof(Category)); }
        }

        private bool _isDeleted;
        public bool IsDeletedCategory
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(nameof(IsDeletedCategory)); }
        }
    }
}

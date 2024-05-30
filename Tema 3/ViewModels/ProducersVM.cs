using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Tema_3.Commands;
using Tema_3.Model.BusinessLogicLayer;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;
using Tema_3.Views;

namespace Tema_3.ViewModels
{
    public class ProducersVM:BasePropertyChange
    {
        ProducersBLL producersBLL= new ProducersBLL();
        
        public ObservableCollection<Producers> ProducersList
        {
            get => producersBLL.Producers; 
            set => producersBLL.Producers = value;
        } 

        public ProducersVM() 
        {
            ProducersList = producersBLL.GetAllProducers();
            Producer = new Producers();
        }

        private Producers _producer;
        public Producers Producer
        {
            get
            {
                return _producer;
            }
            set
            {
                _producer = value;
                NotifyPropertyChanged(nameof(Producer));
            }
        }






        private ICommand _addProducer;
        public ICommand AddProducer
        {
            get
            {
                if (_addProducer == null)
                {
                    _addProducer = new RelayCommand(AddProducerInDB);
                }
                return _addProducer;
            }
        }

        private ICommand _modifyProducer;
        public ICommand ModifyProducer
        {
            get
            {
                if (_modifyProducer == null)
                {
                    _modifyProducer = new RelayCommand(ModifyProducerInDB);
                }
                return _modifyProducer;
            }
        }

        private ICommand _deleteProducer;
        public ICommand DeleteProducer
        {
            get
            {
                if (_deleteProducer == null)
                {
                    _deleteProducer = new RelayCommand(DeleteProducerInDB);
                }
                return _deleteProducer;
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
                    if (window.Title == "ProducersWindow")
                    {
                        AdministratorPage administratorPage = new AdministratorPage();
                        administratorPage.Show();
                        window.Close();
                        break;
                    }
                }
            }
        }

        public void AddProducerInDB()
        {
            if (Producer.NameProducer != null && Producer.Country != null)
            {
                if (producersBLL.VerifyProducerExistanceInDB(Producer.NameProducer, Producer.Country) == 0)
                {
                    producersBLL.AddProducerInDB(Producer);
                }
                else
                {
                    MessageBox.Show("Producer already existent");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }

        private void ModifyProducerInDB()
        {
            if (Producer.NameProducer != null && Producer.Country != null)
            {
                if (producersBLL.VerifyProducerExistanceInDBWithId(Producer) == 0)
                {
                    producersBLL.ModifyProducerInDB(Producer);
                }
                else
                {
                    MessageBox.Show("Producer already existent");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Name or password input not found!");
                return;
            }
        }

        private void DeleteProducerInDB()
        {
            if (Producer.NameProducer != null && Producer.Country != null)
            {
                if (producersBLL.VerifyProducerExistanceInDB(Producer.NameProducer, Producer.Country) > 0)
                {
                    producersBLL.DeleteProducerInDB(Producer);
                }
                else
                {
                    MessageBox.Show("Producer already deleted");
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

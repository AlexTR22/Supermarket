using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;

namespace Tema_3.Model.BusinessLogicLayer
{
    public class ProducersBLL
    {
        public ObservableCollection<Producers> Producers { get; set; }

        ProducersDAL producersDAL=new ProducersDAL();
        public ProducersBLL()
        {
            Producers = new ObservableCollection<Producers>();
        }

        public ObservableCollection<Producers> GetAllProducers()
        {
            return producersDAL.GetAllProducers();
        }

        public int VerifyProducerExistanceInDB(string name, string country)
        {
            return producersDAL.VerifyProducerExistanceInDB(name, country);
        }


        public void AddProducerInDB(Producers producer)
        {
            producersDAL.AddProducerInDB(producer);
            Producers.Add(producer);
        }

        public void ModifyProducerInDB(Producers producer)
        {
            producersDAL.ModifyProducerInDB(producer);
        }

        public void DeleteProducerInDB(Producers producer)
        {
            producersDAL.DeleteProducerInDB(producer);
            producersDAL.DeleteProductsInCascadeByProducer(producer.IdProducer);
            //Users.Remove(user);
        }

        public int VerifyProducerExistanceInDBWithId(Producers producer)
        {
            return producersDAL.VerifyProducerExistanceInDBWithId(producer);
        }

        public int VerifyProducerExistanceInDB(string name)
        {
            return producersDAL.VerifyProducerExistanceInDB(name);
        }
    }
}

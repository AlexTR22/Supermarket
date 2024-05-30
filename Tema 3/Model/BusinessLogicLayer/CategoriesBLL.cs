using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_3.Model.DataAccessLayer;
using Tema_3.Model.EntityLayer;

namespace Tema_3.Model.BusinessLogicLayer
{
    class CategoriesBLL
    {
        CategoriesDAL categoriesDAL = new CategoriesDAL();

        public ObservableCollection<Categories> Categories { get; set; }

        public ObservableCollection<Categories> GetAllCategories()
        {
            return categoriesDAL.GetAllCategories();
        }

        public int VerifyCategoryExistanceInDB(string category)
        {
            return categoriesDAL.VerifyCategoryExistanceInDB(category);
        }

        public void AddCategoryInDB(Categories category)
        {
            categoriesDAL.AddCategoryInDB(category);
            Categories.Add(category);
        }

        public void ModifyCategoryInDB(Categories category)
        {
            categoriesDAL.ModifyCategoryInDB(category);
        }

        public void DeleteCategoryInDB(Categories category)
        {
            categoriesDAL.DeleteCategoryInDB(category);
            categoriesDAL.DeleteProductsInDBInCascadeByCategory(category.IdCategory);
            //Categories.Remove(category);
        }

        internal int VerifyCategoryExistanceInDBWithId(Categories category)
        {
            return categoriesDAL.VerifyCategoryExistanceInDBWithId(category);
        }
    }
}

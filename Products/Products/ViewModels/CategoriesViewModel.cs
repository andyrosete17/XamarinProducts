
namespace Products.ViewModels
{
    using Products.Models;
    using System;
    using System.Collections.ObjectModel;

    public class CategoriesViewModel
    {
        #region Properties
        /// <summary>
        /// Observable collection of categories
        /// Se usa para desplegables (ej: conversacines de whatsap, post de facebook, lista de telf)
        /// </summary>
        public ObservableCollection<Category> Categories { get; set; }
        #endregion 

        #region Constructors
        public CategoriesViewModel()
        {
            LoadCategories();
        }

        #endregion
        #region Private Methods
        private void LoadCategories()
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}

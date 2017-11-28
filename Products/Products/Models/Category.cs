
namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.ViewModels;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Services;

    public class Category
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; } 
        #endregion

        ///Para tener  el comando de las imagenes del categories view
        #region Commands
        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
        }

        #endregion

        #region Constructor
        public Category()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Private Methods
        async void SelectCategory()
        {
            ///Usando el singleton para pasar a productsviewmodel
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModel(Products);

            await navigationService.Navigate("ProductsView");
        } 
        #endregion



        ///No hce falta ahora
        /// <summary>
        /// Si se hace un override de ToString para la categoria se puede escribir el texto
        /// de la descripcíón
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    return Description; 
        //}
    }
}

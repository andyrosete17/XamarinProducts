
namespace Products.ViewModels
{
    using Products.Helpers;
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Xamarin.Forms;

    public class CategoriesViewModel :INotifyPropertyChanged
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Events
        /// <summary>
        /// Se usa INotifyPropertyChanged para refrescar pantalla y ante cambios entre, es como ajax de front
        /// Al final es el disparador de eventos a la pantalla
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        ObservableCollection<Category> _categories;
        #endregion


        #region Properties
        /// <summary>
        /// Observable collection of categories
        /// Se usa para desplegables (ej: conversacines de whatsap, post de facebook, lista de telf)
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Categories)));
                }
            }
        }
        #endregion 

        #region Constructors
        public CategoriesViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            LoadCategories();
        }

        #endregion
        #region Private Methods
        async void LoadCategories()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.isSuccess)
            {
                await dialogService.ShowMessage(Languages.Error, connection.Message);
                return;
            }

            ///Obteniendo mainViewModel por singleton
            var mainViewMode = MainViewModel.GetInstance();
            ///Llamando a la api, parametros de los archivos de recurso y formar la url http://productsapis.azurewebsites.net/api/Categories
            var response = await apiService.GetList<Category>(
                            Application.Current.Resources["URLAPI"].ToString(),
                            Application.Current.Resources["URLPREFIX"].ToString(),
                            Application.Current.Resources["CATEGORYCONTROLLER"].ToString(),
                            mainViewMode.Token.TokenType,
                            mainViewMode.Token.AccessToken);

            if (!response.isSuccess)
            {
                await dialogService.ShowMessage(Languages.Error, response.Message);
                return;
            }

            var categories = (List<Category>)response.Result;

        } 
        #endregion
    }
}

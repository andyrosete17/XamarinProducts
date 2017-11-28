
namespace Products.ViewModels
{
    using Products.Helpers;
    using Products.Models;
    using Products.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
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
        List<Category> categories;
        ObservableCollection<Category> _categoriesList;
        #endregion


        #region Properties
        /// <summary>
        /// Observable collection of categories
        /// Se usa para desplegables (ej: conversacines de whatsap, post de facebook, lista de telf)
        /// se usa esto porque se refresca automaticamente
        /// </summary>
        public ObservableCollection<Category> CategoriesList
        {
            get
            {
                return _categoriesList;
            }
            set
            {
                if (_categoriesList != value)
                {
                    _categoriesList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoriesList)));
                }
            }
        }
        
        #endregion 

        #region Constructors
        public CategoriesViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
            LoadCategories();
        }

        #endregion
        #region Methods
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

           categories = (List<Category>)response.Result;

            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));

        }

        public void AddCategory(Category category)
        {
            //Se usa esta variable para al agregarlo al categoriesList poder ordenarlo
            // se declara categories global como atributos 
            categories.Add(category);
            //Se refresca automaticamente porque es un observablecollection
            //por eso se usa eso en vez de una  lista
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
        }
        #endregion

        #region SingletonCategories
        public static CategoriesViewModel instance;

        public static CategoriesViewModel GetInstance ()
        {
            if(instance == null)
            {
                instance = new CategoriesViewModel();
            }
            return instance;
        }
        #endregion
    }
}

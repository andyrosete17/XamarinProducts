
namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Helpers;
    using Products.Services;
    using System.ComponentModel;
    using System.Windows.Input;
    using Models;
    using Xamarin.Forms;

    public class NewCategoryViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Constructor
        public NewCategoryViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnable = true;
        }
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnable;
        string _description;
        #endregion

        #region Properties
        /// <summary>
        /// Property to set the charger icon
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        /// <summary>
        /// Property to indicate if the login button is enable or not
        /// </summary>
        public bool IsEnable
        {
            get
            {
                return _isEnable;
            }
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnable)));
                }
            }
        }

        public string Description { get; set; }
        #endregion

        #region Command
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage(Languages.Error, Languages.NewCategoryError);
                return;
            }
            IsRunning = true;
            IsEnable = false;

            var connection = await apiService.CheckConnection();
            if (!connection.isSuccess)
            {
                IsRunning = false;
                IsEnable = true;
                await dialogService.ShowMessage(Languages.Error, connection.Message);
                return;
            }

            var category = new Category
            {
                Description = Description
            };

            var mainViewModel = MainViewModel.GetInstance();


            var response = await apiService.Post(
                Application.Current.Resources["URLAPI"].ToString(), 
                Application.Current.Resources["URLPREFIX"].ToString(),
                Application.Current.Resources["CATEGORYCONTROLLER"].ToString(),
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                category);

            if (!response.isSuccess)
            {
                IsRunning = false;
                IsEnable = true;
                await dialogService.ShowMessage(Languages.Error, response.Message);
                return;
            }

            ///Uso de singleton para acceder a la CategoriesViewModel
            category = (Category)response.Result;
            var categoriesViewModel = CategoriesViewModel.GetInstance();
            //Metodo addcategory para refrescar la categoría
            categoriesViewModel.AddCategory(category);
            await navigationService.Back();

            IsRunning = false;
            IsEnable = true;

        }
        #endregion
    }
}


namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Helpers;
    using Products.Models;
    using Products.Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NewProductViewModel : INotifyPropertyChanged
    {
        #region Services
        NavigationService navigationService;
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public NewProductViewModel(int categoryId)
        {
            navigationService = new NavigationService();
            apiService = new ApiService();
            dialogService = new DialogService();
            _categoryId = categoryId;
            IsEnable = true;
        }


        #endregion

        #region Attributes
        bool _isEnable;
        decimal _price;
        double _stock;
        string _description;
        bool _isActive;
        bool _isRunning;
        int _categoryId;
        #endregion

        #region Properties
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnable)));
                }
            }

        }
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
                }
            }

        }
        public double Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stock)));
                }
            }

        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));
                }
            }

        }

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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }

        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }

        }
        #endregion

        #region Command
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(SaveNewProduct);
            }
        }
        #endregion

        #region PrivateMethods
        private async void SaveNewProduct()
        {
            if (Price <= 0)
            {
                await dialogService.ShowMessage(Languages.Error, Languages.NewProductErrorEmptyPrice);
                return;
            }
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage(Languages.Error, Languages.NewProductErrorEmptyDescription);
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

            var product = new Product
            {
                Description = Description,
                Price = Price,
                IsActive = IsActive,
                Stock = Stock,
                CategoryId = _categoryId,
                LastPurchase = DateTime.Now,
            };

            var mainViewModel = MainViewModel.GetInstance();
            var response = await apiService.Post(
                Application.Current.Resources["URLAPI"].ToString(),
                Application.Current.Resources["URLPREFIX"].ToString(),
                Application.Current.Resources["PRODUCTCONTROLLER"].ToString(),
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                product
                );

            if (!response.isSuccess)
            {
                IsEnable = true;
                await dialogService.ShowMessage(Languages.Error, response.Message);
                IsRunning = false;
                IsEnable = true;
                return;
            }

            product = (Product)response.Result;
           
            var productViewModel = ProductsViewModel.GetInstance();
            productViewModel.AddProduct(product);
            await navigationService.Back();

            IsRunning = false;
            IsEnable = true;

        }


        #endregion

    }
}

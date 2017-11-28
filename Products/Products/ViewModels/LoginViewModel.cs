
namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Helpers;
    using Services;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : INotifyPropertyChanged
    {
       
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        string _email;
        string _password;
        bool _isToggle;
        bool _isRunning;
        bool _isEnable;
        #endregion

        #region Properties
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        /// <summary>
        /// Property to set the remember me option
        /// </summary>
        public bool IsToggle
        {
            get
            {
                return _isToggle;
            }
            set
            {
                if (_isToggle != value)
                {
                    _isToggle = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsToggle)));
                }
            }
        }

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
        #endregion


        #region Constructors
        public LoginViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            Email = "andyrosete17@gmail.com";
            Password = "123456";

            IsEnable = true;
            IsToggle = true;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        #endregion

        #region PrivateMethods
        async void Login()
        {
            if(string.IsNullOrWhiteSpace(Email))
            {
                await dialogService.ShowMessage(Languages.Error, Languages.NoEmail);
                return;
            }
             if (string.IsNullOrWhiteSpace(Password))
            {
                await dialogService.ShowMessage(Languages.Error, Languages.NoPassword);
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

            var response = await apiService.GetToken(Application.Current.Resources["URLAPI"].ToString(), Email, Password);

            if (response == null || string.IsNullOrEmpty(response.AccessToken))
            {
                IsRunning = false;
                IsEnable = true;
                await dialogService.ShowMessage(Languages.Error, response.ErrorDescription);
                Password = null;
                return;
            }

            //Singleton continues here
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Categories = new CategoriesViewModel();
            mainViewModel.Token = response; //keep the token to call the api methods

            ///Like this you call the categories view, you made a push in a pile indicating the new page to navigate
            ///La viewmodel no debe tener la vista, se debe aislar, se debe poner en un servicio
            await navigationService.Navigate("CategoriesView");

            Email = null;
            Password = null;
            IsRunning = false;
            IsEnable = true;
        }
        #endregion
    }


}

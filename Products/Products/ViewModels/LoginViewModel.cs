using System.ComponentModel;

namespace Products.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        #endregion Properties

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
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
            IsEnable = true;
            IsToggle = true;
           // Login = new LoginViewModel();
        }

        #endregion
    }


}

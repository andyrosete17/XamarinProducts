namespace Products.ViewModels
{
    using Models;
    public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public TokenResponse Token { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            ///3- instantiate the property it continues in loginviewmodel
            instance = this;
            Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        /// <summary>
        ///1- static property 
        /// </summary>
        static MainViewModel instance;
        /// <summary>
        ///2- initialize and set the instance property. Instantiate a new view model
        /// </summary>
        /// <returns></returns>
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}

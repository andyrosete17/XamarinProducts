namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Products.Services;
    using System.Windows.Input;

    public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public TokenResponse Token { get; set; }
        public ProductsViewModel Products { get; set; }
        public NewCategoryViewModel NewCategory { get; set; }
        #endregion

        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public MainViewModel()
        {
            ///3- instantiate the property it continues in loginviewmodel
            instance = this;
            Login = new LoginViewModel();
            navigationService = new NavigationService();
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

        

        #region Commands
        public ICommand NewCategoryCommand
            {
            get
            {
                return new RelayCommand(GoNewCategory);
            }
        }

        async void GoNewCategory()
        {
            //Siempre que se vaya a navegar hay que bindar la viewmodel
            //en este caso es directo porque estamos en la mainviewmodel
            //sino habría que hacer el singleton
            NewCategory = new NewCategoryViewModel();
            await navigationService.Navigate("NewCategoryView");
        }
        #endregion
    }
}

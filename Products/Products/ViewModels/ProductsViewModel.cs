
namespace Products.ViewModels
{
    using System.Collections.Generic;
    using Products.Models;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Products.Services;

    public class ProductsViewModel :INotifyPropertyChanged
    {
        #region Attributes
        private List<Product> products;
        public ObservableCollection<Product> _productList { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Service
        NavigationService navigationService;
        #endregion

        #region Properties
        public ObservableCollection<Product> ProductList
        {
            get
            {
                return _productList;
            }
            set
            {
                if (_productList != value)
                {
                    _productList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductList)));
                }
            }
        } 
       
        #endregion


        #region Constructor
        public ProductsViewModel(List<Product> products)
        {
            instance = this;
            this.products = products;
            navigationService = new NavigationService();
            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        }

        #endregion

        #region Singleton
        ///Singleton para que se actualice el new product
        public static ProductsViewModel instance;

        public static ProductsViewModel GetInstance()
        {
            if (instance== null)
            {
                instance = new ProductsViewModel(null);
            }
            return instance;
        }
        #endregion

        #region PublicMethod
        public void AddProduct(Product product)
        {
            //Se usa esta variable para al agregarlo al productList poder ordenarlo
            // se declara product global como atributos 
            products.Add(product);
            //Se refresca automaticamente porque es un observablecollection
            //por eso se usa eso en vez de una  lista
            ProductList = new ObservableCollection<Product>(products.OrderBy(c => c.Description));
        }
        #endregion



        public ICommand NewProductCommand
        {
            get
            {
                return new RelayCommand(GoNewProduct);
            }
        }
        async void GoNewProduct()
        {
            var mainViewModel = MainViewModel.GetInstance();
            var categoryId = ProductList[0].CategoryId;

            mainViewModel.NewProduct = new NewProductViewModel(categoryId);

            await navigationService.Navigate("NewProductView");

        }
    }
}

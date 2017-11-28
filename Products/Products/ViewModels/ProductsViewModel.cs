
namespace Products.ViewModels
{
    using System.Collections.Generic;
    using Products.Models;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    public class ProductsViewModel :INotifyPropertyChanged
    {
        #region Attributes
        private List<Product> products;
        public ObservableCollection<Product> _productList { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
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
            this.products = products;
            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        } 
        #endregion
    }
}

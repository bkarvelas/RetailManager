using Caliburn.Micro;
using System.ComponentModel;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;
        private BindingList<ProductModel> _products;
        private BindingList<ProductModel> _cart;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
        }

        // When this event triggers..
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            // ..we are loading the products
            await LoadProducts();
        }

        // This function loads the products asynchronous in the binding product list
        private async Task LoadProducts()
        {
            // Fetch the products with an async call to the API
            var productList = await _productEndpoint.GetAll();

            // Put the products in the Products list
            Products = new BindingList<ProductModel>(productList);
        }

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public BindingList<ProductModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public string SubTotal
        {
            // TODO: Replace with calculation
            get
            {
                return "$0.00";
            }
        }

        public string Tax
        {
            // TODO: Replace with calculation
            get
            {
                return "$0.00";
            }
        }

        public string Total
        {
            // TODO: Replace with calculation
            get
            {
                return "$0.00";
            }
        }

        //public bool CanAddToCart => (
        //    // TODO: Make sure something is selected
        //    // TODO: Make sure there is an item quantity

        //    );

        public void AddToCart()
        {

        }

        //public bool CanRemoveFromCart => (
        //    // TODO: Make sure something is selected
        //    );

        public void RemoveFromCart()
        {

        }

        //public bool CanCheckOut => (
        //    // TODO: Make sure is something in the cart
        //    );

        public void CheckOut()
        {

        }


    }
}

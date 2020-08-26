using Caliburn.Micro;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;

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

        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal
        {
            get
            {
                decimal subTotal = 0;

                foreach (var item in Cart)
                {
                    subTotal += (item.QuantityInCart * item.Product.RetailPrice);
                }
                return subTotal.ToString("C");
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

        // TODO: Make sure something is selected = OK!
        // TODO: Make sure there is an item quantity = OK!
        public bool CanAddToCart => (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity);

        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                Cart.Remove(existingItem);
                existingItem.QuantityInCart += ItemQuantity;
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };

                Cart.Add(item);
            }

            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);

        }

        //public bool CanRemoveFromCart => (
        //    // TODO: Make sure something is selected
        //    );

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        //public bool CanCheckOut => (
        //    // TODO: Make sure is something in the cart
        //    );

        public void CheckOut()
        {

        }


    }
}

using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<string> _products;

        public BindingList<string> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private string _itemQuantity;

        public string ItemQuantity
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

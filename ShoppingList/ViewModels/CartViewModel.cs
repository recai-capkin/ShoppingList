using ShoppingList.Helpers;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingList.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private const string CartFileName = "cart.json";
        public ObservableCollection<Product> Cart { get; set; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshListCommand { get; }

        public CartViewModel()
        {
            Cart = new ObservableCollection<Product>();
            DeleteCommand = new Command<Product>(OnDelete);
            RefreshListCommand = new Command(() => RefreshList());
            Task.Run(async () => await LoadCart());
        }
        private void RefreshList()
        {
            // This will force the ObservableCollection to notify the UI of changes
            var carts = new ObservableCollection<Product>(Cart);
            Cart = carts;
            OnPropertyChanged(nameof(Cart));
        }
        public void AddToCart(Product product)
        {

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Cart.Add(product);
            });
            Task.Run(async () => await SaveCart());
            RefreshList();
        }
        private void OnDelete(Product product)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Cart.Remove(product);
            });
            Task.Run(async () => await SaveCart());
        }
        private async Task LoadCart()
        {
            var cartItems = await FileHelper.ReadFromFileAsync<Product>(CartFileName);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (var item in cartItems)
                {
                    Cart.Add(item);
                }
            });
        }
        private async Task SaveCart()
        {
            await FileHelper.SaveToFileAsync(CartFileName, new List<Product>(Cart));
        }
    }
}

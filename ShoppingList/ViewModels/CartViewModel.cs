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

        public CartViewModel()
        {
            Cart = new ObservableCollection<Product>();
            DeleteCommand = new Command<Product>(OnDelete);
            Task.Run(async () => await LoadCart());
        }

        public void AddToCart(Product product)
        {
            Cart.Add(product);
            Task.Run(async () => await SaveCart());
        }
        private void OnDelete(Product product)
        {
            Cart.Remove(product);
            Task.Run(async () => await SaveCart());
        }
        private async Task LoadCart()
        {
            var cartItems = await FileHelper.ReadFromFileAsync<Product>(CartFileName);
            foreach (var item in cartItems)
            {
                Cart.Add(item);
            }
        }
        private async Task SaveCart()
        {
            await FileHelper.SaveToFileAsync(CartFileName, new List<Product>(Cart));
        }
    }
}

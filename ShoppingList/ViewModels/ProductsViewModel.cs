using ShoppingList.Helpers;
using ShoppingList.Models;
using ShoppingList.ViewModels;
using ShoppingList.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ShoppingList.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private const string FileName = "products.json";
        private CartViewModel _cartViewModel;

        public ObservableCollection<Product> Products { get; set; }
        public ICommand AddProductCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand OpenCartCommand { get; }

        public ProductsViewModel(CartViewModel cartViewModel)
        {
            Products = new ObservableCollection<Product>();
            _cartViewModel = new CartViewModel(); // Bu örnekte basit bir cart ViewModel oluşturduk
            AddProductCommand = new Command(async () => await OnAddProduct());
            DeleteCommand = new Command<Product>(async (product) => await OnDelete(product));
            AddToCartCommand = new Command<Product>((product) => OnAddToCart(product));
            OpenCartCommand = new Command(async () => await OnOpenCart());

            // Uygulama başladığında verileri yükle
            Task.Run(async () => await LoadProducts());
        }

        private async Task LoadProducts()
        {
            var products = await FileHelper.ReadFromFileAsync<Product>(FileName);
            foreach (var product in products)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Products.Add(product);
                });
            }
        }

        private async Task OnAddProduct()
        {
            // Ürün ekleme sayfasını aç
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage(this));
        }

        private async Task OnDelete(Product product)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Products.Remove(product);
            });
            await SaveProducts();
        }

        private void OnAddToCart(Product product)
        {
            _cartViewModel.AddToCart(product);
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var productsPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault() as ProductsPage;
                productsPage?.ShowToast($"{product.Name} sepete eklendi");
            });
        }

        public async Task SaveProducts()
        {
            await FileHelper.SaveToFileAsync(FileName, new List<Product>(Products));
        }
        private async Task OnOpenCart()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CartPage(_cartViewModel));
        }
    }
}

using ShoppingList.Models;
using ShoppingList.ViewModels;
using System.Collections.ObjectModel;

namespace ShoppingList.Views;

public partial class CartPage : ContentPage
{
    public ObservableCollection<Product> Cart { get; set; }

    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}
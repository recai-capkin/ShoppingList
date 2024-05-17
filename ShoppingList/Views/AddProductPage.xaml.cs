using ShoppingList.Models;
using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class AddProductPage : ContentPage
{
    private ProductsViewModel _viewModel;

    public AddProductPage(ProductsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    private async void OnAddProduct(object sender, EventArgs e)
    {
        var productName = ProductNameEntry.Text;
        if (!string.IsNullOrEmpty(productName))
        {
            _viewModel.Products.Add(new Product { Name = productName });
            await _viewModel.SaveProducts();
            await Navigation.PopAsync();
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}
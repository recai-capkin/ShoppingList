using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class ProductsPage : ContentPage
{
	public ProductsPage(CartViewModel cartViewModel)
	{
        InitializeComponent();
        BindingContext = new ProductsViewModel(cartViewModel);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        NavigationPage.SetHasNavigationBar(this, false);
    }
    public async void ShowToast(string message)
    {
        await ToastNotification.Show(message);
    }
}
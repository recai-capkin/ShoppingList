using Microsoft.Maui.Layouts;
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
        var toast = new ToastNotificationView();
        AbsoluteLayout.SetLayoutBounds(toast, new Rect(0.5, 0.9, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
        AbsoluteLayout.SetLayoutFlags(toast, AbsoluteLayoutFlags.PositionProportional);
        MainLayout.Children.Add(toast);  // MainLayout AbsoluteLayout bileşeninin x:Name'dir

        await toast.Show(message);

        // Toast mesajı gösterildikten sonra kaldır
        MainLayout.Children.Remove(toast);
    }
}
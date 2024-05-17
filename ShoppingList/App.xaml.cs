using ShoppingList.ViewModels;
using ShoppingList.Views;

namespace ShoppingList;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        var cartViewModel = new CartViewModel();
        MainPage = new NavigationPage(new ProductsPage(cartViewModel));
    }
}

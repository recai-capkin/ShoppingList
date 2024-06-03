namespace ShoppingList.Views;

public partial class ToastNotificationView : ContentView
{
	public ToastNotificationView()
	{
		InitializeComponent();
	}
    public async Task Show(string message, int duration = 2000)
    {
        MessageLabel.Text = message;
        this.IsVisible = true;
        await this.FadeTo(1, 250); // Fade in
        await Task.Delay(duration); // Wait for the specified duration
        await this.FadeTo(0, 250); // Fade out
        this.IsVisible = false;
    }

}
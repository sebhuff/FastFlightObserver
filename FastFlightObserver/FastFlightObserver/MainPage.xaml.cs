namespace FastFlightObserver;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnClick(object sender, EventArgs e)
	{
		drawable.time += 20;
		graphics_view.Invalidate();
    }
}


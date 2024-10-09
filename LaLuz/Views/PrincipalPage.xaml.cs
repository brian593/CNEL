using LaLuz.ViewModels;

namespace LaLuz.Views;

public partial class PrincipalPage : ContentPage
{
	public PrincipalPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext=viewModel;

	}
    private void Clicked(object sender, EventArgs e)
    {
        myCheckBox.IsChecked = !myCheckBox.IsChecked;
    }
}
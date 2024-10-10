using LaLuz.Services;
using LaLuz.Views;

namespace LaLuz;

public partial class App : Application
{
	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<PrincipalPage>();


    }
}

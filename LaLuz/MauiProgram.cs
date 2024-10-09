using CommunityToolkit.Maui;
using LaLuz.DataAccess;
using LaLuz.Services;
using LaLuz.ViewModels;
using LaLuz.Views;
using Microsoft.Extensions.Logging;

namespace LaLuz;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
#if DEBUG
		builder.Logging.AddDebug();
#endif

        var dbContext = new DWJDBContext();
        dbContext.Database.EnsureCreated();
        dbContext.Dispose();

        builder.Services.AddTransient<IApiServices, ApiServices>();
        builder.Services.AddDbContext<DWJDBContext>();

        // Registro del ViewModel
        builder.Services.AddTransient<MainPageViewModel>();
        // Registro de la página
        builder.Services.AddTransient<PrincipalPage>();


        return builder.Build();
	}
}

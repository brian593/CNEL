using CommunityToolkit.Maui;
using InputKit.Handlers;
using LaLuz.DataAccess;
using LaLuz.Services;
using LaLuz.ViewModels;
using LaLuz.Views;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace LaLuz;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddInputKitHandlers(); 
            })
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddMaterialIconFonts();

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

using Master_of_Emails.ViewModels;
using practice.Pages;

namespace Master_of_Emails;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("TimesNewRoman.otf", "Times");
			});

		builder.Services.AddSingleton<PriorityOneMafPage>();
		builder.Services.AddSingleton<PriorityOneMafPageViewModel>();
		return builder.Build();
	}
}

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
				fonts.AddFont("ArialBlack.ttf", "ArialBlack");
				fonts.AddFont("TimesNewRoman.otf", "Times");
			});

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<DatabasePage>();
        builder.Services.AddSingleton<DatabasePageViewModel>();

		builder.Services.AddSingleton<Settings>();
		builder.Services.AddSingleton<SettingsPageViewModel>();

        builder.Services.AddSingleton<PriorityOneMafPage>();
		builder.Services.AddSingleton<PriorityOneMafPageViewModel>();

		builder.Services.AddSingleton<InconAlertPage>();
		builder.Services.AddSingleton<InconAlertPageViewModel>();

        builder.Services.AddSingleton<ZfoPage>();
        builder.Services.AddSingleton<ZfoPageViewModel>();

        builder.Services.AddSingleton<DuressAlarmPage>();
        builder.Services.AddSingleton<DuressAlarmPageViewModel>();

        builder.Services.AddSingleton<ScadaPage>();
        builder.Services.AddSingleton<ScadaPageViewModel>();

        builder.Services.AddSingleton<FiberAlertPage>();
        builder.Services.AddSingleton<FiberAlertPageViewModel>();

		return builder.Build();
	}
}

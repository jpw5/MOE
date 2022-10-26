using Master_of_Emails.ViewModels;

namespace practice.Pages;

public partial class Settings : ContentPage
{
    public Settings(SettingsPageViewModel settingsPageViewModel)
    {
        InitializeComponent();
        BindingContext = settingsPageViewModel;
    }
}
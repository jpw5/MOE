using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Master_of_Emails.ViewModels;

public partial class SettingsPageViewModel : ObservableObject
{

    public SettingsPageViewModel()
    {

    }

    [RelayCommand]
    public void DarkTheme()
    {
        Application.Current.UserAppTheme = AppTheme.Dark;
    }

    [RelayCommand]
    public void LightTheme()
    {
        Application.Current.UserAppTheme = AppTheme.Light;
    }

}


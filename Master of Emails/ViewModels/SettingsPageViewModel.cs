using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.Themes;
using System.Collections.ObjectModel;

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


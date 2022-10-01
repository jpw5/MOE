using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using Master_of_Emails;
using SQLite;

namespace Master_of_Emails;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        BindingContext = mainPageViewModel;
    }

}


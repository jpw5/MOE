using Master_of_Emails;
using Master_of_Emails.Database;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;


namespace practice.Pages;

public partial class DatabasePage : ContentPage
{
    public DB DB;
    public DatabasePage(DatabasePageViewModel databasePageViewModel)
	{
		InitializeComponent();
        BindingContext = databasePageViewModel;
        if (DB.DatabaseConnection == null)
            DB.DatabaseInit();
    }
}

/* Unmerged change from project 'Master of Emails (net6.0-maccatalyst)'
Before:
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using Master_of_Emails;
After:
using Master_of_Emails;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
*/

/* Unmerged change from project 'Master of Emails (net6.0-windows10.0.19041.0)'
Before:
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using Master_of_Emails;
After:
using Master_of_Emails;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
*/

/* Unmerged change from project 'Master of Emails (net6.0-ios)'
Before:
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using Master_of_Emails;
After:
using Master_of_Emails;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
*/
using Master_of_Emails.ViewModels;

namespace Master_of_Emails;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        BindingContext = mainPageViewModel;
    }

}


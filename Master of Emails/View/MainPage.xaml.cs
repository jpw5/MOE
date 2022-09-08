using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;

namespace Master_of_Emails;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        BindingContext = mainPageViewModel;
    }

    void person_search(object sender, EventArgs e)
    {
        //person_search_result_phone_label.Text = person_search_result_phone_label.Text + person_search_bar.Text;

    }

    void organization_search(object sender, EventArgs e)
    {
        //organization_search_result_phone_label.Text = organization_search_result_phone_label.Text + organization_search_bar.Text;

    }



}


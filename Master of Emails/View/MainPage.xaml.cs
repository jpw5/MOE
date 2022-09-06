using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;

namespace Master_of_Emails;

public partial class MainPage : ContentPage
{
    public TollPlazaRepository TollPlazaRepo = new();
    TableQuery<TollPlaza> tollPlazas;

    public string StatusMessage;
    public MainPage()
    {
        InitializeComponent();
    }

    public string Text;

    void Plaza_search(object sender, EventArgs e)
    {

        try
        {

            tollPlazas = TollPlazaRepo.PlazaQuery(Int32.Parse(plaza_search_bar.Text));
            
            if (!tollPlazas.Any())
            {
                DisplayAlert("Failed to Retrive", "Please enter a valid Plaza ID Number Or The Plaza ID does not exist. " + StatusMessage, "Close");
            }

            else
            {
                foreach (TollPlaza plaza in tollPlazas)
                {
                    plaza_search_result_phone_label.Text = "Phone: " + plaza.Plaza_phone_number;
                    plaza_search_result_name_label.Text = "Plaza: " + plaza.Plaza_name + " " + plaza.Plaza_roadway + " Mile Post " + plaza.Plaza_milepost + " " + plaza.Plaza_region;
                }
            }
           

        }

        catch
        {
            DisplayAlert("Failed to Retrive", "Please enter a valid Plaza ID Number Or The Plaza ID does not exist. " + StatusMessage, "Close");
        }
       
    }

    void person_search(object sender, EventArgs e)
    {
        //person_search_result_phone_label.Text = person_search_result_phone_label.Text + person_search_bar.Text;

    }

    void organization_search(object sender, EventArgs e)
    {
        //organization_search_result_phone_label.Text = organization_search_result_phone_label.Text + organization_search_bar.Text;

    }

    private void OnPriorityOneMafClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("PriorityOneMafPage");
    }

    private void OnInconAlertClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("InconAlertPage");
    }

    private void OnZfoClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("ZfoPage");
    }

    private void OnDuressAlarmClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("DuressAlarmPage");
    }

    private void OnScadaClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("ScadaPage");
    }

    private void OnFiberAlertClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("FiberAlertPage");
    }

}


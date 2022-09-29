using Google.Apis.Compute.v1.Data;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class ZfoPage : ContentPage
{
    public Outlook.Application objApp = new();
    public Outlook.MailItem mail = null;
    public string Template = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");

    public TollPlazaRepository TollPlazaRepo = new();
    public TollLaneRepository TollLaneRepo = new();
    public TollTechnicianRepository TollTechnicianRepo = new();
    public TollBomitemRepository TollBomitemRepo = new();

    public TableQuery<TollLane> tollLanesQueryByPlazaId;
    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;
    public TableQuery<TollTechnician> tollTechnicianQueryByRegion;
    public TableQuery<TollBomitem> tollBomitemQueryByLaneType;

    public List<string> TollLane = new();
    public List<string> TollLaneList = new();

    public string Region;
    public int PlazaId;

    public string Plaza;
    public string Lane;
    public string Requestor;
    public string Reason;
    public string StartDate;
    public string EndDate;

    public ZfoPage(ZfoPageViewModel zfoPageViewModel)
	{
        InitializeComponent();
        BindingContext=zfoPageViewModel;
    }

    private void ZFOEmail_Button_Pressed(object sender, EventArgs e)
    {

        if (selectPlaza.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose a Plaza", "Close");
            return;
        }

        else if (!TollLaneList.Any())
        {
            DisplayAlert("Alert", "Choose Lane(s)", "Close");
            return;
        }

        else if(string.IsNullOrEmpty(selectRequestor.Text))
        {
            DisplayAlert("Alert", "Enter Requestor", "Close");
            return;
        }

        else if (selectReason.Text == null)
        {
            DisplayAlert("Alert", "Enter Reason", "Close");
            return;
        }

        Plaza = (string)selectPlaza.SelectedItem;
        for (int i = 0; i < TollLaneList.Count; i++)
        {
            Lane += TollLaneList[i] + " ";
        }
        TollLaneList.Clear();
        Requestor = selectRequestor.Text;
        Reason = selectReason.Text;
        StartDate = selectStartDate.Text;
        EndDate = selectEndDate.Text;

        try
        {
            string To = "ali.shakoor2249@gmail.com";
            string Subject = "SunWatch ZFO Alert - " + Plaza.ToUpper() + " / " + Lane.ToUpper();

            string Body = "<font size=5>" + "<b>" + "****SunWatch ZFO Alert****" + "</b>" + "</font>" + "<br>" + "<br>" +
            "<font size=4>" + "<b>" + "Plaza: " + "</b>" + Plaza + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Lane(s): " + "</b>" + Lane + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Requestor: " + "</b>" + Requestor + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Reason: " + "</b>" + Reason + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Start Date/Time: " + "</b>" + StartDate + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "End Date/Time: " + "</b>" + EndDate + "</font>" + "<br>";

            mail = (Outlook.MailItem)objApp.CreateItemFromTemplate(Template);
            mail.To = To;
            mail.Subject = Subject;
            mail.HTMLBody = Body;
            mail.Display();
            mail = null;
            Lane = null;
        }

        catch(Exception ex)
        {
            DisplayAlert("Alert", "Close MOE, make sure Outlook is running, and try again. " + ex.Message, "close");
        }
      
    }
    private void SelectRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectRegion.SelectedIndex;

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            Region = selectRegion.Items[selectedIndex];

            tollPlazaQueryByRegionName = TollPlazaRepo.QueryByRegionName(Region);
            foreach (TollPlaza tollPlaza in tollPlazaQueryByRegionName)
            {
                selectPlaza.ItemsSource.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name + " " + tollPlaza.Plaza_roadway
                + " MP " + tollPlaza.Plaza_milepost);
            }
        }
    }
    private void SelectPlaza_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectPlaza.SelectedIndex;

        if (selectedIndex != -1)
        {
            selectLane.ItemsSource = null;
            TollLane.Clear();
            var Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            PlazaId = Int32.Parse(Split[0]);
            tollLanesQueryByPlazaId = TollLaneRepo.QueryByPlazaId(PlazaId);
            foreach (TollLane tollLane in tollLanesQueryByPlazaId)
            {
                TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_Type);
            }
            selectLane.ItemsSource = TollLane;
        }
    }
    private void SelectLane_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //DisplayAlert("Check",sender.ToString(),"Close");

        if (e.CurrentSelection.Count == 0)
            return;

        else
        {
            TollLaneList.Clear();
            for (int i = 0; i < e.CurrentSelection.Count; i++)
            {
                TollLaneList.Add(e.CurrentSelection[i].ToString());
                //DisplayAlert("Check", TollLane[i], "Close");
            }
        }
    }
}



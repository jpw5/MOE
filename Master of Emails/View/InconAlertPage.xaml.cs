using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class InconAlertPage : ContentPage
{
    public Outlook.Application objApp = new();
    public Outlook.MailItem mail = null;
    public string Template = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");

    public TollPlazaRepository TollPlazaRepo = new();
    public TollLaneRepository TollLaneRepo = new();
    public TollTechnicianRepository TollTechnicianRepo = new();
    public TollBomitemRepository TollBomitemRepo = new();
    public TollEmailDistributionRepository TollEmailDistributionRepo = new();

    public TableQuery<TollLane> tollLanesQueryByPlazaId;
    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;
    public TableQuery<TollTechnician> tollTechnicianQueryByRegion;
    public TableQuery<TollBomitem> tollBomitemQueryByLaneType;
    public TableQuery<TollEmailDistribution> StandardDistributionIncon;


    public List<string> TollLane = new();
    public List<string> TollLaneList = new();

    public string Region;
    public int PlazaId;

    public string Plaza;
    public string Roadway;
    public string Lane;
    public string Date;
    public string Requestor;
    public string RequestorPhoneNumber;
    public string Duration;
    public string Units;
    public string IncidentOrESR;
    public string Reason;

    public string EmailType = "Incon";
    public string To;
    public string Cc;
    public string Subject;
    public string Body;

    public InconAlertPage(InconAlertPageViewModel inconAlertPageViewModel)
    {
        InitializeComponent();
        BindingContext = inconAlertPageViewModel;
    }

    private void InconAlertEmail_Button_Pressed(object sender, EventArgs e)
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

        else if (string.IsNullOrEmpty(selectRequestor.Text))
        {
            DisplayAlert("Alert", "Enter Requestor", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectPhoneNumber.Text))
        {
            DisplayAlert("Alert", "Enter Requestor Phone Number", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectDuration.Text))
        {
            DisplayAlert("Alert", "Enter Duration", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(Units))
        {
            DisplayAlert("Alert", "Choose Unit", "Close");
            return;
        }

        else if (selectReason.Text == null)
        {
            DisplayAlert("Alert", "Enter Reason", "Close");
            return;
        }

        Plaza = (string)selectPlaza.SelectedItem;
        var Split = Plaza.Split(" ", 2);
        PlazaId = Int32.Parse(Split[0]);
        tollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(PlazaId);
        foreach (TollPlaza plaza in tollPlazaQueryByPlazaId)
        {
            Roadway = plaza.Plaza_roadway;
        }

        for (int i = 0; i < TollLaneList.Count; i++)
        {
            Lane += TollLaneList[i] + " ";
        }

        TollLaneList.Clear();
        Region = selectRegion.SelectedItem.ToString();
        Date = selectDate.Text;
        Requestor = selectRequestor.Text;
        RequestorPhoneNumber = selectPhoneNumber.Text;
        Duration = selectDuration.Text;
        Reason = selectReason.Text;

        To = "";
        Cc = "";
        StandardDistributionIncon =
        TollEmailDistributionRepo.QueryByRegionEmailTypeAndPlazaId(Region, EmailType, "ALL");

        foreach (TollEmailDistribution standarddistributionIncon in StandardDistributionIncon)
        {
            To = standarddistributionIncon.Email_distribution_to;
            Cc = standarddistributionIncon.Email_distribution_cc;
        }

        string Subject = "InConAlert for Plaza - " + Plaza.ToUpper() + " / " + Lane.ToUpper();
        string Body = "<font size=5>" + "<b>" + "****SunWatch InConAlert****" + "</b>" + "</font>" + "<br>" + "<br>" +
        "<font size=4>" + "<b>" + "Plaza: " + "</b>" + Plaza + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Roadway: " + "</b>" + Roadway + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Lane: " + "</b>" + Lane + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Date/Time Contacted: " + "</b>" + Date + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Requestor: " + "</b>" + Requestor + " / " + RequestorPhoneNumber + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Duration of Work: " + "</b>" + Duration +" "+ Units + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Reason: " + "</b>" + Reason + "</font>" + "<br>";

        try
        {
            mail = (Outlook.MailItem)objApp.CreateItemFromTemplate(Template);
            mail.To = To;
            mail.CC = Cc;
            mail.Subject = Subject;
            mail.HTMLBody = Body;
            mail.Display();
            mail = null;
            Lane = null;
        }

        catch (Exception ex)
        {
            DisplayAlert("Alert", "Close MOE, make sure Outlook is running, and try again. " + ex.Message, "close");
        }
    }
    private void SelectRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectRegion.SelectedIndex;
        List<string> plazas = new();

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            Region = selectRegion.Items[selectedIndex];
            plazas.Clear();
            tollPlazaQueryByRegionName = TollPlazaRepo.QueryByRegionName(Region);
            foreach (TollPlaza tollPlaza in tollPlazaQueryByRegionName)
            {
                plazas.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name);
            }

            plazas.Sort();
            foreach (string tollPlaza in plazas)
            {
                selectPlaza.ItemsSource.Add(tollPlaza);
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
            TollLane.Sort();
            selectLane.ItemsSource = TollLane;
        }
    }
    private void SelectLane_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
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
    private void HoursRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Units = "Hours";
    }
    private void MinuetsRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Units = "Minuets";
    }
}
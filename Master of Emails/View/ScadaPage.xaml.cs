using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace practice.Pages;

public partial class ScadaPage : ContentPage
{
    public Outlook.Application objApp = new();
    public Outlook.MailItem mail = null;
    public string Template = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");

    public TollPlazaRepository TollPlazaRepo = new();
    public TollLaneRepository TollLaneRepo = new();
    public TollEmailDistributionRepository TollEmailDistributionRepo = new();

    public TableQuery<TollLane> tollLanesQueryByPlazaId;
    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;
    public TableQuery<TollEmailDistribution> StandardDistributionScadaAll;
    public TableQuery<TollEmailDistribution> StandardDistributionScadaInfinity;

    public string Region;
    public int PlazaId;

    public string Plaza;
    public string PlazaCompany;
    public string Roadway;
    public string SelectedHours;
    public string BuildingNumber;
    public string Alarm;
    public string Date;
    public string MilePost;
    public string WorkOrderNumber;
    public string Temperature;
    public string FacilitiesContact;
    public string FacilitiesContactPhone;

    public string EmailType = "SCADA";
    public string To;
    public string Cc;
    public string Subject;
    public string Body;

    public ScadaPage(ScadaPageViewModel scadaPageViewModel)
	{
		InitializeComponent();
        BindingContext = scadaPageViewModel;
	}
    private void ScadaEmailButton_Pressed(object sender, EventArgs e)
    {
        if (selectPlaza.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose a Plaza", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(SelectedHours))
        {
            DisplayAlert("Alert", "Choose Hours", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectContact.Text))
        {
            DisplayAlert("Alert", "Enter Contact", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectPhoneNumber.Text))
        {
            DisplayAlert("Alert", "Enter Phone Number", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectScadaAlarm.Text))
        {
            DisplayAlert("Alert", "Enter SCADA Alarm", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectBuildingNumber.Text))
        {
            DisplayAlert("Alert", "Enter Building Alarm", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectWorkOrderNumber.Text))
        {
            DisplayAlert("Alert", "Enter Work Order Number", "Close");
            return;
        }

        Region = selectRegion.SelectedItem.ToString();
        var Split = selectPlaza.SelectedItem.ToString().Split(" ", 2);
        PlazaId = Int32.Parse(Split[0]);
        tollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(PlazaId);
        foreach (TollPlaza plaza in tollPlazaQueryByPlazaId)
        {
            Plaza = plaza.Plaza_id + " " + plaza.Plaza_name;
            Roadway = plaza.Plaza_roadway;
            MilePost = plaza.Plaza_milepost.ToString();
            PlazaCompany = plaza.Plaza_company;
        }

        BuildingNumber=selectBuildingNumber.Text;
        Alarm = selectScadaAlarm.Text;
        WorkOrderNumber = selectWorkOrderNumber.Text;
        Date = selectDate.Text;
        Temperature = selectTemperature.Text;
        FacilitiesContact = selectContact.Text;
        FacilitiesContactPhone = selectPhoneNumber.Text;

        if(PlazaCompany=="Infinity")
        {
            StandardDistributionScadaInfinity =
            TollEmailDistributionRepo.QueryByRegionEmailTypeAndPlazaId(Region, EmailType, PlazaCompany);

            foreach (TollEmailDistribution emaildistributionSCADA in StandardDistributionScadaInfinity)
            {
                To = emaildistributionSCADA.Email_distribution_to;
                Cc = emaildistributionSCADA.Email_distribution_cc;
            }
        }

        else
        {
            StandardDistributionScadaAll =
            TollEmailDistributionRepo.QueryByRegionEmailTypeAndPlazaId(Region, EmailType, "ALL");

            foreach (TollEmailDistribution emaildistributionSCADA in StandardDistributionScadaAll)
            {
                To = emaildistributionSCADA.Email_distribution_to;
                Cc = emaildistributionSCADA.Email_distribution_cc;
            }
        }
   
        Subject = "SCADA Alarm - " + Plaza.ToUpper();
        Body = "<font size=5>" + "<b>" + "****SunWatch SCADA Alarm - " + SelectedHours + "*****" + "</b>" + "</font>" + "<br>" + "<br>" +
        "<font size=4>" + "<b>" + "Plaza: " + "</b>" + Plaza + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Roadway: " + "</b>" + Roadway + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Building Number: " + "</b>" + BuildingNumber + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Alarm: " + "</b>" + Alarm + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Contact: " + "</b>" + FacilitiesContact + " / " + FacilitiesContactPhone + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Date/Time Contacted: " + "</b>" + Date + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Mile Post: " + "</b>" + MilePost + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Work Order #: " + "</b>" + WorkOrderNumber + "</font>" + "<br>";

        try
        {
            mail = (Outlook.MailItem)objApp.CreateItemFromTemplate(Template);
            mail.To = To;
            mail.CC = Cc;
            mail.Subject = Subject;
            mail.HTMLBody = Body;
            mail.Display();
            mail = null;
        }

        catch(Exception ex)
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
                plazas.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name + " " + tollPlaza.Plaza_roadway + " MP " +
                tollPlaza.Plaza_milepost);
            }

            plazas.Sort();
            foreach (string tollPlaza in plazas)
            {
                selectPlaza.ItemsSource.Add(tollPlaza);
            }
        }

    }
    private void After_hours_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SelectedHours = "After Hours";
        selectWorkOrderNumber.Text = "";
    }

    private void Normal_hours_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SelectedHours = "Normal Hours";
        selectWorkOrderNumber.Text="NA";
    }

}
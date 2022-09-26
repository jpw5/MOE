using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class DuressAlarmPage : ContentPage
{
    public Outlook.Application objApp = new();
    public Outlook.MailItem mail = null;
    public string Template = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");

    public TollPlazaRepository TollPlazaRepo = new();
    public TollLaneRepository TollLaneRepo = new();
    
    public TableQuery<TollLane> tollLanesQueryByPlazaId;
    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;
  
    public string Region;
    public int PlazaId;

    public string Plaza;
    public string Roadway;
    public string Lane;
    public string Alarm;
    public string PlazaSupervisor;
    public string DuressReason;
    public string Date;

    public DuressAlarmPage(DuressAlarmPageViewModel duressAlarmPageViewModel)
	{
        InitializeComponent();
        BindingContext = duressAlarmPageViewModel;
    }

    private void DuressAlarmEmailButton_Pressed(object sender, EventArgs e)
    {
        if (selectPlaza.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose a Plaza", "Close");
            return;
        }

        else if (selectLane.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose Lane", "Close");
            return;
        }

        else if (selectDuressReason.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose Alarm Reason", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(Alarm))
        {
            DisplayAlert("Alert", "Choose Alarm", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectPlazaSupervisor.Text))
        {
            DisplayAlert("Alert", "Enter Plaza Supervisor", "Close");
            return;
        }

        Region = selectRegion.SelectedItem.ToString();
        var Split = selectPlaza.SelectedItem.ToString().Split(" ", 2);
        PlazaId = Int32.Parse(Split[0]);
        tollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(PlazaId);
        foreach (TollPlaza plaza in tollPlazaQueryByPlazaId)
        {
            Plaza = plaza.Plaza_id.ToString() + " " + plaza.Plaza_name;
            Roadway = plaza.Plaza_roadway;
        }

        Lane = selectLane.SelectedItem.ToString();
        DuressReason=selectDuressReason.SelectedItem.ToString();
        Date = selectDate.Text;
        PlazaSupervisor= selectPlazaSupervisor.Text;
        DuressReason = selectDuressReason.SelectedItem.ToString();

        string To = "ali.shakoor2249@gmail.com";
        string Subject = "Duress Alarm at " + Plaza.ToUpper() + " / " + Lane.ToUpper();
        
        string Body = "<font size=5>" + "<b>" + "****SunWatch Duress Alarm****" + "</b>" + "</font>" + "<br>" + "<br>" +
        "<font size=4>" + "<b>" + "Plaza: " + "</b>" + Plaza + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Roadway: " + "</b>" + Roadway + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Lane(s): " + "</b>" + Lane + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Date/Time: " + "</b>" + Date + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Alarm: " + "</b>" + Alarm + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Supervisor: " + "</b>" + PlazaSupervisor + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Reason: " + "</b>" + DuressReason + "</font>" + "<br>";

        mail = (Outlook.MailItem)objApp.CreateItemFromTemplate(Template);
        mail.To = To;
        mail.Subject = Subject;
        mail.HTMLBody = Body;
        mail.Display();
        mail = null;
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
                selectPlaza.ItemsSource.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name + " " + tollPlaza.Plaza_roadway + " MP " +
                tollPlaza.Plaza_milepost);
            }
        }
    }
    private void SelectPlaza_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectPlaza.SelectedIndex;

        if (selectedIndex != -1)
        {
            selectLane.ItemsSource.Clear();
            var Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            PlazaId = Int32.Parse(Split[0]);
            tollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(PlazaId);
            foreach (TollPlaza plaza in tollPlazaQueryByPlazaId)
            {
                Plaza = plaza.Plaza_id.ToString() + " " + plaza.Plaza_name;
            }

            tollLanesQueryByPlazaId = TollLaneRepo.QueryByPlazaId(PlazaId);
            foreach (TollLane tollLane in tollLanesQueryByPlazaId)
            {
                selectLane.ItemsSource.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_Type);
            }

            //DisplayAlert("Check", Plaza + " "+Roadway, "Close");
        }
    }
    private void VaultRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Alarm = "Vault Under Duress";
    }

    private void LaneRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Alarm = "Lane Duress Alarm";
    }
}
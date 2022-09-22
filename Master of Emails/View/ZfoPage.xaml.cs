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

        string To = "ali.shakoor2249@gmail.com";
        string Subject = "SunWatch ZFO Alert - " + Plaza.ToUpper() + " / " + Lane.ToUpper();
        string Body = "****SunWatch ZFO Alert****" + "<br>" + "<br>" +
        "Plaza: " + Plaza + "<br>" + "Lane(s): " + Lane + "<br>" + "Requestor: " + Requestor + "<br>"+
        "Reason: " + Reason + "<br>" + "Start Date/Time: " + StartDate + "<br>" + "End Date/Time: " + EndDate;

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



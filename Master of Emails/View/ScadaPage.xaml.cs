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
    public TollTechnicianRepository TollTechnicianRepo = new();
    public TollBomitemRepository TollBomitemRepo = new();

    public TableQuery<TollLane> tollLanesQueryByPlazaId;
    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;
    public TableQuery<TollTechnician> tollTechnicianQueryByRegion;
    public TableQuery<TollBomitem> tollBomitemQueryByLaneType;

    public string Region;
    public int PlazaId;

    public string Plaza;
    public string Roadway;

    public string SelectedHours;
   

    public string Date;
    public ScadaPage(ScadaPageViewModel scadaPageViewModel)
	{
		InitializeComponent();
        BindingContext = scadaPageViewModel;
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

    private void After_hours_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SelectedHours = "After Hours";
    }

    private void Normal_hours_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SelectedHours = "Normal Hours";
    }

    private void ScadaEmailButton_Pressed(object sender, EventArgs e)
    {

    }
}
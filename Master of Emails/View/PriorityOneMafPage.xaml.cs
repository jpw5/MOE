using Master_of_Emails.Database;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using practice.Pages;
using SQLite;
using System.Collections;
using System.Linq;

namespace practice.Pages;


public partial class PriorityOneMafPage : ContentPage
{
    public TollPlazaRepository TollPlazaRepo = new();
    public TollLaneRepository TollLaneRepo = new();

    public List<TollLane> TollLaneList = new();
    public TableQuery<TollLane> tollLanesQueryByPlazaId;

    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;

    public string Region;
    public int PlazaId;
    public String Plaza;
    public String Roadway;
    public String Lane;
    public String[] Split;

    public PriorityOneMafPage(PriorityOneMafPageViewModel priorityOneMafPageViewModel)
    {
        InitializeComponent();
        BindingContext = priorityOneMafPageViewModel;
    }

    private void SelectRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectRegion.SelectedIndex;

        if(selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            Region = selectRegion.Items[selectedIndex];
            tollPlazaQueryByRegionName=TollPlazaRepo.QueryByRegionName(Region);

            foreach(TollPlaza tollPlaza in tollPlazaQueryByRegionName)
            {
                selectPlaza.ItemsSource.Add(tollPlaza.Plaza_id+" "+tollPlaza.Plaza_name+" "+tollPlaza.Plaza_roadway+" MP "+tollPlaza.Plaza_milepost);
            }
            
        }

    }
    private void SelectPlaza_SelectedIndexChanged(object sender, EventArgs e)
    {   
        int selectedIndex = selectPlaza.SelectedIndex;

        if (selectedIndex != -1)
        {
            selectLane.ItemsSource.Clear();
            Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            PlazaId = Int32.Parse(Split[0]);
            tollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(PlazaId);
            foreach (TollPlaza plaza in tollPlazaQueryByPlazaId)
            {
                Plaza = plaza.Plaza_name;
                Roadway = plaza.Plaza_roadway;
                
            }

            tollLanesQueryByPlazaId = TollLaneRepo.QueryByPlazaId(PlazaId);

                foreach (TollLane tollLane in tollLanesQueryByPlazaId)
                {
                    selectLane.ItemsSource.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_Type);
                }

            //DisplayAlert("Check", Plaza + " "+Roadway, "Close");
        } 

    }

    private void SelectLane_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectLane.SelectedIndex;

        if (selectedIndex != -1)
        {
            Lane = selectLane.SelectedItem.ToString();
        }

        //DisplayAlert("Check", Lane, "Close");

    }

   
}

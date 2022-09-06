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
    public TableQuery<TollLane> tollLanes;
    public TableQuery<TollPlaza> tollPlazas;

    public int Plaza_id;
    public String Plaza;
    public String Roadway;
    public String Lane;
    public String[] Split;

    public PriorityOneMafPage(PriorityOneMafPageViewModel PriorityOneMafPageViewModel)
    {
        InitializeComponent();
        BindingContext = PriorityOneMafPageViewModel;
    }

    private void selectPlaza_SelectedIndexChanged(object sender, EventArgs e)
    {   
            
        int selectedIndex = selectPlaza.SelectedIndex;

        if (selectedIndex != -1)
        {
            selectLane.ItemsSource.Clear();
            Plaza = selectPlaza.Items[selectedIndex];
            Split = Plaza.Split(" ", 2);
            Plaza_id = Int32.Parse(Split[0]);
            tollLanes = TollLaneRepo.LaneQuery(Plaza_id);

                foreach (TollLane tollLane in tollLanes)
                {
                    selectLane.ItemsSource.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_Type);
                }

            tollPlazas = TollPlazaRepo.PlazaQuery(Plaza_id);
            foreach (TollPlaza plaza in tollPlazas)
            {
                Roadway = plaza.Plaza_roadway;
            }

            DisplayAlert("Check", Plaza + " "+Roadway, "Close");
        } 

    }

    private void selectLane_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectLane.SelectedIndex;

        if (selectedIndex != -1)
        {
            Lane = selectLane.SelectedItem.ToString();
        }

        DisplayAlert("Check", Lane, "Close");

    }
}

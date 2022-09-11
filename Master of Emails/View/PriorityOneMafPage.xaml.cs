using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;

namespace practice.Pages;

public partial class PriorityOneMafPage : ContentPage
{
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
    public string Lane;
    public string Bomitem;
    public string Problem;
    public string MAF;
    public string ActionTake;

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
            selectTechnician.ItemsSource.Clear();
            Region = selectRegion.Items[selectedIndex];

            tollPlazaQueryByRegionName=TollPlazaRepo.QueryByRegionName(Region);
            foreach(TollPlaza tollPlaza in tollPlazaQueryByRegionName)
            {
                selectPlaza.ItemsSource.Add(tollPlaza.Plaza_id+" "+tollPlaza.Plaza_name+" "+tollPlaza.Plaza_roadway+" MP "+tollPlaza.Plaza_milepost);
            }

            tollTechnicianQueryByRegion = TollTechnicianRepo.QueryTechnicianByRegion(Region);
            foreach(TollTechnician tollTechnician in tollTechnicianQueryByRegion)
            {
                selectTechnician.ItemsSource.Add(tollTechnician.Technician_name);
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
        selectBomitem.ItemsSource.Clear();
        int selectedIndex = selectLane.SelectedIndex;

        if (selectedIndex != -1)
        {
            Lane = selectLane.SelectedItem.ToString();
            var Split = selectLane.Items[selectedIndex].Split(" ", 2);
            Bomitem = (Split[1]);
        }

        tollBomitemQueryByLaneType= TollBomitemRepo.QueryByLaneType(Bomitem);
        foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
        {
            selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
        }

        //DisplayAlert("Check", Bomitem, "Close");

    }


}

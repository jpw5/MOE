using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class PriorityOneMafPage : ContentPage
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
    public string Lane;
    public string LaneType;
    public string Bomitem;
    public string Problem;
    public string ActionTaken;
    public string Technician;
    public string MAF;
    public DateTime Date = DateTime.Now;

    public PriorityOneMafPage(PriorityOneMafPageViewModel priorityOneMafPageViewModel)
    {
        InitializeComponent();
        BindingContext = priorityOneMafPageViewModel;
    }

    private void PriorityOneEmail_Button_Pressed(object sender, EventArgs e)
    {

        string To = "ali.shakoor2249@gmail.com";
        string Subject = "Priority 1 - " + Plaza.ToUpper() + " / " + Lane.ToUpper();
        string Body = "****SunWatch Priority 1 MAF****" + "<br>" + "<br>" +
        "Plaza: "+Plaza + "<br>" + "Roadway: "+Roadway + "<br>" + "Lane: "+Lane + "<br>" + "Bomitem: "+Bomitem + "<br>" + 
        "Problem: "+Problem + "<br>" + "Action Take: "+ActionTaken + "<br>" + "Technician: "+Technician + "<br>" + " Date/Time Contacted: " +
         Date + "<br>" + "MAF#: "+MAF;

        mail = (Outlook.MailItem)objApp.CreateItemFromTemplate(Template);
        mail.To = To;
        mail.Subject = Subject;
        mail.HTMLBody = Body;
        mail.Display();
    }

    private void SelectRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectRegion.SelectedIndex;

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            selectTechnician.ItemsSource.Clear();
            Region = selectRegion.Items[selectedIndex];

            tollPlazaQueryByRegionName = TollPlazaRepo.QueryByRegionName(Region);
            foreach (TollPlaza tollPlaza in tollPlazaQueryByRegionName)
            {
                selectPlaza.ItemsSource.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name + " " + tollPlaza.Plaza_roadway + " MP " +
                tollPlaza.Plaza_milepost);
            }

            tollTechnicianQueryByRegion = TollTechnicianRepo.QueryTechnicianByRegion(Region);
            foreach (TollTechnician tollTechnician in tollTechnicianQueryByRegion)
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
                Plaza = plaza.Plaza_id.ToString() + " " + plaza.Plaza_name;
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
            LaneType = (Split[1]);
        }

        if(LaneType=="ADM")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }
        
        else if (LaneType == "DED")
        {
             tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
             foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
             {
                 selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
             }
        }

        else if (LaneType == "ORT")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

        }

        else if (LaneType == "COAPM")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }

        else if (LaneType == "MB")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }
        else if (LaneType == "ME")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }
        else if (LaneType == "MX")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }

        //DisplayAlert("Check", Bomitem, "Close");

    }
    private void SelectTechnician_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectTechnician.SelectedIndex;

        if (selectedIndex != -1)
        {
            Technician = selectTechnician.Items[selectedIndex];
        }
    }

    private void SelectBomitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectBomitem.SelectedIndex;
        if (selectedIndex != -1)
        {
            Bomitem = selectBomitem.Items[selectedIndex];
        }
    }

    private void SelectProblem_TextChanged(object sender, TextChangedEventArgs e)
    {
        Problem = e.NewTextValue;
    }

    private void SelectMafNumber_TextChanged(object sender, TextChangedEventArgs e)
    {
        MAF = e.NewTextValue;
    }

    private void SelectActionTaken_TextChanged(object sender, TextChangedEventArgs e)
    {
        ActionTaken = e.NewTextValue;
    }

}


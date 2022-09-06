using Google.Apis.Compute.v1.Data;
using Master_of_Emails;
using Master_of_Emails.Database;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using System.Linq;

namespace practice.Pages;

public partial class DatabasePage : ContentPage
{
    public DB DB;

    public string Text;
    public string StatusMessage;

    public TollRegionRepository TollRegionRepo = new();
    public TollPlazaRepository TollPlazaRepo= new();
    public TollLaneRepository TollLaneRepo = new();

    public List<TollsRegion> Regions;
    public List<TollPlaza> Plazas;
    public List<TollLane> Lanes;

    public DatabasePage()
	{
		InitializeComponent();

        if (DB.DatabaseConnection == null)
            DB.DatabaseInit();
    }

    public void OnNewRegionButtonClicked(object sender, EventArgs args)
    {
        StatusMessage = "";
        TollRegionRepo.AddRegion(newRegion.Text);
        DisplayAlert("Failed to Add Region", TollRegionRepo.StatusMessage,"Close");
        newRegion.Text = "";
    }

    public void OnGetRegionButtonClicked(object sender, EventArgs args)
    {
        
        Regions =  TollRegionRepo.GetRegions();
        String AllRegions="";

        foreach (var region in Regions)
        {
            AllRegions+=region.Region_id+" "+region.Region_name+"\n";
        }

        DisplayAlert("Region List", AllRegions, "Close");

        //var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MOE.db");
        //DisplayAlert("Alert", databasePath, "accept");
    }

    public void OnDeleteRegionButtonClicked(object sender, EventArgs args)
    {
        
        try
        {
            StatusMessage = "";
            int id = Int32.Parse(deleteRegion.Text);
            TollRegionRepo.DeleteRegion(id);
            deleteRegion.Text = "";
        }
        catch (Exception ex) 

        {
            StatusMessage = ex.Message; 
            DisplayAlert("Failed to Delete", "Please enter a valid Region ID Number. "+StatusMessage, "Close");
        }  
       
    }

    public void OnNewPlazaButtonClicked(object sender, EventArgs args)
    {
        try
        {
            TollPlazaRepo.AddPlaza(Int32.Parse(newPlazaId.Text), newPlazaName.Text, newPlazaRoadway.Text, Int32.Parse(newPlazaMilepost.Text), newPlazaRegionName.Text, newPlazaRegionPhoneNumber.Text);

            newPlazaId.Text = "";
            newPlazaName.Text = "";
            newPlazaRoadway.Text = "";
            newPlazaMilepost.Text = "";
            newPlazaRegionName.Text = "";
            newPlazaRegionPhoneNumber.Text = "";

            DisplayAlert("Alert", TollPlazaRepo.StatusMessage, "Close");
        }

        catch (Exception ex)
        {
            StatusMessage = ex.Message;
            DisplayAlert("Failed to Add Plaza", "Please enter valid Plaza Inputs. " + StatusMessage, "Close");
        }
    
    }

    public void OnGetPlazaButtonClicked(object sender, EventArgs args)
    {

        Plazas= TollPlazaRepo.GetPlazas();
        String AllPlazas = "";

        foreach (var plaza in Plazas)
        {
            AllPlazas += plaza.Plaza_id + " " + plaza.Plaza_name + " " + plaza.Plaza_roadway + " Mile Post " +plaza.Plaza_milepost+" "+plaza.Plaza_region+"\n";
        }

        DisplayAlert("Plaza List", AllPlazas, "Close");

        //var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MOE.db");
        //DisplayAlert("Alert", databasePath, "accept");
    }

    public void OnDeletePlazaButtonClicked(object sender, EventArgs args)
    {
        try
        {
            StatusMessage = "";
            int id = Int32.Parse(deletePlaza.Text);
            TollPlazaRepo.DeletePlaza(id);
            deletePlaza.Text = "";
        }
        catch (Exception ex)

        {
            StatusMessage = ex.Message;
            DisplayAlert("Failed to Delete", "Please enter a valid Plaza ID Number. " + StatusMessage, "Close");
        }
    }

    public void OnNewLaneButtonClicked(object sender, EventArgs args)
    {
        try
        {
            TollLaneRepo.AddLane(Int32.Parse(newLanePlazaId.Text), Int32.Parse(newLaneNumber.Text), newLaneType.Text);

            newLanePlazaId.Text = "";
            newLaneNumber.Text = "";
            newLaneType.Text = "";
          
            DisplayAlert("Alert", TollLaneRepo.StatusMessage, "Close");
        }

        catch (Exception ex)
        {
            StatusMessage = ex.Message;
            DisplayAlert("Failed to Add Lane", "Please enter valid Lane Inputs. " + StatusMessage, "Close");
        }

    }

    public void OnDeleteLaneButtonClicked(object sender, EventArgs args)
    {
        try
        {
            StatusMessage = "";
            int id = Int32.Parse(deleteLane.Text);
            TollLaneRepo.DeleteLane(id);
            deleteLane.Text = "";
        }
        catch (Exception ex)

        {
            StatusMessage = ex.Message;
            DisplayAlert("Failed to Delete", "Please enter a valid Lane ID Number. " + StatusMessage, "Close");
        }
    }

    public void OnGetLaneButtonClicked(object sender, EventArgs args)
    {
        Lanes = TollLaneRepo.GetLanes();
        String AllLanes = "";

        foreach (var lane in Lanes)
        {
            AllLanes += lane.Id+" "+lane.Plaza_id + " Lane " + lane.Lane_number + " " + lane.Lane_Type + "\n";
        }

        DisplayAlert("Plaza List", AllLanes, "Close");

    }



}
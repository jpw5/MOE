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
    public TollsRegionRepository TollsRegionRepo = new();
    public TollPlazaRepository PlazaRepo= new();
    public List<TollsRegion> Region;
    public List<TollPlaza> Plazas;
    public string StatusMessage;

    public DatabasePage()
	{
		InitializeComponent();

        if (DB.DatabaseConnection == null)
            DB.DatabaseInit();
    }

    public void OnNewRegionButtonClicked(object sender, EventArgs args)
    {
        StatusMessage = "";
        TollsRegionRepo.AddRegion(newRegion.Text);
        DisplayAlert("Alert", TollsRegionRepo.StatusMessage,"Close");
        newRegion.Text = "";
    }

    public void OnGetRegionButtonClicked(object sender, EventArgs args)
    {
        
        Region =  TollsRegionRepo.GetRegions();
        String AllRegions="";

        foreach (var region in Region)
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
            TollsRegionRepo.RemoveRegion(id);
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
            PlazaRepo.AddPlaza(Int32.Parse(newPlazaId.Text), newPlazaName.Text, newPlazaRoadway.Text, Int32.Parse(newPlazaMilepost.Text), newPlazaRegionName.Text, newPlazaRegionPhoneNumber.Text);

            newPlazaId.Text = "";
            newPlazaName.Text = "";
            newPlazaRoadway.Text = "";
            newPlazaMilepost.Text = "";
            newPlazaRegionName.Text = "";
            newPlazaRegionPhoneNumber.Text = "";

            DisplayAlert("Alert", PlazaRepo.StatusMessage, "Close");
        }

        catch (Exception ex)
        {
            StatusMessage = ex.Message;
            DisplayAlert("Failed to Add Plaza", "Please enter valid Plaza Inputs. " + StatusMessage, "Close");
        }
    
    }

    public void OnGetPlazaButtonClicked(object sender, EventArgs args)
    {

        Plazas= PlazaRepo.GetPlazas();
        String AllPlazas = "";

        foreach (var plaza in Plazas)
        {
            AllPlazas += plaza.Plaza_id + " " + plaza.Plaza_name + " " + plaza.Plaza_roadway + " Mile Post " +plaza.Plaza_milepost+" "+plaza.Plaza_region+"\n";
        }

        DisplayAlert("Plaza List", AllPlazas, "Close");

        //var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MOE.db");
        //DisplayAlert("Alert", databasePath, "accept");
    }


}
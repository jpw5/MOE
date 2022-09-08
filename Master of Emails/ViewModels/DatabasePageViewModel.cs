using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Database;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels; 
    public partial class DatabasePageViewModel: ObservableObject
    {
        public DB DB;

        public TollRegionRepository TollRegionRepo=new();
        public List<TollRegion> TollRegion = new();
        [ObservableProperty]
        public string newRegion;
        [ObservableProperty]
        public string newRegionStatusMessage;
        [ObservableProperty]
        public string removeRegion;
        [ObservableProperty]
        public string removeRegionStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> regionList;

        public TollPlazaRepository TollPlazaRepo = new();
        public List<TollPlaza> TollPlaza = new();
        [ObservableProperty]
        public string newPlazaId;
        [ObservableProperty]
        public string newPlazaName;
        [ObservableProperty]
        public string newPlazaRoadway;
        [ObservableProperty]
        public string newPlazaMilepost;
        [ObservableProperty]
        public string newPlazaRegionName;
        [ObservableProperty]
        public string newPlazaPhoneNumber;
        [ObservableProperty]
        public string newPlazaStatusMessage;
        [ObservableProperty]
        public string removePlaza;
        [ObservableProperty]
        public string removePlazaStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> plazaList;

        public TollLaneRepository TollLaneRepo = new();
        public List<TollLane> TollLane = new();
        [ObservableProperty]
        public string newLanePlazaId;
        [ObservableProperty]
        public string newLaneNumber;
        [ObservableProperty]
        public string newLaneType;
        [ObservableProperty]
        public string newLaneStatusMessage;
        [ObservableProperty]
        public string removeLane;
        [ObservableProperty]
        public string removeLaneStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> laneList;

        public TollPersonaleRepository TollPersonaleRepo = new();
        public List<TollPersonale> TollPersonale = new();
        [ObservableProperty]
        public string newPersonaleKnId;
        [ObservableProperty]
        public string newPersonaleName;
        [ObservableProperty]
        public string newPersonalePhoneNumber;
        [ObservableProperty]
        public string newPersonaleEmail;
        [ObservableProperty]
        public string newPersonaleRole;
        [ObservableProperty]
        public string newPersonaleStatusMessage;
        [ObservableProperty]
        public string removePersonale;
        [ObservableProperty]
        public string removePersonaleStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> personaleList;

    public DatabasePageViewModel()
        {
            RegionList = new ObservableCollection<string>();
            PlazaList = new ObservableCollection<string>();
            LaneList = new ObservableCollection<string>();
            PersonaleList = new ObservableCollection<string>();

        if (DB.DatabaseConnection == null)
                DB.DatabaseInit();
        }

        [RelayCommand]
        async void AddNewRegion()
            {
            if (string.IsNullOrWhiteSpace(NewRegion))
               {
                    NewRegionStatusMessage = "Error: Please enter a valid name."+NewRegion;
                    await Task.Delay(2000);
                    NewRegionStatusMessage = "";
                    return;
                 }
                  
            try
                 {
                    TollRegionRepo.AddRegion(NewRegion);
                    NewRegion = "";
                    NewRegionStatusMessage = "Success: Region Added.";
                    await Task.Delay(2000);
                    NewRegionStatusMessage = "";
                  }

            catch (Exception)
                  {
                    NewRegionStatusMessage = "Error: Failed to add region.";
                    await Task.Delay(2000);
                    NewRegionStatusMessage = "";
                  }
             
             }

        [RelayCommand]
        async void DeleteRegion()
        {
        if (string.IsNullOrWhiteSpace(RemoveRegion))
            {
            RemoveRegionStatusMessage = "Error: Please enter a valid Region ID number.";
            await Task.Delay(2000);
            RemoveRegionStatusMessage = "";
            return;
            }

        try
            {
            TollRegionRepo.DeleteRegion(Int32.Parse(RemoveRegion));
            RemoveRegion = "";
            RemoveRegionStatusMessage = "Success: Region Deleted.";
            await Task.Delay(2000);
            RemoveRegionStatusMessage = "";
        }

        catch (Exception)

            {
            RemoveRegionStatusMessage = "Error: Failed to delete.";
            await Task.Delay(2000);
            RemoveRegionStatusMessage = "";
            }
        }

        [RelayCommand]
        private void GetAllRegions()
        {
            RegionList.Clear();
            TollRegion = TollRegionRepo.GetRegions();
            foreach(TollRegion region in TollRegion)
            {
            RegionList.Add(region.Region_id.ToString() + " " + region.Region_name);
            }
        }

        [RelayCommand]
        async void AddNewPlaza()
        {
            try
            {
            TollPlazaRepo.AddPlaza(Int32.Parse(NewPlazaId), NewPlazaName, NewPlazaRoadway, Int32.Parse(NewPlazaMilepost), NewPlazaRegionName, NewPlazaPhoneNumber);
            NewPlazaId = "";
            NewPlazaName= "";
            NewPlazaRoadway = "";
            NewPlazaMilepost = "";
            NewPlazaRegionName = "";
            NewPlazaPhoneNumber = "";
            NewPlazaStatusMessage = "Success: Plaza Added.";
            await Task.Delay(2000);
            NewPlazaStatusMessage = "";
            }

            catch (Exception)
            {
                NewPlazaStatusMessage = "Error: Please enter valid inputs.";
                await Task.Delay(2000);
                NewPlazaStatusMessage = "";
            }
        }

        [RelayCommand]
        async void DeletePlaza()
        {
            try
            {
                int id = Int32.Parse(RemovePlaza);
                TollPlazaRepo.DeletePlaza(id);
                RemovePlaza = "";
                RemovePlazaStatusMessage = "Success: Plaza Deleted.";
                await Task.Delay(2000);
                RemovePlazaStatusMessage = "";
            }
            catch (Exception)
            {
                RemovePlazaStatusMessage = "Error: Enter a valid plaza ID.";
                await Task.Delay(2000);
                RemovePlazaStatusMessage = "";

            }
        }

    [RelayCommand]
    private void GetAllPlazas()
    {
        PlazaList.Clear();
        TollPlaza = TollPlazaRepo.GetPlazas();
        foreach (TollPlaza plaza in TollPlaza)
        {
            PlazaList.Add(plaza.Plaza_id.ToString() + " " + plaza.Plaza_name+" "+plaza.Plaza_roadway+" MP:"+plaza.Plaza_milepost+" "+plaza.Plaza_region);
        }
    }

    [RelayCommand]
    async void AddNewLane()
    {
        try
        {
            TollLaneRepo.AddLane(Int32.Parse(NewLanePlazaId), Int32.Parse(NewLaneNumber), NewLaneType);
            NewLanePlazaId = "";
            NewLaneNumber = "";
            NewLaneType = "";
            NewLaneStatusMessage = "Success: Lane Added";
            await Task.Delay(2000);
            NewLaneStatusMessage = "";
        }

        catch (Exception)
        {
            NewLaneStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewLaneStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteLane()
    {
        try
        {
            
            int id = Int32.Parse(RemoveLane);
            TollLaneRepo.DeleteLane(id);
            RemoveLane = "";
            RemoveLaneStatusMessage = "Success: Lane Deleted.";
            await Task.Delay(2000);
            RemoveLaneStatusMessage = "";
        }
        catch (Exception)

        {
            RemoveLaneStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveLaneStatusMessage = "";
        }
    }

    [RelayCommand]
    private void GetAllLanes()
    {
        LaneList.Clear();
        TollLane = TollLaneRepo.GetLanes();
        foreach (TollLane lane in TollLane)
        {
            LaneList.Add("Plaza "+lane.Plaza_id+" Lane "+lane.Lane_number.ToString() + " " + lane.Lane_Type);
        }
    }

    [RelayCommand]
    async void AddNewPersonale()
    {
        try
        {
            TollPersonaleRepo.AddPersonale(NewPersonaleKnId, NewPersonaleName, NewPersonalePhoneNumber, NewPersonaleEmail, NewPersonaleRole);
            NewPersonaleKnId = "";
            NewPersonaleName = "";
            NewPersonalePhoneNumber = "";
            NewPersonaleEmail = "";
            NewPersonaleRole = "";
            NewPersonaleStatusMessage = "Success: Personale Added";
            await Task.Delay(2000);
            NewPersonaleStatusMessage = "";
        }

        catch (Exception)
        {
            NewPersonaleStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewPersonaleStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeletePersonale()
    {
       if(RemovePersonale==null)
        {
            RemovePersonaleStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemovePersonaleStatusMessage = "";
       }
        else
        {
            TollPersonaleRepo.DeletePersonale(RemovePersonale);
            RemovePersonale = "";
            RemovePersonaleStatusMessage = "Success: Personal Deleted.";
            await Task.Delay(2000);
            RemovePersonaleStatusMessage = "";
        }
            
        
       
    }
    [RelayCommand]
    private void GetAllPersonale()
    {
        PersonaleList.Clear();
        try
        {
            TollPersonale = TollPersonaleRepo.GetPersonale();
            foreach (TollPersonale personale in TollPersonale)
            {
                PersonaleList.Add(personale.Personale_kn_id + " " + personale.Personale_name + " "+personale.Personale_role+" \n" + personale.Personale_phone_number + " " + personale.Personale_email);
                PersonaleList.Add(" ");
            }
        }

        catch(Exception)
        {
            PersonaleList.Add("No Data Found");
        }
      
    }


}


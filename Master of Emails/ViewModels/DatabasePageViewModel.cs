﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Database;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using System.Collections.ObjectModel;
using System.Linq;

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

        public TollTechnicianRepository TollTechnicianRepo = new();
        public List<TollTechnician> TollTechnician = new();
        [ObservableProperty]
        public string newTechnicianKnId;
        [ObservableProperty]
        public string newTechnicianName;
        [ObservableProperty]
        public string newTechnicianPhoneNumber;
        [ObservableProperty]
        public string newTechnicianEmail;
        [ObservableProperty]
        public string newTechnicianRegion;
        [ObservableProperty]
        public string newTechnicianStatusMessage;
        [ObservableProperty]
        public string removeTechnician;
        [ObservableProperty]
        public string removeTechnicianStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> technicianList;

        public TollBomitemRepository TollBomitemRepo = new();
        public List<TollBomitem> TollBomitem = new();
        [ObservableProperty]
        public string newBomitemLaneType;
        [ObservableProperty]
        public string newBomitemName;
    [   ObservableProperty]
        public string newBomitemStatusMessage;
        [ObservableProperty]
        public string removeBomitem;
        [ObservableProperty]
        public string removeBomitemStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> bomitemList;

        public TollDuressReasonRepository TollDuressReasonRepo = new();
        public List<TollDuressReason> TollDuressReason = new();
        [ObservableProperty]
        public string newDuressReason;
        [ObservableProperty]
        public string newDuressReasonStatusMessage;
        [ObservableProperty]
        public string removeDuressReason;
        [ObservableProperty]
        public string removeDuressReasonStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> duressReasonList;

        public TollScadaAlarmRepository TollScadaAlarmRepo = new();
        public List<TollScadaAlarm> TollScadaAlarm = new();
        [ObservableProperty]
        public string newScadaAlarm;
        [ObservableProperty]
        public string newScadaAlarmStatusMessage;
        [ObservableProperty]
        public string removeScadaAlarm;
        [ObservableProperty]
        public string removeScadaAlarmStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> scadaAlarmList;

        public TollFacilitiesTelecomRepository TollFacilitiesTelecomRepo = new();
        public List<TollFacilitiesTelecom> TollFacilitiesTelecom = new();

        [ObservableProperty]
        public string newFacilitiesTelecomKnID;
        [ObservableProperty]
        public string newFacilitiesTelecomName;
        [ObservableProperty]
        public string newFacilitiesTelecomPhoneNumber;
        [ObservableProperty]
        public string newFacilitiesTelecomAlternatePhoneNumber;
        [ObservableProperty]
        public string newFacilitiesTelecomEmail;
        [ObservableProperty]
        public string newFacilitiesTelecomStatusMessage;
        [ObservableProperty]
        public string newDepartment;
        [ObservableProperty]
        public string removeFacilitiesTelecom;
        [ObservableProperty]
        public string removeFacilitiesTelecomStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> facilitiesTelecomList;

    public DatabasePageViewModel()
        {
            RegionList = new ObservableCollection<string>();
            PlazaList = new ObservableCollection<string>();
            LaneList = new ObservableCollection<string>();
            TechnicianList = new ObservableCollection<string>();
            BomitemList = new ObservableCollection<string>();
            DuressReasonList = new ObservableCollection<string>();
            ScadaAlarmList = new ObservableCollection<string>();
            FacilitiesTelecomList = new ObservableCollection<string>();

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
            try
            {
                TollRegion = TollRegionRepo.GetRegions();
                foreach (TollRegion region in TollRegion)
                {
                    RegionList.Add(region.Region_id.ToString() + " " + region.Region_name);
                }

            }

            catch (Exception)
            {
                RegionList.Add("No Data in database");
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
        try
        {
            TollPlaza = TollPlazaRepo.GetPlazas();
            foreach (TollPlaza plaza in TollPlaza)
            {
              PlazaList.Add(plaza.Plaza_id.ToString() + " " + plaza.Plaza_name + " " + plaza.Plaza_roadway + " MP:" + plaza.Plaza_milepost + " " + plaza.Plaza_region);
            }

        }

        catch
        {
            PlazaList.Add("No Data in database");
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
    async void AddNewTechnician()
    {
        if (string.IsNullOrWhiteSpace(NewTechnicianKnId) | string.IsNullOrWhiteSpace(NewTechnicianName) |
            string.IsNullOrWhiteSpace(NewTechnicianPhoneNumber) | string.IsNullOrWhiteSpace(NewTechnicianEmail) |
            string.IsNullOrWhiteSpace(NewTechnicianRegion))
        {
            NewTechnicianStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewTechnicianStatusMessage = "";
            return;
        }
           
        try
        {
            
            TollTechnicianRepo.AddTechnician(NewTechnicianKnId, NewTechnicianName, NewTechnicianPhoneNumber, 
            NewTechnicianEmail, NewTechnicianRegion);

            NewTechnicianKnId = "";
            NewTechnicianName = "";
            NewTechnicianPhoneNumber = "";
            NewTechnicianEmail = "";
            NewTechnicianRegion = "";
            NewTechnicianStatusMessage = "Success: Technician Added";
            await Task.Delay(2000);
            NewTechnicianStatusMessage = "";
        }

        catch (Exception)
        {
            NewTechnicianStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewTechnicianStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteTechnician()
    {
       if(RemoveTechnician==null)
        {
            RemoveTechnicianStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveTechnicianStatusMessage = "";
            return;
       }

        try
        {
            TollTechnicianRepo.DeleteTechnician(RemoveTechnician);
            RemoveTechnician = "";
            RemoveTechnicianStatusMessage = "Success: Technician Deleted.";
            await Task.Delay(2000);
            RemoveTechnicianStatusMessage = "";
        }

        catch (Exception)
        {
            RemoveTechnicianStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveTechnicianStatusMessage = "";
        }
        
         
        
    }
    [RelayCommand]
    private void GetAllTechnicians()
    {
        TechnicianList.Clear();
        try
        {
            TollTechnician = TollTechnicianRepo.GetTechnician();
            foreach (TollTechnician technician in TollTechnician)
            {
                TechnicianList.Add(technician.Technician_kn_id + " " + technician.Technician_name + " " + 
                technician.Technician_region+" \n" + technician.Technician_phone_number + " " + 
                technician.Technician_email);

                TechnicianList.Add(" ");
            }
        }

        catch(Exception)
        {
            TechnicianList.Add("No Data Found");
        }
      
    }

    [RelayCommand]
    async void AddNewBomitem()
    {
        if (string.IsNullOrWhiteSpace(NewBomitemLaneType) | string.IsNullOrWhiteSpace(NewBomitemName))
        {
            NewBomitemStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewBomitemStatusMessage = "";
            return;
        }

        try
        {

            TollBomitemRepo.AddBomitem(NewBomitemLaneType, NewBomitemName);
            NewBomitemLaneType = "";
            NewBomitemName = "";
            NewBomitemStatusMessage = "Success: Bom Item Added";
            await Task.Delay(2000);
            NewBomitemStatusMessage = "";
        }

        catch(Exception)
        {
            NewBomitemStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewBomitemStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteBomitem()
    {
        if (string.IsNullOrWhiteSpace(RemoveBomitem))
        {
            RemoveBomitemStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveBomitemStatusMessage = "";
            return ;
        }
        try
        {
            TollBomitemRepo.DeleteBomitem(Int32.Parse(RemoveBomitem));
            RemoveBomitem = "";
            RemoveBomitemStatusMessage = "Success: Bomitem Deleted.";
            await Task.Delay(2000);
            RemoveBomitemStatusMessage = "";
        }

        catch (Exception)
        {
            RemoveBomitemStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveBomitemStatusMessage = "";
        }
    }

    [RelayCommand]
    private void GetAllBomitems()
    {
        BomitemList.Clear();
        try
        {
            TollBomitem = TollBomitemRepo.GetBomitems();
            foreach (TollBomitem bomitem in TollBomitem)
            {
                BomitemList.Add(bomitem.Bomitem_id + " " + bomitem.Bomitem_name);
            }
        }

        catch (Exception)
        {
            BomitemList.Add("No Data Found");
        }
    }

    [RelayCommand]
    async void AddNewDuressReason ()
    {
        if (string.IsNullOrWhiteSpace(NewDuressReason))
        {
            NewDuressReasonStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewDuressReasonStatusMessage = "";
            return;
        }

        try
        {
            TollDuressReasonRepo.AddDuressReason(NewDuressReason);
            NewDuressReason = "";
            
            NewDuressReasonStatusMessage= "Success: Duress Reason Added";
            await Task.Delay(2000);
            NewDuressReasonStatusMessage = "";
        }

        catch (Exception)
        {
            NewDuressReasonStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewDuressReasonStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteDuressReason()
    {
        if (string.IsNullOrWhiteSpace(RemoveDuressReason))
        {
            RemoveDuressReasonStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveDuressReasonStatusMessage = "";
            return;
        }
        try
        {
            TollDuressReasonRepo.DeleteDuressReason(Int32.Parse(RemoveDuressReason));
            RemoveDuressReason = "";
            RemoveDuressReasonStatusMessage = "Success: Duress Reason Deleted.";
            await Task.Delay(2000);
            RemoveDuressReasonStatusMessage = "";
        }

        catch (Exception)
        {
            RemoveDuressReasonStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveDuressReasonStatusMessage = "";
        }
    }

    [RelayCommand]
    private void GetAllDuressReasons()
    {
        DuressReasonList.Clear();
        try
        {
            TollDuressReason = TollDuressReasonRepo.GetDuressReasons();
            foreach (TollDuressReason duressreason in TollDuressReason)
            {
                DuressReasonList.Add(duressreason.Duress_reason_id + " " + duressreason.Duress_reason_name);
            }
        }

        catch (Exception)
        {
            DuressReasonList.Add("No Data Found");
        }
    }

    [RelayCommand]
    async void AddNewScadaAlarm()
    {
        if (string.IsNullOrWhiteSpace(NewScadaAlarm))
        {
            NewScadaAlarmStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewScadaAlarmStatusMessage = "";
            return;
        }

        try
        {
            TollScadaAlarmRepo.AddScadaAlarm(NewScadaAlarm);
            NewScadaAlarm = "";
            NewScadaAlarmStatusMessage = "Success: SCADA Alarm Added";
            await Task.Delay(2000);
            NewScadaAlarmStatusMessage = "";
        }

        catch (Exception)
        {
            NewScadaAlarmStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewScadaAlarmStatusMessage = "";
        }

    }

    [RelayCommand]
    async void DeleteScadaAlarm()
    {
        if (string.IsNullOrWhiteSpace(RemoveScadaAlarm))
        {
            RemoveScadaAlarmStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveScadaAlarmStatusMessage = "";
            return;
        }
        try
        {
            TollScadaAlarmRepo.DeleteScadaAlarm(Int32.Parse(RemoveScadaAlarm));
            RemoveScadaAlarm = "";
            RemoveScadaAlarmStatusMessage = "Success: SCADA Alarm Deleted.";
            await Task.Delay(2000);
            RemoveScadaAlarmStatusMessage = "";
        }

        catch (Exception)
        {
            RemoveScadaAlarmStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveScadaAlarmStatusMessage = "";
        }
    }

    [RelayCommand]
    private void GetAllScadaAlarms()
    {
        ScadaAlarmList.Clear();
        try
        {
            TollScadaAlarm = TollScadaAlarmRepo.GetScadaAlarms();
            foreach (TollScadaAlarm scadaalarm in TollScadaAlarm)
            {
                ScadaAlarmList.Add(scadaalarm.Scada_alarm_id + " " + scadaalarm.Scada_alarm_name);
            }
        }

        catch (Exception)
        {
            ScadaAlarmList.Add("No Data Found");
        }
    }

    [RelayCommand]
    async void AddNewFacilitiesTelecom()
    {
        if (string.IsNullOrWhiteSpace(NewFacilitiesTelecomKnID) | string.IsNullOrWhiteSpace(NewFacilitiesTelecomName)|
            string.IsNullOrWhiteSpace(NewFacilitiesTelecomPhoneNumber) | string.IsNullOrWhiteSpace(NewFacilitiesTelecomAlternatePhoneNumber)| 
            string.IsNullOrWhiteSpace(NewFacilitiesTelecomEmail))
        {
            NewFacilitiesTelecomStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewFacilitiesTelecomStatusMessage = "";
            return;
        }

        try
        {
            TollFacilitiesTelecomRepo.AddFacilitiesTelecom(NewFacilitiesTelecomKnID, NewFacilitiesTelecomName, NewFacilitiesTelecomPhoneNumber,
            NewFacilitiesTelecomAlternatePhoneNumber, NewFacilitiesTelecomEmail, NewDepartment);

            NewFacilitiesTelecomStatusMessage = "Success: Personale Added";
            await Task.Delay(2000);
            NewFacilitiesTelecomStatusMessage = "";
            NewFacilitiesTelecomKnID = "";
            NewFacilitiesTelecomName = "";
            NewFacilitiesTelecomPhoneNumber = "";
            NewFacilitiesTelecomAlternatePhoneNumber = "";
            NewFacilitiesTelecomEmail = "";
            NewDepartment = "";
        }

        catch (Exception)
        {
            NewFacilitiesTelecomStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            NewFacilitiesTelecomStatusMessage = "";
        }

    }

    [RelayCommand]
    async void DeleteFacilitiesTelecom()
    {
        if (string.IsNullOrWhiteSpace(RemoveFacilitiesTelecom))
        {
            RemoveFacilitiesTelecomStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveFacilitiesTelecomStatusMessage = "";
            return;
        }
        try
        {
            TollFacilitiesTelecomRepo.DeleteFacilitiesTelecom(RemoveFacilitiesTelecom);
            RemoveFacilitiesTelecom = "";
            RemoveFacilitiesTelecomStatusMessage = "Success: Personale Deleted.";
            await Task.Delay(2000);
            RemoveFacilitiesTelecomStatusMessage = "";
        }

        catch (Exception)
        {
            RemoveFacilitiesTelecomStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            RemoveFacilitiesTelecomStatusMessage = "";
        }
    }

    [RelayCommand]
    private void GetAllFacilitiesTelecom()
    {
        FacilitiesTelecomList.Clear();
        try
        {
            TollFacilitiesTelecom = TollFacilitiesTelecomRepo.GetFacilitiesTelecoms();
            foreach (TollFacilitiesTelecom facilitiestelecom in TollFacilitiesTelecom)
            {
                FacilitiesTelecomList.Add(facilitiestelecom.Facilities_telecom_kn_id + " " 
                + facilitiestelecom.Facilities_telecom_name + " "+facilitiestelecom.Facilities_telecom_phone_number
                +" "+facilitiestelecom.Facilities_telecom_email);

                FacilitiesTelecomList.Add(" ");
            }
        }

        catch (Exception)
        {
            FacilitiesTelecomList.Add("No Data Found");
        }
    }


}


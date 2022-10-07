using CommunityToolkit.Mvvm.ComponentModel;
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
        public string newPlazaCompany;
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
        public string newDepartment;
        [ObservableProperty]
        public string newFacilitiesTelecomStatusMessage;
        [ObservableProperty]
        public string removeFacilitiesTelecom;
        [ObservableProperty]
        public string removeFacilitiesTelecomStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> facilitiesTelecomList;

        public TollOrganizationRepository TollOrganizationRepo = new();
        public List<TollOrganization> TollOrganization = new();
        [ObservableProperty]
        public string newOrganizationName;
        [ObservableProperty]
        public string newOrganizationPhoneNumber;
        [ObservableProperty]
        public string newOrganizationEmail;
        [ObservableProperty]
        public string newOrganizationStatusMessage;
        [ObservableProperty]
        public string removeOrganization;
        [ObservableProperty]
        public string removeOrganizationStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> organizationList;

        public TollEmailDistributionRepository TollEmailDistributionRepo = new();
        public List<TollEmailDistribution> TollEmailDistribution = new();
        [ObservableProperty]
        public string newEmailDistributionRegion;
        [ObservableProperty]
        public string newEmailDistributionType;
        [ObservableProperty]
        public string newEmailDistributionPlazaId;
        [ObservableProperty]
        public string newEmailDistributionTo;
        [ObservableProperty]
        public string newEmailDistributionCc;
        [ObservableProperty]
        public string newEmailDistributionStatusMessage;
        [ObservableProperty]
        public string removeEmailDistribution;
        [ObservableProperty]
        public string removeEmailDistributionStatusMessage;
        [ObservableProperty]
        public ObservableCollection<string> emailDistributionList;

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
        public string newPersonaleDepartment;
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
            TechnicianList = new ObservableCollection<string>();
            BomitemList = new ObservableCollection<string>();
            DuressReasonList = new ObservableCollection<string>();
            FacilitiesTelecomList = new ObservableCollection<string>();
            OrganizationList= new ObservableCollection<string>();
            EmailDistributionList = new ObservableCollection<string>();
            PersonaleList = new ObservableCollection<string>();
            
        if (DB.DatabaseConnection == null)
                DB.DatabaseInit();
        }

        [RelayCommand]
        async void AddNewRegion()
            {
            try
                 {
                    TollRegionRepo.AddRegion(NewRegion);
                    NewRegion = "";
                    NewRegionStatusMessage = "Success: Region Added.";
                    await Task.Delay(2000);
                    NewRegionStatusMessage = "";
                  }

            catch (Exception ex)
                  {
                    NewRegionStatusMessage = "Error: Failed to add region. "+ex.Message;
                    await Task.Delay(2000);
                    NewRegionStatusMessage = "";
                  }
             
             }

        [RelayCommand]
        async void DeleteRegion()
        {
        try
            {
            TollRegionRepo.DeleteRegion(Int32.Parse(RemoveRegion));
            RemoveRegion = "";
            RemoveRegionStatusMessage = "Success: Region Deleted.";
            await Task.Delay(2000);
            RemoveRegionStatusMessage = "";
        }

        catch (Exception ex)

            {
            RemoveRegionStatusMessage = "Error: Failed to delete. "+ex.Message;
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
                    RegionList.Add
                        (
                        "ID: "+region.Region_id + " \n" + 
                        "Name: "+region.Region_name + " \n"
                        );
                }
            }

            catch (Exception ex)
            {
                RegionList.Add("No Data in database. "+ex.Message);
            }      
        }

        [RelayCommand]
        async void AddNewPlaza()
        {
            try
            {

                TollPlazaRepo.AddPlaza
                (
                Int32.Parse(NewPlazaId), 
                NewPlazaName, NewPlazaRoadway, 
                Int32.Parse(NewPlazaMilepost), 
                NewPlazaRegionName, 
                NewPlazaPhoneNumber,
                NewPlazaCompany
                );

            NewPlazaId = "";
            NewPlazaName= "";
            NewPlazaRoadway = "";
            NewPlazaMilepost = "";
            NewPlazaRegionName = "";
            NewPlazaPhoneNumber = "";
            NewPlazaCompany = "";
            NewPlazaStatusMessage = "Success: Plaza Added.";
            await Task.Delay(2000);
            NewPlazaStatusMessage = "";
            }

            catch (Exception ex)
            {
                NewPlazaStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
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
            catch (Exception ex)
            {
                RemovePlazaStatusMessage = "Error: Enter a valid plaza ID. "+ex.Message;
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
              PlazaList.Add
                    (
                    "Plaza: "+plaza.Plaza_id + " \n" + 
                    "Name: "+plaza.Plaza_name + " \n" + 
                    "Roadway: "+plaza.Plaza_roadway + " \n" + 
                    "MP: "+plaza.Plaza_milepost + " \n" + 
                    "Region: "+plaza.Plaza_region + " \n"
                    );
            }
        }
        catch(Exception ex)
        {
            PlazaList.Add("No Data in database. "+ex.Message);
        }
    }

    [RelayCommand]
    async void AddNewLane()
    {
        try
        {
            TollLaneRepo.AddLane
                (
                Int32.Parse(NewLanePlazaId), 
                Int32.Parse(NewLaneNumber), 
                NewLaneType
                );

            NewLanePlazaId = "";
            NewLaneNumber = "";
            NewLaneType = "";
            NewLaneStatusMessage = "Success: Lane Added";
            await Task.Delay(2000);
            NewLaneStatusMessage = "";
        }

        catch (Exception ex)
        {
            NewLaneStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
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
        catch (Exception ex)

        {
            RemoveLaneStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
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

            LaneList.Add
                (
                "Plaza "+lane.Plaza_id + " \n" +
                "Lane: "+lane.Lane_number + " \n" + 
                "Type: "+lane.Lane_Type + " \n"
                );
        }
    }

    [RelayCommand]
    async void AddNewTechnician()
    {
        try
        {
            
            TollTechnicianRepo.AddTechnician
                (
                NewTechnicianKnId, 
                NewTechnicianName, 
                NewTechnicianPhoneNumber, 
                NewTechnicianEmail, 
                NewTechnicianRegion
                );

            NewTechnicianKnId = "";
            NewTechnicianName = "";
            NewTechnicianPhoneNumber = "";
            NewTechnicianEmail = "";
            NewTechnicianRegion = "";
            NewTechnicianStatusMessage = "Success: Technician Added";
            await Task.Delay(2000);
            NewTechnicianStatusMessage = "";
        }

        catch (Exception ex)
        {
            NewTechnicianStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
            await Task.Delay(2000);
            NewTechnicianStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteTechnician()
    {
        try
        {
            TollTechnicianRepo.DeleteTechnician(RemoveTechnician);
            RemoveTechnician = "";
            RemoveTechnicianStatusMessage = "Success: Technician Deleted.";
            await Task.Delay(2000);
            RemoveTechnicianStatusMessage = "";
        }

        catch (Exception ex)
        {
            RemoveTechnicianStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
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
                TechnicianList.Add
                    (
                    "KNID: "+technician.Technician_kn_id + " \n" + 
                    "Name: "+technician.Technician_name + " \n" + 
                    "Region: "+technician.Technician_region+" \n" + 
                    "Phone: "+technician.Technician_phone_number + " \n" + 
                    "Email: "+technician.Technician_email+" \n"
                    );

                
            }
        }

        catch(Exception ex)
        {
            TechnicianList.Add("No Data Found"+ex.Message);
        }
    }

    [RelayCommand]
    async void AddNewBomitem()
    {
        try
        {
            TollBomitemRepo.AddBomitem(NewBomitemLaneType, NewBomitemName);
            NewBomitemLaneType = "";
            NewBomitemName = "";
            NewBomitemStatusMessage = "Success: Bom Item Added";
            await Task.Delay(2000);
            NewBomitemStatusMessage = "";
        }

        catch(Exception ex)
        {
            NewBomitemStatusMessage = "Error: Please enter valid inputs. " +ex.Message;
            await Task.Delay(2000);
            NewBomitemStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteBomitem()
    {
        try
        {
            TollBomitemRepo.DeleteBomitem(Int32.Parse(RemoveBomitem));
            RemoveBomitem = "";
            RemoveBomitemStatusMessage = "Success: Bomitem Deleted.";
            await Task.Delay(2000);
            RemoveBomitemStatusMessage = "";
        }

        catch (Exception ex)
        {
            RemoveBomitemStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
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
                BomitemList.Add
                    (
                    bomitem.Bomitem_id + " " + 
                    bomitem.Bomitem_name
                    );
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
        try
        {
            TollDuressReasonRepo.AddDuressReason(NewDuressReason);
            NewDuressReason = "";
            
            NewDuressReasonStatusMessage= "Success: Duress Reason Added";
            await Task.Delay(2000);
            NewDuressReasonStatusMessage = "";
        }

        catch (Exception ex)
        {
            NewDuressReasonStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
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
                DuressReasonList.Add
                    (
                    "ID: "+duressreason.Duress_reason_id + " \n" + 
                    "Reason: "+duressreason.Duress_reason_name + " \n"
                    );
            }
        }

        catch (Exception)
        {
            DuressReasonList.Add("No Data Found");
        }
    }

    [RelayCommand]
    async void AddNewFacilitiesTelecom()
    {
        try
        {
            TollFacilitiesTelecomRepo.AddFacilitiesTelecom
               (
               NewFacilitiesTelecomKnID, 
               NewFacilitiesTelecomName, 
               NewFacilitiesTelecomPhoneNumber,
               NewFacilitiesTelecomAlternatePhoneNumber, 
               NewFacilitiesTelecomEmail, 
               NewDepartment
               );

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

        catch (Exception ex)
        {
            NewFacilitiesTelecomStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
            await Task.Delay(2000);
            NewFacilitiesTelecomStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteFacilitiesTelecom()
    {
      
        try
        {
            TollFacilitiesTelecomRepo.DeleteFacilitiesTelecom(RemoveFacilitiesTelecom);
            RemoveFacilitiesTelecom = "";
            RemoveFacilitiesTelecomStatusMessage = "Success: Personale Deleted.";
            await Task.Delay(2000);
            RemoveFacilitiesTelecomStatusMessage = "";
        }

        catch (Exception ex)
        {
            RemoveFacilitiesTelecomStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
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
                FacilitiesTelecomList.Add
                  (
                  "KNID: " + facilitiestelecom.Facilities_telecom_kn_id + " \n" +
                  "Name: " + facilitiestelecom.Facilities_telecom_name + " \n" +
                  "Phone Number: " + facilitiestelecom.Facilities_telecom_phone_number + " \n" +
                  "Email: " + facilitiestelecom.Facilities_telecom_email + " \n"
                  );
              
            }
        }

        catch (Exception ex)
        {
            FacilitiesTelecomList.Add("No Data Found. "+ex.Message);
        }
    }

    [RelayCommand]
    async void AddNewOrganization()
    {
        try
        {
            TollOrganizationRepo.AddOrganization
            (
            NewOrganizationName, 
            NewOrganizationPhoneNumber, 
            NewOrganizationEmail 
            );

            NewOrganizationStatusMessage = "Success: Organization Added";
            await Task.Delay(2000);
            NewOrganizationStatusMessage = "";
            NewOrganizationName = "";
            NewOrganizationPhoneNumber = "";
            NewOrganizationEmail = "";
        }

        catch (Exception ex)
        {
            NewOrganizationStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
            await Task.Delay(2000);
            NewOrganizationStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteOrganization()
    {
        try
        {
            TollOrganizationRepo.DeleteOrgnization(Int32.Parse(RemoveOrganization));
            RemoveOrganization = "";
            RemoveOrganizationStatusMessage = "Success: Organization Deleted.";
            await Task.Delay(2000);
            RemoveOrganizationStatusMessage = "";
        }

        catch (Exception ex)
        {
            RemoveOrganizationStatusMessage = "Error: Please enter valid inputs. "+ex.Message;
            await Task.Delay(2000);
            RemoveOrganizationStatusMessage = "";
        }
    }

    [RelayCommand]
    private void GetAllOrganizations()
    {
        OrganizationList.Clear();
        try
        {
            TollOrganization = TollOrganizationRepo.GetOrganizations();
            foreach (TollOrganization organization in TollOrganization)
            {
                OrganizationList.Add
                (
                "ID: "+organization.Organization_id+ " \n"+
                "Name: "+organization.Organization_name+" \n" +
                "Number: "+organization.Organization_phone_number+" \n"+
                "Email: "+organization.Organization_email+"\n"
                );
                
            }
        }

        catch (Exception ex)
        {
            OrganizationList.Add("No Data Found. "+ex.Message);
        }
    }

    [RelayCommand]
    async void AddNewEmailDistribution()
    {
        try
        {
            TollEmailDistributionRepo.AddEmailDistribution
            (
            NewEmailDistributionRegion, 
            NewEmailDistributionType,
            NewEmailDistributionPlazaId,
            NewEmailDistributionTo,
            NewEmailDistributionCc
            );

            NewEmailDistributionStatusMessage = "Success: Email Distribution Added";
            await Task.Delay(2000);
            NewEmailDistributionStatusMessage = "";
            NewEmailDistributionRegion = "";
            NewEmailDistributionType = "";
            NewEmailDistributionPlazaId = "";
            NewEmailDistributionTo = "";
            NewEmailDistributionCc = "";
        }

        catch (Exception ex)
        {
            NewEmailDistributionStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            NewEmailDistributionStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteEmailDistribution()
    {
        try
        {
            TollEmailDistributionRepo.DeleteEmailDistribution(Int32.Parse(RemoveEmailDistribution));
            RemoveEmailDistribution = "";
            RemoveEmailDistributionStatusMessage = "Success: Email Distribution Deleted.";
            await Task.Delay(2000);
            RemoveEmailDistributionStatusMessage = "";
        }

        catch (Exception ex)
        {
            RemoveEmailDistributionStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            RemoveEmailDistributionStatusMessage = "";
        }
    }


    [RelayCommand]
    private void GetAllEmailDistributions()
    {
        EmailDistributionList.Clear();
        try
        {
            TollEmailDistribution = TollEmailDistributionRepo.GetEmailDistributions();
            foreach (TollEmailDistribution emaildistribution in TollEmailDistribution)
            {
                EmailDistributionList.Add
                (
                "Region: " + emaildistribution.Email_distribution_region + " \n" +
                "Type: " + emaildistribution.Email_distribution_type + " \n" +
                "Plaza: " + emaildistribution.Email_distribution_plaza_id + " \n" +
                "To: " + emaildistribution.Email_distribution_to + " \n" +
                "CC: " + emaildistribution.Email_distribution_cc + " \n"
                );

            }
        }

        catch (Exception ex)
        {
            EmailDistributionList.Add("No Data Found. " + ex.Message);
        }
    }

    [RelayCommand]
    async void AddNewPersonale()
    {
        try
        {
            TollPersonaleRepo.AddPersonale
            (
            NewPersonaleKnId,
            NewPersonaleName,
            NewPersonalePhoneNumber,
            NewPersonaleEmail,
            NewPersonaleDepartment
            );

            NewPersonaleStatusMessage = "Success: Personale Added";
            await Task.Delay(2000);
            NewPersonaleStatusMessage = "";
            NewPersonaleKnId = "";
            NewPersonaleName = "";
            NewPersonalePhoneNumber = "";
            NewPersonaleEmail = "";
            NewPersonaleDepartment = "";
        }

        catch (Exception ex)
        {
            NewPersonaleStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            NewPersonaleStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeletePersonale()
    {
        try
        {
            TollPersonaleRepo.DeletePersonale(RemovePersonale);
            RemovePersonale = "";
            RemovePersonaleStatusMessage = "Success: Email Distribution Deleted.";
            await Task.Delay(2000);
            RemovePersonaleStatusMessage = "";
        }

        catch (Exception ex)
        {
            RemovePersonaleStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
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
                PersonaleList.Add
                (
                "ID: " + personale.Personale_kn_id + " \n" +
                "Name: " + personale.Personale_name + " \n" +
                "Phone: " + personale.Personale_phone_number + " \n" +
                "Email: " + personale.Personale_email + " \n" +
                "Department: " + personale.Personale_department + " \n"
                );

            }
        }

        catch (Exception ex)
        {
            PersonaleList.Add("No Data Found. " + ex.Message);
        }
    }
}


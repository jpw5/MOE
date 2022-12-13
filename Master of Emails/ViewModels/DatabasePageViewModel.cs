using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Database;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;
public partial class DatabasePageViewModel : ObservableObject
{
    public DB DB;

    public TollRegionRepository TollRegionRepo = new();
    public List<TollRegion> TollRegion = new();
    [ObservableProperty]
    public string newRegion;
    [ObservableProperty]
    public string regionStatusMessage;
    [ObservableProperty]
    public string removeRegion;
  
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
    public string plazaStatusMessage;
    [ObservableProperty]
    public string removePlaza;
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
    public string newLaneDirection;
    [ObservableProperty]
    public string laneStatusMessage;
    [ObservableProperty]
    public string removeLane;
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
    public string technicianStatusMessage;
    [ObservableProperty]
    public string removeTechnician;
    [ObservableProperty]
    public ObservableCollection<string> technicianList;

    public TollBomitemRepository TollBomitemRepo = new();
    public List<TollBomitem> TollBomitem = new();
    [ObservableProperty]
    public string newBomitemLaneType;
    [ObservableProperty]
    public string newBomitemName;
    [ObservableProperty]
    public string bomitemStatusMessage;
    [ObservableProperty]
    public string removeBomitem;
    [ObservableProperty]
    public ObservableCollection<string> bomitemList;

    public TollDuressReasonRepository TollDuressReasonRepo = new();
    public List<TollDuressReason> TollDuressReason = new();
    [ObservableProperty]
    public string newDuressReason;
    [ObservableProperty]
    public string removeDuressReason;
    [ObservableProperty]
    public string duressReasonStatusMessage;
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
    public string facilitiesTelecomStatusMessage;
    [ObservableProperty] public string removeFacilitiesTelecom;
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
    public string organizationStatusMessage;
    [ObservableProperty]
    public string removeOrganization;
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
    public string emailDistributionStatusMessage;
    [ObservableProperty]
    public string removeEmailDistribution;
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
    public string newPersonaleRole;
    [ObservableProperty]
    public string personaleStatusMessage;
    [ObservableProperty]
    public string removePersonale;
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
        OrganizationList = new ObservableCollection<string>();
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
            RegionStatusMessage = "Success: Region Added.";
            await Task.Delay(2000);
            RegionStatusMessage = "";
        }

        catch (Exception ex)
        {
            RegionStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            RegionStatusMessage = "";
        }

    }

    [RelayCommand]
    async void DeleteRegion()
    {
        try
        {
            TollRegionRepo.DeleteRegion(Int32.Parse(RemoveRegion));
            RemoveRegion = "";
            RegionStatusMessage = "Success: Region Deleted.";
            await Task.Delay(2000);
            RegionStatusMessage = "";
        }

        catch (Exception ex)

        {
            RegionStatusMessage = "Error: Enter a valid Region ID. " + ex.Message;
            await Task.Delay(2000);
            RegionStatusMessage = "";
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
                    "ID: " + region.Region_id + " \n" +
                    "Name: " + region.Region_name + " \n"
                    );
            }
        }

        catch (Exception ex)
        {
            RegionList.Add("No Data in database. " + ex.Message);
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
            NewPlazaName = "";
            NewPlazaRoadway = "";
            NewPlazaMilepost = "";
            NewPlazaRegionName = "";
            NewPlazaPhoneNumber = "";
            NewPlazaCompany = "";
            PlazaStatusMessage = "Success: Plaza Added.";
            await Task.Delay(2000);
            PlazaStatusMessage = "";
        }

        catch (Exception ex)
        {
            PlazaStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            PlazaStatusMessage = "";
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
            PlazaStatusMessage = "Success: Plaza Deleted.";
            await Task.Delay(2000);
            PlazaStatusMessage = "";
        }
        catch (Exception ex)
        {
            PlazaStatusMessage = "Error: Enter a valid plaza ID. " + ex.Message;
            await Task.Delay(2000);
            PlazaStatusMessage = "";

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
                      "Plaza: " + plaza.Plaza_id + " \n" +
                      "Name: " + plaza.Plaza_name + " \n" +
                      "Roadway: " + plaza.Plaza_roadway + " \n" +
                      "MP: " + plaza.Plaza_milepost + " \n" +
                      "Region: " + plaza.Plaza_region + " \n"
                      );
            }
        }
        catch (Exception ex)
        {
            PlazaList.Add("No Data in database. " + ex.Message);
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
                NewLaneType,
                NewLaneDirection
                );

            NewLanePlazaId = "";
            NewLaneNumber = "";
            NewLaneType = "";
            NewLaneDirection = "";
            LaneStatusMessage = "Success: Lane Added";
            await Task.Delay(2000);
            LaneStatusMessage = "";
        }

        catch (Exception ex)
        {
            LaneStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            LaneStatusMessage = "";
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
            LaneStatusMessage = "Success: Lane Deleted.";
            await Task.Delay(2000);
            LaneStatusMessage = "";
        }
        catch (Exception ex)

        {
            LaneStatusMessage = "Error: Please enter a valid Lane ID. " + ex.Message;
            await Task.Delay(2000);
            LaneStatusMessage = "";
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
                "Plaza " + lane.Plaza_id + " \n" +
                "Lane: " + lane.Lane_number + " \n" +
                "Type: " + lane.Lane_type + " \n" +
                "Direction: "+lane.Lane_direction + " \n"
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
            TechnicianStatusMessage = "Success: Technician Added";
            await Task.Delay(2000);
            TechnicianStatusMessage = "";
        }

        catch (Exception ex)
        {
            TechnicianStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            TechnicianStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteTechnician()
    {
        try
        {
            TollTechnicianRepo.DeleteTechnician(RemoveTechnician);
            RemoveTechnician = "";
            TechnicianStatusMessage = "Success: Technician Deleted.";
            await Task.Delay(2000);
            TechnicianStatusMessage = "";
        }

        catch (Exception ex)
        {
            TechnicianStatusMessage = "Error: Please enter a valid Technician ID. " + ex.Message;
            await Task.Delay(2000);
            TechnicianStatusMessage = "";
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
                    "KNID: " + technician.Technician_kn_id + " \n" +
                    "Name: " + technician.Technician_name + " \n" +
                    "Region: " + technician.Technician_region + " \n" +
                    "Phone: " + technician.Technician_phone_number + " \n" +
                    "Email: " + technician.Technician_email + " \n"
                    );
            }
        }

        catch (Exception ex)
        {
            TechnicianList.Add("No Data Found" + ex.Message);
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
            BomitemStatusMessage = "Success: Bom Item Added";
            await Task.Delay(2000);
            BomitemStatusMessage = "";
        }

        catch (Exception ex)
        {
            BomitemStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            BomitemStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteBomitem()
    {
        try
        {
            TollBomitemRepo.DeleteBomitem(Int32.Parse(RemoveBomitem));
            RemoveBomitem = "";
            BomitemStatusMessage = "Success: Bomitem Deleted.";
            await Task.Delay(2000);
            BomitemStatusMessage = "";
        }

        catch (Exception ex)
        {
            BomitemStatusMessage = "Error: Please enter a valid BOM Item ID. " + ex.Message;
            await Task.Delay(2000);
            BomitemStatusMessage = "";
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
    async void AddNewDuressReason()
    {
        try
        {
            TollDuressReasonRepo.AddDuressReason(NewDuressReason);
            NewDuressReason = "";

            DuressReasonStatusMessage = "Success: Duress Reason Added";
            await Task.Delay(2000);
            DuressReasonStatusMessage = "";
        }

        catch (Exception ex)
        {
            DuressReasonStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            DuressReasonStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteDuressReason()
    {
        if (string.IsNullOrWhiteSpace(RemoveDuressReason))
        {
            DuressReasonStatusMessage = "Error: Please enter valid inputs.";
            await Task.Delay(2000);
            DuressReasonStatusMessage = "";
            return;
        }
        try
        {
            TollDuressReasonRepo.DeleteDuressReason(Int32.Parse(RemoveDuressReason));
            RemoveDuressReason = "";
            DuressReasonStatusMessage = "Success: Duress Reason Deleted.";
            await Task.Delay(2000);
            DuressReasonStatusMessage = "";
        }

        catch (Exception)
        {
            DuressReasonStatusMessage = "Error: Please enter a valid Duress Reason ID.";
            await Task.Delay(2000);
            DuressReasonStatusMessage = "";
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
                    "ID: " + duressreason.Duress_reason_id + " \n" +
                    "Reason: " + duressreason.Duress_reason_name + " \n"
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

            FacilitiesTelecomStatusMessage = "Success: Personale Added";
            await Task.Delay(2000);
            FacilitiesTelecomStatusMessage = "";
            NewFacilitiesTelecomKnID = "";
            NewFacilitiesTelecomName = "";
            NewFacilitiesTelecomPhoneNumber = "";
            NewFacilitiesTelecomAlternatePhoneNumber = "";
            NewFacilitiesTelecomEmail = "";
            NewDepartment = "";
        }

        catch (Exception ex)
        {
            FacilitiesTelecomStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            FacilitiesTelecomStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteFacilitiesTelecom()
    {

        try
        {
            TollFacilitiesTelecomRepo.DeleteFacilitiesTelecom(RemoveFacilitiesTelecom);
            RemoveFacilitiesTelecom = "";
            FacilitiesTelecomStatusMessage = "Success: Personale Deleted.";
            await Task.Delay(2000);
            FacilitiesTelecomStatusMessage = "";
        }

        catch (Exception ex)
        {
            FacilitiesTelecomStatusMessage = "Error: Please enter valid ID. " + ex.Message;
            await Task.Delay(2000);
            FacilitiesTelecomStatusMessage = "";
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
            FacilitiesTelecomList.Add("No Data Found. " + ex.Message);
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

            OrganizationStatusMessage = "Success: Organization Added";
            await Task.Delay(2000);
            OrganizationStatusMessage = "";
            NewOrganizationName = "";
            NewOrganizationPhoneNumber = "";
            NewOrganizationEmail = "";
        }

        catch (Exception ex)
        {
            OrganizationStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            OrganizationStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteOrganization()
    {
        try
        {
            TollOrganizationRepo.DeleteOrgnization(Int32.Parse(RemoveOrganization));
            RemoveOrganization = "";
            OrganizationStatusMessage = "Success: Organization Deleted.";
            await Task.Delay(2000);
            OrganizationStatusMessage = "";
        }

        catch (Exception ex)
        {
            OrganizationStatusMessage = "Error: Please enter a valid Organization ID. " + ex.Message;
            await Task.Delay(2000);
            OrganizationStatusMessage = "";
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
                "ID: " + organization.Organization_id + " \n" +
                "Name: " + organization.Organization_name + " \n" +
                "Number: " + organization.Organization_phone_number + " \n" +
                "Email: " + organization.Organization_email + "\n"
                );

            }
        }

        catch (Exception ex)
        {
            OrganizationList.Add("No Data Found. " + ex.Message);
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

            EmailDistributionStatusMessage = "Success: Email Distribution Added";
            await Task.Delay(2000);
            EmailDistributionStatusMessage = "";
            NewEmailDistributionRegion = "";
            NewEmailDistributionType = "";
            NewEmailDistributionPlazaId = "";
            NewEmailDistributionTo = "";
            NewEmailDistributionCc = "";
        }

        catch (Exception ex)
        {
            EmailDistributionStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            EmailDistributionStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeleteEmailDistribution()
    {
        try
        {
            TollEmailDistributionRepo.DeleteEmailDistribution(Int32.Parse(RemoveEmailDistribution));
            RemoveEmailDistribution = "";
            EmailDistributionStatusMessage = "Success: Email Distribution Deleted.";
            await Task.Delay(2000);
            EmailDistributionStatusMessage = "";
        }

        catch (Exception ex)
        {
            EmailDistributionStatusMessage = "Error: Please enter a valid ID. " + ex.Message;
            await Task.Delay(2000);
            EmailDistributionStatusMessage = "";
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
            NewPersonaleDepartment,
            NewPersonaleRole
            );

            PersonaleStatusMessage = "Success: Personale Added";
            await Task.Delay(2000);
            PersonaleStatusMessage = "";
            NewPersonaleKnId = "";
            NewPersonaleName = "";
            NewPersonalePhoneNumber = "";
            NewPersonaleEmail = "";
            NewPersonaleDepartment = "";
            NewPersonaleRole = "";
        }

        catch (Exception ex)
        {
            PersonaleStatusMessage = "Error: Please enter valid inputs. " + ex.Message;
            await Task.Delay(2000);
            PersonaleStatusMessage = "";
        }
    }

    [RelayCommand]
    async void DeletePersonale()
    {
        try
        {
            TollPersonaleRepo.DeletePersonale(RemovePersonale);
            RemovePersonale = "";
            PersonaleStatusMessage = "Success: Email Distribution Deleted.";
            await Task.Delay(2000);
            PersonaleStatusMessage = "";
        }

        catch (Exception ex)
        {
            PersonaleStatusMessage = "Error: Please enter a valid personale ID. " + ex.Message;
            await Task.Delay(2000);
            PersonaleStatusMessage = "";
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
                "Department: " + personale.Personale_department + " \n" +
                "Role: " + personale.Personale_role + " \n"
                );

            }
        }

        catch (Exception ex)
        {
            PersonaleList.Add("No Data Found. " + ex.Message);
        }
    }
}


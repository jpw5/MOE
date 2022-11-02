
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;

namespace Master_of_Emails.ViewModels;
public partial class MainPageViewModel : ObservableObject
{
    public TollTechnicianRepository TollTechnicianRepo = new();
    public TableQuery<TollTechnician> TollTechnicianQuery;
    public List<TollTechnician> TollTechnician = new();

    public TollFacilitiesTelecomRepository TollFacilitiesTelecomRepo = new();
    public TableQuery<TollFacilitiesTelecom> TollFacilitiesTelecomQuery;
    public List<TollFacilitiesTelecom> TollFacilitiesTelecom = new();

    public TollPersonaleRepository TollPersonaleRepo = new();
    public TableQuery<TollPersonale> TollPersonaleQuery;
    public List<TollPersonale> TollPersonale = new();


    public TollPlazaRepository TollPlazaRepo = new();
    public TableQuery<TollPlaza> TollPlazaQueryByPlazaId;
    public TableQuery<TollPlaza> TollPlazaQueryByPlazaName;

    public TollOrganizationRepository TollOrgnizationRepo = new();
    public TableQuery<TollOrganization> TollOrganizationQuery;
    public List<TollOrganization> TollOrganization = new();

    [ObservableProperty]
    public string personaleSearch;
    [ObservableProperty]
    public string personaleSearchResult = "Search Result Area";

    [ObservableProperty]
    public string plazaSearch;
    [ObservableProperty]
    public string plazaSearchResult = "Search Result Area";

    [ObservableProperty]
    public string organizationSearch;
    [ObservableProperty]
    public string organizationSearchResult = "Search Result Area";

    public MainPageViewModel()
    {

    }

    [RelayCommand]
    public async void ReturnPersonale()
    {
        PersonaleSearchResult = "";
        int PersonaleSearchResultAmount = 0;

        try
        {
            TollTechnicianQuery = TollTechnicianRepo.QueryTechnicianByName(PersonaleSearch);
            TollFacilitiesTelecomQuery = TollFacilitiesTelecomRepo.QueryPersonaleByName(PersonaleSearch);
            TollPersonaleQuery = TollPersonaleRepo.QueryPersonaleByName(PersonaleSearch);

            if (TollTechnicianQuery.Any())
            {

                foreach (TollTechnician personale in TollTechnicianQuery)
                {
                    PersonaleSearchResult += (
                    "Name: " + personale.Technician_name + " \n" +
                    "Phone: " + personale.Technician_phone_number + " \n" +
                    "Email: " + personale.Technician_email + " \n" +
                    "Dpeartment: " + personale.Technician_region + " Technician \n\n");
                    PersonaleSearchResultAmount++;
                    PersonaleSearch = PersonaleSearchResultAmount + " Record(s) Found.";
                }
            }

            if (TollFacilitiesTelecomQuery.Any())
            {

                foreach (TollFacilitiesTelecom personale in TollFacilitiesTelecomQuery)
                {

                    PersonaleSearchResult += (
                    "Name: " + personale.Facilities_telecom_name + " \n" +
                    "Phone: " + personale.Facilities_telecom_phone_number + " \n" +
                    "Alternate Phone: " + personale.Facilities_telecom_alerternate_number + " \n" +
                    "Email: " + personale.Facilities_telecom_email + " \n" +
                    "Dpeartment: " + personale.Department + " \n\n");

                    PersonaleSearchResultAmount++;
                    PersonaleSearch = PersonaleSearchResultAmount + " Record(s) Found.";
                }
            }

            if (TollPersonaleQuery.Any())
            {
                foreach (TollPersonale personale in TollPersonaleQuery)
                {

                    PersonaleSearchResult += (
                    "Name: " + personale.Personale_name + " \n" +
                    "Phone: " + personale.Personale_phone_number + " \n" +
                    "Email: " + personale.Personale_email + " \n" +
                    "Dpeartment: " + personale.Personale_department + " \n" +
                    "Role: " + personale.Personale_role + " \n\n");

                    PersonaleSearchResultAmount++;
                    PersonaleSearch = PersonaleSearchResultAmount + " Record(s) Found.";
                }
            }

            if (!TollTechnicianQuery.Any() && !TollFacilitiesTelecomQuery.Any() && !TollPersonaleQuery.Any())
            {
                PersonaleSearch = "No Record Found.";
                await Task.Delay(2000);
                PersonaleSearch = "";
            }
        }
        catch (Exception)
        {
            PersonaleSearch = ".";
            await Task.Delay(2000);
            PersonaleSearch = "";
        }
    }

    [RelayCommand]
    public async void ReturnPlaza()
    {

        PlazaSearchResult = "";
        int PlazaSearchResultAmount = 0;

        try
        {
            TollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(Int32.Parse(PlazaSearch));

            if (TollPlazaQueryByPlazaId.Any())
            {
                foreach (TollPlaza plaza in TollPlazaQueryByPlazaId)
                {
                    PlazaSearchResult += (
                    "Plaza: " + plaza.Plaza_id + " " + plaza.Plaza_name + " \n" +
                    "Roadway: " + plaza.Plaza_roadway + " \n" +
                    "Mile Post: " + plaza.Plaza_milepost + " \n" +
                    "Phone Number: " + plaza.Plaza_phone_number + " \n" +
                    "Region: " + plaza.Plaza_region + " \n\n");

                    PlazaSearchResultAmount++;
                    PlazaSearch = PlazaSearchResultAmount + " Record(s) Found.";
                }
            }
            else
            {
                PlazaSearch = "No Record Found.";
                await Task.Delay(2000);
                PlazaSearch = "";
            }
        }
        catch (Exception)
        {
            PlazaSearch = "No Record Found By Plaza ID.";
            await Task.Delay(2000);
            PlazaSearch = "";
        }
    }

    [RelayCommand]
    public async void ReturnOrganization()
    {
        OrganizationSearchResult = "";
        int OrganizationSearchResultAmount = 0;

        try
        {
            TollOrganizationQuery = TollOrgnizationRepo.QueryByOrganizationName(OrganizationSearch);

            if (TollOrganizationQuery.Any())
            {
                foreach (TollOrganization organization in TollOrganizationQuery)
                {
                    OrganizationSearchResult += (
                    "Organization: " + organization.Organization_name + " \n" +
                    "Phone Number: " + organization.Organization_phone_number + " \n" +
                    "Email: " + organization.Organization_email + " \n\n");

                    OrganizationSearchResultAmount++;
                    OrganizationSearch = OrganizationSearchResultAmount + " Record(s) Found.";
                }
            }
            else
            {
                OrganizationSearch = "No Record Found.";
                await Task.Delay(2000);
                OrganizationSearch = "";
            }
        }
        catch (Exception)
        {
            OrganizationSearch = "No Record Found.";
            await Task.Delay(2000);
            OrganizationSearch = "";
        }

    }

    [RelayCommand]
    public void ClearPersonaleSearch()
    {
        PersonaleSearch = "";
        PersonaleSearchResult = "Search Result Area";

    }

    [RelayCommand]
    public void ClearPlazaSearch()
    {
        PlazaSearch = "";
        PlazaSearchResult = "Search Result Area";
    }

    [RelayCommand]
    public void ClearOrganizationSearch()
    {
        OrganizationSearch = "";
        OrganizationSearchResult = "Search Result Area";
    }

    [RelayCommand]
    public static void OnPriorityOneMafClicked()
    {
        Shell.Current.GoToAsync("PriorityOneMafPage");
    }

    [RelayCommand]
    public static void OnInconAlertClicked()
    {
        Shell.Current.GoToAsync("InconAlertPage");
    }

    [RelayCommand]
    public static void OnZfoClicked()
    {
        Shell.Current.GoToAsync("ZfoPage");
    }

    [RelayCommand]
    public static void OnDuressAlarmClicked()
    {
        Shell.Current.GoToAsync("DuressAlarmPage");
    }

    [RelayCommand]
    public static void OnScadaClicked()
    {
        Shell.Current.GoToAsync("ScadaPage");
    }

    [RelayCommand]
    public static void OnFiberAlertClicked()
    {
        Shell.Current.GoToAsync("FiberAlertPage");
    }
}


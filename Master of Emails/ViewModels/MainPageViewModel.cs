using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using System.Linq;

namespace Master_of_Emails.ViewModels;
public partial class MainPageViewModel: ObservableObject
{
    public TollTechnicianRepository TollTechnicianRepo = new();
    public TableQuery<TollTechnician> TollTechnicianQuery;
    public List<TollTechnician> TollTechnician = new();

    public TollFacilitiesTelecomRepository TollFacilitiesTelecomRepo = new();
    public TableQuery<TollFacilitiesTelecom> TollFacilitiesTelecomQuery;
    public List<TollFacilitiesTelecom> TollFacilitiesTelecom = new();

    public TollPlazaRepository TollPlazaRepo = new();
    public TableQuery<TollPlaza> TollPlazaQuery;

    [ObservableProperty]
    public string plazaSearch;
    [ObservableProperty]
    public string plazaPhoneResult="Phone: ";
    [ObservableProperty]
    public string plazaNameResult="Name: ";

    [ObservableProperty]
    public string personaleSearch;
    [ObservableProperty]
    public string personalePhoneResult = "Phone: ";
    [ObservableProperty]
    public string personaleEmailResult = "Email: ";

    public List<string> TechnicianList;

    public MainPageViewModel()
    {
        
    }

    [RelayCommand]
    public async void ReturnPersonale()
    {
        List<string> PhoneNumber = new();
        List<string> Email=new();
        List<string> Region = new();
        List<string> FullName = new();

        try
        {
            TollTechnicianQuery = TollTechnicianRepo.QueryTechnicianByName(PersonaleSearch);
            TollFacilitiesTelecomQuery=TollFacilitiesTelecomRepo.QueryPersonaleByName(PersonaleSearch);

            if(TollTechnicianQuery.Any())
            {
                PhoneNumber.Clear();
                Email.Clear();
                FullName.Clear();
                Region.Clear();

                foreach (TollTechnician personale in TollTechnicianQuery)
                {
                    PhoneNumber.Add(personale.Technician_phone_number);
                    Email.Add(personale.Technician_email);
                    FullName.Add(personale.Technician_name);
                    Region.Add(personale.Technician_region);
                    PersonalePhoneResult = "Phone: " + PhoneNumber[0];
                    PersonaleEmailResult = "Email: " + Email[0];
                    PersonaleSearch = FullName[0] + " (" + Region[0] + " Tech)";
                }
            }

            else if(TollFacilitiesTelecomQuery.Any())
            {
                PhoneNumber.Clear();
                Email.Clear();
                FullName.Clear();

                foreach(TollFacilitiesTelecom personale in TollFacilitiesTelecomQuery)
                {
                    PhoneNumber.Add(personale.Facilities_telecom_phone_number+" Alternate: "+personale.Facilities_telecom_alerternate_number);
                    Email.Add(personale.Facilities_telecom_email);
                    FullName.Add(personale.Facilities_telecom_name);
                    PersonalePhoneResult = "Phone: " + PhoneNumber[0];
                    PersonaleEmailResult = "Email: " + Email[0];
                    PersonaleSearch = FullName[0]+" (Facilities/Telecom)";
                }

            }

            else
            {
                PersonalePhoneResult = "Failed to Retrive. The entered Name was invalid or nonexistant.";
                PersonaleEmailResult = "Failed to Retrive. The entered Name was invalid or nonexistant.";
                await Task.Delay(2000);
                PersonalePhoneResult = "Phone: ";
                PersonaleEmailResult = "Name: ";
            }
        }

        catch (Exception)
        {
            PersonalePhoneResult = "Failed to Retrive. The entered Name was invalid or nonexistant.";
            PersonaleEmailResult = "Failed to Retrive. The entered Name was invalid or nonexistant.";
            await Task.Delay(2000);
            PersonalePhoneResult = "Phone: ";
            PersonaleEmailResult = "Name: ";
        }
    }

    [RelayCommand]
    public async void ReturnPlaza()
    {
        try
        {
            TollPlazaQuery = TollPlazaRepo.QueryByPlazaId(Int32.Parse(PlazaSearch));

            if (!TollPlazaQuery.Any())
            {
                PlazaPhoneResult= "Failed to Retrive. The entered ID was invalid or nonexistant.";
                PlazaNameResult= "Failed to Retrive. The entered ID was invalid or nonexistant.";
                await Task.Delay(2000);
                PlazaPhoneResult = "Phone: ";
                PlazaNameResult = "Name: ";
            }

            else
            {
                foreach (TollPlaza plaza in TollPlazaQuery)
                {
                    PlazaPhoneResult = "Phone: " + plaza.Plaza_phone_number;
                    PlazaNameResult = "Plaza: " + plaza.Plaza_name + " " + plaza.Plaza_roadway + " Mile Post " + plaza.Plaza_milepost + " " + plaza.Plaza_region;
                }
            }
        }

        catch (Exception)
        {
            PlazaPhoneResult = "Failed to Retrive. The entered ID was invalid or nonexistant.";
            PlazaNameResult = "Failed to Retrive. The entered ID was invalid or nonexistant";
            await Task.Delay(2000);
            PlazaPhoneResult = "Phone: ";
            PlazaNameResult = "Name: ";
        }
    }

    [RelayCommand]
    public void ClearPersonaleSearch()
    {
        PersonaleSearch = "";
        PersonalePhoneResult = "Phone: ";
        PersonaleEmailResult = "Name: ";
    }

    [RelayCommand]
    public void ClearPlazaSearch()
    {
        PlazaSearch = "";
        PlazaPhoneResult = "Phone: ";
        PlazaNameResult = "Name: ";
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


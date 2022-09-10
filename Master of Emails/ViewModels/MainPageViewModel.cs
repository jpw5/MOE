using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Apis.Compute.v1.Data;
using Master_of_Emails.Database;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;
public partial class MainPageViewModel: ObservableObject
{
    public TollPlazaRepository TollPlazaRepo = new();
    public TableQuery<TollPlaza> TollPlazaQuery;

    public TollTechnicianRepository TollPersonaleRepo = new();
    public TableQuery<TollTechnician> TollPersonaleQuery;

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
    public string personaleEmailResult = "Name: ";



    public MainPageViewModel()
    {

     
    }

    [RelayCommand]
    public async void ReturnPersonale()
    {
        try
        {
            TollPersonaleQuery = TollPersonaleRepo.QueryTechnicianByName(PersonaleSearch);

            if(!TollPersonaleQuery.Any())
            {
                PersonalePhoneResult = "Failed to Retrive. The entered ID was invalid or nonexistant";
                PersonaleEmailResult = "Failed to Retrive. The entered ID was invalid or nonexistant.";
                await Task.Delay(2000);
                PersonalePhoneResult = "Phone: ";
                PersonaleEmailResult = "Name: ";
            }

            else
            {
                foreach (TollTechnician personale in TollPersonaleQuery)
                {
                    PersonalePhoneResult = "Phone: " + personale.Technician_phone_number;
                    PersonaleEmailResult = "Email: " + personale.Technician_email;
                }
            }
        }

        catch (Exception)
        {
            PersonalePhoneResult = "Failed to Retrive. The entered ID was invalid or nonexistant";
            PersonaleEmailResult = "Failed to Retrive. The entered ID was invalid or nonexistant.";
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
                PlazaPhoneResult= "Failed to Retrive. The entered ID was invalid or nonexistant";
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


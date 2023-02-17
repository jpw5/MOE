﻿
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;

public partial class ScadaPageViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<string> tollRegionList;
    [ObservableProperty]
    public string region;
    public TollRegionRepository TollRegionRepo = new();
    public List<TollRegion> TollRegion = new();

    [ObservableProperty]
    public ObservableCollection<string> tollPlazaList;
    [ObservableProperty]
    public string plaza;
    public TollPlazaRepository TollPlazaRepo = new();
    public List<TollPlaza> TollPlaza = new();

    [ObservableProperty]
    public string contact;
    [ObservableProperty]
    public string phoneResult;
    [ObservableProperty]
    public string alternatePhoneResult;
    public TollFacilitiesTelecomRepository TollFacilitiesTelecomRepo = new();
    public TableQuery<TollFacilitiesTelecom> TollFacilitiesTelecomQuery;

    public TollOrganizationRepository TollOrgnizationRepo = new();
    public TableQuery<TollOrganization> TollOrganizationQuery;

    [ObservableProperty]
    public string scadaAlarm;

    [ObservableProperty]
    public string buildingNumber;

    [ObservableProperty]
    public string workOrderNumber;

    [ObservableProperty]
    public string date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");

    [ObservableProperty]
    public string temperature = "NA";


    public ScadaPageViewModel()
    {
        tollRegionList = new ObservableCollection<string>();
        tollPlazaList = new ObservableCollection<string>();
        PopulateRegionList();
    }

    public void PopulateRegionList()
    {
        TollRegion = TollRegionRepo.GetRegions();
        if (TollRegion != null)
        {
            foreach (TollRegion region in TollRegion)
            {
                tollRegionList.Add(region.Region_name);
            }
        }
    }

    [RelayCommand]
    public async void ReturnPersonale()
    {
        TollFacilitiesTelecomQuery = TollFacilitiesTelecomRepo.QueryPersonaleByName(Contact);
        TollOrganizationQuery = TollOrgnizationRepo.QueryByOrganizationName(Contact);

        List<string> PhoneNumber = new();
        List<string> AlternatePhoneNumber = new();
        List<string> FullName = new();

        if (TollFacilitiesTelecomQuery.Any())
        {
            PhoneNumber.Clear();
            AlternatePhoneNumber.Clear();
            FullName.Clear();
            foreach (TollFacilitiesTelecom facilitiestelecom in TollFacilitiesTelecomQuery)
            {
                PhoneNumber.Add(facilitiestelecom.Facilities_telecom_phone_number);
                AlternatePhoneNumber.Add(facilitiestelecom.Facilities_telecom_alerternate_number);
                FullName.Add(facilitiestelecom.Facilities_telecom_name);
                PhoneResult = "Phone: " + PhoneNumber[0];
                AlternatePhoneResult = "Alternate: " + AlternatePhoneNumber[0];
                Contact = FullName[0];

            }
        }

        else if (TollOrganizationQuery.Any())
        {
            PhoneNumber.Clear();
            FullName.Clear();

            foreach (TollOrganization organization in TollOrganizationQuery)
            {
                PhoneNumber.Add(organization.Organization_phone_number);
                FullName.Add(organization.Organization_name);
                PhoneResult = "Phone: " + PhoneNumber[0];
                Contact = FullName[0];
            }
        }

        else
        {
            PhoneResult = "Failed to Retrive. The entered Name was invalid or nonexistant";
            await Task.Delay(2000);
            PhoneResult = "";
        }

    }

    [RelayCommand]
    public void Clear()
    {
        tollRegionList?.Clear();
        PopulateRegionList();
        tollPlazaList?.Clear();
        Contact = "";
        PhoneResult = "";
        AlternatePhoneResult = "";
        ScadaAlarm = "";
        BuildingNumber = "";
        WorkOrderNumber = "";
        Temperature = "NA";
        Date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");
    }

}


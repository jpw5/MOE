﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;

public partial class InconAlertPageViewModel : ObservableObject
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
    public ObservableCollection<string> tollLaneList;
    [ObservableProperty]
    public string lane;
    public TollLaneRepository TollLaneRepo = new();
    public List<TollLane> TollLane = new();

    [ObservableProperty]
    public string requestor;
    [ObservableProperty]
    public string phoneResult;
    public TollTechnicianRepository TollTechnicianRepo = new();
    public TableQuery<TollTechnician> TollTechnicianQuery;

    public TollOrganizationRepository TollOrgnizationRepo = new();
    public TableQuery<TollOrganization> TollOrganizationQuery;

    [ObservableProperty]
    public string date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");

    [ObservableProperty]
    public string duration;

    [ObservableProperty]
    public string incidentOrESR = "NA";

    [ObservableProperty]
    public string reason;

    [ObservableProperty]
    public string unit;

    public InconAlertPageViewModel()
    {
        tollRegionList = new ObservableCollection<string>();
        tollPlazaList = new ObservableCollection<string>();
        tollLaneList = new ObservableCollection<string>();
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
        TollTechnicianQuery = TollTechnicianRepo.QueryTechnicianByName(Requestor);
        TollOrganizationQuery = TollOrgnizationRepo.QueryByOrganizationName(Requestor);

        List<string> PhoneNumber = new();
        List<string> FullName = new();

        if (TollTechnicianQuery.Any())
        {
            PhoneNumber.Clear();
            FullName.Clear();
            foreach (TollTechnician personale in TollTechnicianQuery)
            {
                PhoneNumber.Add(personale.Technician_phone_number);
                FullName.Add(personale.Technician_name);
                PhoneResult = "Phone: " + PhoneNumber[0];
                Requestor = FullName[0];
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
                Requestor = FullName[0];
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
        tollLaneList?.Clear();
        Requestor = "";
        PhoneResult = "";
        Duration = "";
        IncidentOrESR = "";
        Reason = "";
        Date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");
    }
}


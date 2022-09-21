using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;

    public partial class InconAlertPageViewModel : ObservableObject
    {

        public TollTechnicianRepository TollPersonaleRepo = new();
        public TableQuery<TollTechnician> TollPersonaleQuery;

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
        public string personaleSearch;
        [ObservableProperty]
        public string personalePhoneResult;

        [ObservableProperty]
        public DateTime date = DateTime.Now;

        [ObservableProperty]
        public string duration;

        [ObservableProperty]
        public string incidentOrESR;

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
        TollPersonaleQuery = TollPersonaleRepo.QueryTechnicianByName(PersonaleSearch);

        if (!TollPersonaleQuery.Any())
        {
            PersonalePhoneResult = "Failed to Retrive. The entered Name was invalid or nonexistant";
            await Task.Delay(2000);
            PersonalePhoneResult = "";  
        }

        else
        {
            foreach (TollTechnician personale in TollPersonaleQuery)
            {
                PersonalePhoneResult = "Phone: " + personale.Technician_phone_number;
            }
        }
    }

    [RelayCommand]
    public void Clear()
    {
        tollRegionList.Clear();
        PopulateRegionList();
        tollPlazaList.Clear();
        tollLaneList.Clear();
        PersonaleSearch = "";
        PersonalePhoneResult = "";
        Duration = "";
        IncidentOrESR = "";
        Reason = "";
        Date= DateTime.Now;
    }


}


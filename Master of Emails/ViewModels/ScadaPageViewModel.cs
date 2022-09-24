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
        public ObservableCollection<string> tollScadaAlarmList;
        [ObservableProperty]
        public string scadaAlarm;
        public TollScadaAlarmRepository TollScadaAlarmRepo = new();
        public List<TollScadaAlarm> TollScadaAlarm = new();

        [ObservableProperty]
        public string requestor;
        [ObservableProperty]
        public string phoneResult;
        public TollFacilitiesTelecomRepository TollFacilitiesTelecomRepo = new();
        public TableQuery<TollFacilitiesTelecom> TollFacilitiesTelecom;

        [ObservableProperty]
        public string buildingNumber;

        [ObservableProperty]
        public string alarm;

        [ObservableProperty]
        public string date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");


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

}


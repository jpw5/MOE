using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;
    public partial class DuressAlarmPageViewModel: ObservableObject
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
        public ObservableCollection<string> tollDuressReasonList;
        public List<TollDuressReason> TollDuressReason= new();
        public TollDuressReasonRepository TollDuressReasonRepo=new();   
        [ObservableProperty]
        public string duressReason;

        [ObservableProperty]
        public string date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");

        [ObservableProperty]
        public string plazaSupervisor;

    public DuressAlarmPageViewModel()
    {
        tollRegionList = new ObservableCollection<string>();
        tollPlazaList = new ObservableCollection<string>();
        tollLaneList = new ObservableCollection<string>();
        tollDuressReasonList = new ObservableCollection<string>();
        PopulateRegionList();
        PopulateDuressReasonList();
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

    public void PopulateDuressReasonList()
    {
        
        TollDuressReason=TollDuressReasonRepo.GetDuressReasons();
        if (TollDuressReasonRepo != null)
        {
            foreach(TollDuressReason duressreason in TollDuressReason)
            {
                tollDuressReasonList.Add(duressreason.Duress_reason_name);
            }
        }
    }

    [RelayCommand]
    public void Clear()
    {
        tollRegionList?.Clear();
        PopulateRegionList();
        tollPlazaList?.Clear();
        tollLaneList?.Clear();
        tollDuressReasonList?.Clear();
        PlazaSupervisor = "";
        Date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");
    }
}


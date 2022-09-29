using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;

public partial class PriorityOneMafPageViewModel : ObservableObject
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
    public ObservableCollection<string> tollBomitemList;
    [ObservableProperty]
    public string bomitem;
    public TollBomitemRepository TollBomitemRepo = new();
    public List<TollBomitem> TollBomitem = new();

    [ObservableProperty]
    public ObservableCollection<string> tollTechnicianList;
    [ObservableProperty]
    public string technician;
    public TollTechnicianRepository TollTechnicianRepo = new();
    public List<TollTechnician> TollTechnician = new();
    public TableQuery<TollTechnician> TollTechnicianQuery;

    [ObservableProperty]
    public string date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");

    [ObservableProperty]
    public string problem;

    [ObservableProperty]
    public string mafNumber;

    [ObservableProperty]
    public string actionTaken;

    public PriorityOneMafPageViewModel()
    {
        tollRegionList = new ObservableCollection<string>();
        tollPlazaList = new ObservableCollection<string>();
        tollLaneList = new ObservableCollection<string>();
        tollTechnicianList=new ObservableCollection<string>();
        tollBomitemList=new ObservableCollection<string>();
        PopulateRegionList();
    }
    public void PopulateRegionList()
    {
        TollRegion= TollRegionRepo.GetRegions();
        if(TollRegion!=null)
        {
            foreach (TollRegion region in TollRegion)
            {
                tollRegionList.Add(region.Region_name);
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
        tollBomitemList?.Clear();
        tollTechnicianList?.Clear();
        Date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");
        Problem= "";
        MafNumber= "";
        ActionTaken= "";
    }

}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;

public partial class PriorityOneMafPageViewModel : ObservableObject
{
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

    public PriorityOneMafPageViewModel()
    {
        tollPlazaList = new ObservableCollection<string>();
        tollLaneList = new ObservableCollection<string>();
        PopulatePlazaList();
        
    }

    public void PopulatePlazaList()
    {
        TollPlaza = TollPlazaRepo.GetPlazas();
        foreach(TollPlaza plaza in TollPlaza)
        {
            tollPlazaList.Add(plaza.Plaza_id.ToString()+" "+plaza.Plaza_name);
        }
    }
}

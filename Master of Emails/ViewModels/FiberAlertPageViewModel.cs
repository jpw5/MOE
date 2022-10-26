using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using System.Collections.ObjectModel;

namespace Master_of_Emails.ViewModels;

public partial class FiberAlertPageViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<string> tollRegionList;
    [ObservableProperty]
    public string region;
    public TollRegionRepository TollRegionRepo = new();
    public List<TollRegion> TollRegion = new();

    [ObservableProperty]
    public string milePost;

    [ObservableProperty]
    public string affectedArea;

    [ObservableProperty]
    public string who;

    [ObservableProperty]
    public string amountCut;

    [ObservableProperty]
    public string fiberTechnician;

    [ObservableProperty]
    public string reportedBy;

    [ObservableProperty]
    public string phoneNumber;

    [ObservableProperty]
    public string startDate = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");

    [ObservableProperty]
    public string endDate = "TBD";

    public FiberAlertPageViewModel()
    {
        tollRegionList = new ObservableCollection<string>();
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
    public void Clear()
    {
        tollRegionList?.Clear();
        PopulateRegionList();
        MilePost = "";
        AffectedArea = "";
        Who = "";
        AmountCut = "";
        FiberTechnician = "";
        ReportedBy = "";
        PhoneNumber = "";
        StartDate = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");
    }



}


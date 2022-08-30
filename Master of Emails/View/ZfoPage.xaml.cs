using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using practice.Pages;
using System.Collections;
using System.Linq;

namespace practice.Pages;

public partial class ZfoPage : ContentPage
{

    public TollsRegionRepository TollsRegionRepo = new();
    public List<TollsRegion> TollRegionList;
   
    public ZfoPage()
	{
    InitializeComponent();

        TollRegionList=TollsRegionRepo.GetRegions();
        var RegionList = new List<String>
        {
            " "
        };

        foreach (var Reg in TollRegionList)
        {
            RegionList.Add(Reg.Region_name);
        }
        selectRegion.ItemsSource = RegionList;

    }

   
}



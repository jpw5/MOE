using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using practice.Pages;
using System.Collections;
using System.Linq;

namespace practice.Pages;


public partial class PriorityOneMafPage : ContentPage
{
    public TollPlazaRepository TollPlazaRepo = new();
    public List<TollPlaza> TollPlazaList=new();
    public String[] PlazaId;
    public String PlazaName;
    public String Item;
    

    public PriorityOneMafPage()
	{
		InitializeComponent();

        TollPlazaList = TollPlazaRepo.GetPlazas();
        var TollPlaza = new List<String>
        {
            " "
        };

        foreach (var Reg in TollPlazaList)
        {
            TollPlaza.Add(Reg.Plaza_id+" "+Reg.Plaza_name);
        }
        selectPlaza.ItemsSource = TollPlaza;
    }

    void PlazaSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Item = (string)args.SelectedItem;
        PlazaId = Item.Split(' ');
        //DisplayAlert("Failed to Retrive", "Please enter a valid Plaza ID Number Or The Plaza ID does not exist. " + PlazaId[0],  "Close");

    }
}
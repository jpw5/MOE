using Master_of_Emails.Components;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class ZfoPage : ContentPage
{
    readonly SharedComponents SharedComponents = new();

    public ZfoPage(ZfoPageViewModel zfoPageViewModel)
    {
        InitializeComponent();
        BindingContext = zfoPageViewModel;
    }

    private void ZFOEmail_Button_Pressed(object sender, EventArgs e)
    {
        
        bool CheckZFOInputs=SharedComponents.
             CheckZFOInputs(selectRegion.SelectedItem, selectPlaza.SelectedItem,
             SharedComponents.TollLaneList, selectRequestor.Text, selectReason.Text, selectStartDate.Text, 
             selectEndDate.Text);

       if(CheckZFOInputs==true)
        {
            SharedComponents.Plaza = selectPlaza.SelectedItem.ToString();
            for (int i = 0; i < SharedComponents.TollLaneList.Count; i++)
            {
                SharedComponents.Lane += SharedComponents.TollLaneList[i] + " ";
            }

            SharedComponents.Region = selectRegion.SelectedItem.ToString();
            SharedComponents.Requestor = selectRequestor.Text;
            SharedComponents.Reason = selectReason.Text;
            SharedComponents.StartDate = selectStartDate.Text;
            SharedComponents.EndDate = selectEndDate.Text;

            SharedComponents.To = "";
            SharedComponents.Cc = "";

            SharedComponents.StandardDistributionZFO = (
            SharedComponents.
            TollEmailDistributionRepo.
            QueryByRegionEmailTypeAndPlazaId(SharedComponents.Region, SharedComponents.EmailTypeZFO, "ALL"));

            foreach (TollEmailDistribution emaildistributionZFO in SharedComponents.StandardDistributionZFO)
            {
                SharedComponents.To = emaildistributionZFO.Email_distribution_to;
                SharedComponents.Cc = emaildistributionZFO.Email_distribution_cc;
            }

            SharedComponents.Subject = "SunWatch ZFO Alert - " + SharedComponents.Plaza.ToUpper() + " / " + SharedComponents.Lane.ToUpper();
            SharedComponents.Body = "<font size=5>" + "<b>" + "****SunWatch ZFO Alert****" + "</b>" + "</font>" + "<br>" + "<br>" +
            "<font size=4>" + "<b>" + "Plaza: " + "</b>" + SharedComponents.Plaza + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Lane(s): " + "</b>" + SharedComponents.Lane + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Requestor: " + "</b>" + SharedComponents.Requestor + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Reason: " + "</b>" + SharedComponents.Reason + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Start Date/Time: " + "</b>" + SharedComponents.StartDate + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "End Date/Time: " + "</b>" + SharedComponents.EndDate + "</font>" + "<br>";

            try
            {
                SharedComponents.Mail = (Outlook.MailItem)SharedComponents.ObjApp.
                CreateItemFromTemplate(SharedComponents.Template);
                SharedComponents.Mail.To = SharedComponents.To;
                SharedComponents.Mail.CC = SharedComponents.Cc;
                SharedComponents.Mail.Subject = SharedComponents.Subject;
                SharedComponents.Mail.HTMLBody = SharedComponents.Body;
                SharedComponents.Mail.Display();
                SharedComponents.Mail = null;
                SharedComponents.Lane = null;
            }

            catch (Exception ex)
            {
                DisplayAlert("Alert", "Close MOE, make sure Outlook is running, and try again. " + ex.Message, "close");
            }
        }

        else
        {
            DisplayAlert("Alert", "One or more inputs are empty", "Close");
        }

   

    }
    private void SelectRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectRegion.SelectedIndex;
        List<string> TollPlazas = new();

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            SharedComponents.Region = selectRegion.Items[selectedIndex];
            TollPlazas.Clear();

            SharedComponents.TollPlazaQueryByRegionName = SharedComponents.TollPlazaRepo.
            QueryByRegionName(SharedComponents.Region);

            foreach (TollPlaza tollPlaza in SharedComponents.TollPlazaQueryByRegionName)
            {
                if (tollPlaza.Plaza_company != "Infinity")
                {
                    TollPlazas.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name);
                }
            }

            TollPlazas.Sort();
            foreach (string tollPlaza in TollPlazas)
            {
                selectPlaza.ItemsSource.Add(tollPlaza);
            }
        }
    }
    private void SelectPlaza_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectPlaza.SelectedIndex;
        List<string> TollLanes = new();
        
        if (selectedIndex != -1)
        {
            selectLane.ItemsSource = null;
            TollLanes.Clear();

            var Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            SharedComponents.PlazaId = Int32.Parse(Split[0]);

            TollLanes = SharedComponents.GetLanes(selectPlaza.Items[selectedIndex], SharedComponents.PlazaId);

            TollLanes.Sort();
            selectLane.ItemsSource = TollLanes;
        }
    }
    private void SelectLane_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        else
        {
            SharedComponents.TollLaneList.Clear();
            for (int i = 0; i < e.CurrentSelection.Count; i++)
            {
                SharedComponents.TollLaneList.Add(e.CurrentSelection[i].ToString());
            }
        }
    }
}



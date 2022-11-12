using Master_of_Emails.Components;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class InconAlertPage : ContentPage
{
    readonly SharedComponents SharedComponents = new();

    public InconAlertPage(InconAlertPageViewModel inconAlertPageViewModel)
    {
        InitializeComponent();
        BindingContext = inconAlertPageViewModel;
    }

    private void InconAlertEmail_Button_Pressed(object sender, EventArgs e)
    {
      

        bool CheckInconAlertInputs = 
             SharedComponents.CheckInconAlertInputs(selectRegion.SelectedItem, selectPlaza.SelectedItem, 
             SharedComponents.TollLaneList, selectRequestor.Text, selectPhoneNumber.Text, selectReason.Text, 
             selectStartDate.Text, selectIncidentOrESR.Text, selectDuration.Text, SharedComponents.Units);

        if(CheckInconAlertInputs==true)
        {
            SharedComponents.Region = selectRegion.SelectedItem.ToString();
            for (int i = 0; i < SharedComponents.TollLaneList.Count; i++)
            {
                SharedComponents.Lane += SharedComponents.TollLaneList[i] + " ";
            }
            SharedComponents.StartDate = selectStartDate.Text;
            SharedComponents.Requestor = selectRequestor.Text;
            SharedComponents.PhoneNumber = selectPhoneNumber.Text;
            SharedComponents.Duration = selectDuration.Text;
            SharedComponents.Reason = selectReason.Text;

            SharedComponents.To = "";
            SharedComponents.Cc = "";
            SharedComponents.StandardDistributionIncon =
            SharedComponents.TollEmailDistributionRepo.QueryByRegionEmailTypeAndPlazaId(SharedComponents.Region,
            SharedComponents.EmailTypeIncon, "ALL");

            foreach (TollEmailDistribution standarddistributionIncon in SharedComponents.StandardDistributionIncon)
            {
                SharedComponents.To = standarddistributionIncon.Email_distribution_to;
                SharedComponents.Cc = standarddistributionIncon.Email_distribution_cc;
            }

            SharedComponents.Subject = "InConAlert for Plaza - " + SharedComponents.Plaza.ToUpper() + " / " + 
            SharedComponents.Lane.ToUpper();

            SharedComponents.Body = "<font size=5>" + "<b>" + "****SunWatch InConAlert****" + "</b>" + "</font>" + 
            "<br>" + "<br>" + "<font size=4>" + "<b>" + "Plaza: " + "</b>" + SharedComponents.Plaza + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Roadway: " + "</b>" + SharedComponents.Roadway + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Lane: " + "</b>" + SharedComponents.Lane + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Date/Time Contacted: " + "</b>" + SharedComponents.StartDate + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Requestor: " + "</b>" + SharedComponents.Requestor + " / " + SharedComponents.PhoneNumber + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Duration of Work: " + "</b>" + SharedComponents.Duration + " " + SharedComponents.Units + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Reason: " + "</b>" + SharedComponents.Reason + "</font>" + "<br>";

            try
            {
                SharedComponents.Mail = (Outlook.MailItem)SharedComponents.
                ObjApp.CreateItemFromTemplate(SharedComponents.Template);
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
        List<string> plazas = new();

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            SharedComponents.Region = selectRegion.Items[selectedIndex];
            plazas.Clear();
            SharedComponents.TollPlazaQueryByRegionName = 
            SharedComponents.TollPlazaRepo.QueryByRegionName(SharedComponents.Region);

            foreach (TollPlaza tollPlaza in SharedComponents.TollPlazaQueryByRegionName)
            {
                plazas.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name);
            }

            plazas.Sort();
            foreach (string tollPlaza in plazas)
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
            SharedComponents.PlazaName = Split[1];

            SharedComponents.TollPlazaQueryByPlazaName =
            SharedComponents.TollPlazaRepo.QueryByPlazaName(SharedComponents.PlazaName);
            foreach (TollPlaza plaza in SharedComponents.TollPlazaQueryByPlazaName)
            {
                SharedComponents.Plaza = plaza.Plaza_id + " " + plaza.Plaza_name;
                SharedComponents.Roadway = plaza.Plaza_roadway;
            }

            TollLanes = SharedComponents.GetLanes();
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
    private void HoursRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SharedComponents.Units = "Hours";
    }
    private void MinuetsRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SharedComponents.Units = "Minuets";
    }
}
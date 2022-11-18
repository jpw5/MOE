using Master_of_Emails.Components;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using System.Security.Claims;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class DuressAlarmPage : ContentPage
{
    readonly SharedComponents SharedComponents = new();

    public DuressAlarmPage(DuressAlarmPageViewModel duressAlarmPageViewModel)
    {
        InitializeComponent();
        BindingContext = duressAlarmPageViewModel;
    }

    private void DuressAlarmEmailButton_Pressed(object sender, EventArgs e)
    {
        bool CheckDuressAlarmInputs=SharedComponents.CheckDuressAlarmInputs(selectRegion.SelectedItem, 
             selectPlaza.SelectedItem, selectLane.SelectedItem, selectDuressReason.SelectedItem, 
             SharedComponents.Alarm, selectPlazaSupervisor.Text, selectStartDate.Text);

        if(CheckDuressAlarmInputs==true)
        {
            SharedComponents.Region = selectRegion.SelectedItem.ToString();
            SharedComponents.Lane = selectLane.SelectedItem.ToString();
            SharedComponents.DuressReason = selectDuressReason.SelectedItem.ToString();
            SharedComponents.StartDate = selectStartDate.Text;
            SharedComponents.PlazaSupervisor = selectPlazaSupervisor.Text;

            SharedComponents.StandardDistributionDuress =
            SharedComponents.TollEmailDistributionRepo.
            QueryByRegionEmailTypeAndPlazaId(SharedComponents.Region, SharedComponents.EmailTypeDuress, 
            SharedComponents.PlazaId.ToString());

            foreach (TollEmailDistribution emaildistributionDuress in SharedComponents.StandardDistributionDuress)
            {
                SharedComponents.To = emaildistributionDuress.Email_distribution_to;
                SharedComponents.Cc = emaildistributionDuress.Email_distribution_cc;
            }

            SharedComponents.Subject = "Duress Alarm at " + SharedComponents.Plaza.ToUpper() + " / " + SharedComponents.Lane.ToUpper();
            SharedComponents.Body = "<font size=5>" + "<b>" + "****SunWatch Duress Alarm****" + "</b>" + "</font>" + "<br>" + "<br>" +
            "<font size=4>" + "<b>" + "Plaza: " + "</b>" + SharedComponents.Plaza + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Roadway: " + "</b>" + SharedComponents.Roadway + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Lane(s): " + "</b>" + SharedComponents.Lane + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Date/Time: " + "</b>" + SharedComponents.StartDate + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Alarm: " + "</b>" + SharedComponents.Alarm + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Supervisor: " + "</b>" + SharedComponents.PlazaSupervisor + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Reason: " + "</b>" + SharedComponents.DuressReason + "</font>" + "<br>";

            try
            {
                SharedComponents.Mail = (Outlook.MailItem)SharedComponents.ObjApp.CreateItemFromTemplate(SharedComponents.Template);
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
            SharedComponents.TollPlazaQueryByRegionName = SharedComponents.TollPlazaRepo.QueryByRegionName(SharedComponents.Region);
            foreach (TollPlaza tollPlaza in SharedComponents.TollPlazaQueryByRegionName)
            {
                if (tollPlaza.Plaza_company != "Infinity")
                {
                    plazas.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name);
                }
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
            selectLane.ItemsSource.Clear();
            TollLanes.Clear();

            var Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            SharedComponents.PlazaId = Int32.Parse(Split[0]);
            SharedComponents.TollPlazaQueryByPlazaId = SharedComponents.TollPlazaRepo.QueryByPlazaId(SharedComponents.PlazaId);

            foreach (TollPlaza plaza in SharedComponents.TollPlazaQueryByPlazaId)
            {
                SharedComponents.Plaza = plaza.Plaza_id + " " + plaza.Plaza_name;
                SharedComponents.Roadway = plaza.Plaza_roadway;
            }

            TollLanes = SharedComponents.GetLanes();
            TollLanes.Sort();
            foreach (string tollLane in TollLanes)
            {
                selectLane.ItemsSource.Add(tollLane);
            }

        }
    }
    private void VaultRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SharedComponents.Alarm = "Vault Under Duress";
    }

    private void LaneRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SharedComponents.Alarm = "Lane Duress Alarm";
    }
}
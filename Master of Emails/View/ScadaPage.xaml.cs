using Master_of_Emails.Components;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace practice.Pages;

public partial class ScadaPage : ContentPage
{
    readonly SharedComponents SharedComponents = new();

    public ScadaPage(ScadaPageViewModel scadaPageViewModel)
    {
        InitializeComponent();
        BindingContext = scadaPageViewModel;
    }
    private void ScadaEmailButton_Pressed(object sender, EventArgs e)
    {

        bool CheckScadaInputs = SharedComponents.CheckScadaInputs(selectRegion.SelectedItem,
             selectPlaza.SelectedItem, SharedComponents.SelectedHours, selectContact.Text,
             selectPhoneNumber.Text, selectScadaAlarm.Text, selectBuildingNumber.Text,
             selectWorkOrderNumber.Text, selectStartDate.Text, selectTemperature.Text);

        if(CheckScadaInputs==true)
        {
            SharedComponents.Region = selectRegion.SelectedItem.ToString();
            SharedComponents.TollPlazaQueryByPlazaId = SharedComponents.TollPlazaRepo.QueryByPlazaId(SharedComponents.PlazaId);
            foreach (TollPlaza plaza in SharedComponents.TollPlazaQueryByPlazaId)
            {
                SharedComponents.Plaza = plaza.Plaza_id + " " + plaza.Plaza_name;
                SharedComponents.Roadway = plaza.Plaza_roadway;
                SharedComponents.MilePost = plaza.Plaza_milepost.ToString();
                SharedComponents.PlazaCompany = plaza.Plaza_company;
            }

            SharedComponents.BuildingNumber = selectBuildingNumber.Text;
            SharedComponents.Alarm = selectScadaAlarm.Text;
            SharedComponents.WorkOrderNumber = selectWorkOrderNumber.Text;
            SharedComponents.StartDate = selectStartDate.Text;
            SharedComponents.Temperature = selectTemperature.Text;
            SharedComponents.Contact = selectContact.Text;
            SharedComponents.PhoneNumber = selectPhoneNumber.Text;

            if (SharedComponents.PlazaCompany == "Infinity")
            {
                SharedComponents.StandardDistributionScadaInfinity =
                SharedComponents.TollEmailDistributionRepo.QueryByRegionEmailTypeAndPlazaId
                (SharedComponents.Region, SharedComponents.EmailTypeSCADA, SharedComponents.PlazaCompany);

                foreach (TollEmailDistribution emaildistributionSCADA in SharedComponents.StandardDistributionScadaInfinity)
                {
                    SharedComponents.To = emaildistributionSCADA.Email_distribution_to;
                    SharedComponents.Cc = emaildistributionSCADA.Email_distribution_cc;
                }
            }

            else
            {
                SharedComponents.StandardDistributionScadaAll =
                SharedComponents.TollEmailDistributionRepo.QueryByRegionEmailTypeAndPlazaId(SharedComponents.Region, SharedComponents.EmailTypeSCADA, "ALL");

                foreach (TollEmailDistribution emaildistributionSCADA in SharedComponents.StandardDistributionScadaAll)
                {
                    SharedComponents.To = emaildistributionSCADA.Email_distribution_to;
                    SharedComponents.Cc = emaildistributionSCADA.Email_distribution_cc;
                }
            }

            SharedComponents.Subject = "SCADA Alarm - " + SharedComponents.Plaza.ToUpper();
            SharedComponents.Body = "<font size=5>" + "<b>" + "****SunWatch SCADA Alarm - " + SharedComponents.SelectedHours + "*****" + "</b>" + "</font>" + "<br>" + "<br>" +
            "<font size=4>" + "<b>" + "Plaza: " + "</b>" + SharedComponents.Plaza + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Roadway: " + "</b>" + SharedComponents.Roadway + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Building Number: " + "</b>" + SharedComponents.BuildingNumber + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Alarm: " + "</b>" + SharedComponents.Alarm + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Contact: " + "</b>" + SharedComponents.Contact + " / " + SharedComponents.PhoneNumber + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Date/Time Contacted: " + "</b>" + SharedComponents.StartDate + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Mile Post: " + "</b>" + SharedComponents.MilePost + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Work Order #: " + "</b>" + SharedComponents.WorkOrderNumber + "</font>" + "<br>";

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
        List<string> plazas = new();

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            SharedComponents.Region = selectRegion.Items[selectedIndex];
            plazas.Clear();
            SharedComponents.TollPlazaQueryByRegionName = SharedComponents.TollPlazaRepo.QueryByRegionName(SharedComponents.Region);
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
        if (selectedIndex != -1)
        {
            var Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            SharedComponents.PlazaId = Int32.Parse(Split[0]);
        }
    }

    private void After_hours_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SharedComponents.SelectedHours = "After Hours";
        selectWorkOrderNumber.Text = "";
    }

    private void Normal_hours_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SharedComponents.SelectedHours = "Normal Hours";
        selectWorkOrderNumber.Text = "NA";
    }

}
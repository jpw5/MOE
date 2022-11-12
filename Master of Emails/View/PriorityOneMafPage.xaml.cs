using Master_of_Emails.Components;
using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class PriorityOneMafPage : ContentPage
{
    readonly SharedComponents SharedComponents = new();

    public PriorityOneMafPage(PriorityOneMafPageViewModel priorityOneMafPageViewModel)
    {
        InitializeComponent();
        BindingContext = priorityOneMafPageViewModel;
    }

    private void PriorityOneEmail_Button_Pressed(object sender, EventArgs e)
    {
        bool CheckPriorityOneInputs=
        SharedComponents.CheckPriorityOneInputs(selectRegion.SelectedItem, selectPlaza.SelectedItem, 
        selectLane.SelectedItem, selectBomitem.SelectedItem, selectTechnician.SelectedItem, selectMafNumber.Text, 
        selectProblem.Text, selectActionTaken.Text);

        if(CheckPriorityOneInputs==true)
        {
            SharedComponents.Region = selectRegion.SelectedItem.ToString();
            SharedComponents.Lane = selectLane.SelectedItem.ToString();
            SharedComponents.Bomitem = selectBomitem.SelectedItem.ToString();
            SharedComponents.Technician = selectTechnician.SelectedItem.ToString();
            SharedComponents.StartDate = selectStartDate.Text;
            SharedComponents.MafNumber = selectMafNumber.Text;
            SharedComponents.Problem = selectProblem.Text;
            SharedComponents.ActionTaken = selectActionTaken.Text;
            SharedComponents.To = "";
            SharedComponents.Cc = "";

            SharedComponents.StandardDistributionP1 = SharedComponents.TollEmailDistributionRepo.
            QueryByRegionEmailTypeAndPlazaId(SharedComponents.Region, SharedComponents.EmailTypeP1, "ALL");

            SharedComponents.StandardDistributionP1PlazaId = SharedComponents.TollEmailDistributionRepo.
            QueryByPlazaId(SharedComponents.Roadway);

            if (SharedComponents.StandardDistributionP1PlazaId.Any())
            {
                foreach (TollEmailDistribution tollEmailDistribution in SharedComponents.StandardDistributionP1PlazaId)
                {
                    SharedComponents.To = tollEmailDistribution.Email_distribution_to;
                    SharedComponents.Cc = tollEmailDistribution.Email_distribution_cc;
                }
            }

            else
            {
                foreach (TollEmailDistribution tollEmailDistribution in SharedComponents.StandardDistributionP1)
                {
                    SharedComponents.To = tollEmailDistribution.Email_distribution_to;
                    SharedComponents.Cc = tollEmailDistribution.Email_distribution_cc;
                }
            }

            SharedComponents.Subject = "Priority 1 - " + SharedComponents.Plaza.ToUpper() + " / " + SharedComponents.Lane.ToUpper();
            SharedComponents.Body = "<font size=5>" + "<b>" + "****SunWatch Priority 1 MAF****" + "</b>" + "</font>" + "<br>" + "<br>" +
            "<font size=4>" + "<b>" + "Plaza: " + "</b>" + SharedComponents.Plaza + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Roadway: " + "</b>" + SharedComponents.Roadway + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Lane: " + "</b>" + SharedComponents.Lane + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "BOM Item: " + "</b>" + SharedComponents.Bomitem + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Problem: " + "</b>" + SharedComponents.Problem + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Action Taken: " + "</b>" + SharedComponents.ActionTaken + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Technician: " + "</b>" + SharedComponents.Technician + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "Date/Time Contacted: " + "</b>" + SharedComponents.StartDate + "</font>" + "<br>" +
            "<font size=4>" + "<b>" + "MAF#: " + "</b>" + SharedComponents.MafNumber + "</font>";

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
        List<string> technicians = new();

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            selectTechnician.ItemsSource.Clear();
            plazas.Clear();
            technicians.Clear();

            SharedComponents.TollPlazaQueryByRegionName = SharedComponents.TollPlazaRepo.
            QueryByRegionName(selectRegion.Items[selectedIndex]);

            foreach (TollPlaza tollPlaza in SharedComponents.TollPlazaQueryByRegionName)
            {
                plazas.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name);
            }

            plazas.Sort();
            foreach (string tollPlaza in plazas)
            {
                selectPlaza.ItemsSource.Add(tollPlaza);
            }

            SharedComponents.TollTechnicianQueryByRegion = 
            SharedComponents.TollTechnicianRepo.QueryTechnicianByRegion(selectRegion.Items[selectedIndex]);

            foreach (TollTechnician tollTechnician in SharedComponents.TollTechnicianQueryByRegion)
            {
                technicians.Add(tollTechnician.Technician_name);
            }

            technicians.Sort();
            foreach (string tollTechnician in technicians)
            {
                selectTechnician.ItemsSource.Add(tollTechnician);
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
            foreach (string tollLane in TollLanes)
            {
                selectLane.ItemsSource.Add(tollLane);
            }
        }
    }
    private void SelectLane_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = selectLane.SelectedIndex;
        List<string> TollBomitems = new();

        if (selectedIndex != -1)
        {
            selectBomitem.ItemsSource.Clear();
            TollBomitems.Clear();
            SharedComponents.Lane = selectLane.Items[selectedIndex];
   
            TollBomitems=SharedComponents.GetBomitem(SharedComponents.Lane);
            TollBomitems.Sort();
            foreach(string tollBomitem in TollBomitems)
            {
                selectBomitem.ItemsSource.Add(tollBomitem);
            }
            
        }
    }
}


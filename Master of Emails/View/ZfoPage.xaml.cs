using Master_of_Emails;
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

        if (selectPlaza.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose a Plaza", "Close");
            return;
        }

        else if (!SharedComponents.TollLaneList.Any())
        {
            DisplayAlert("Alert", "Choose Lane(s)", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectRequestor.Text))
        {
            DisplayAlert("Alert", "Enter Requestor", "Close");
            return;
        }

        else if (selectReason.Text == null)
        {
            DisplayAlert("Alert", "Enter Reason", "Close");
            return;
        }

        SharedComponents.Plaza = (string)selectPlaza.SelectedItem;
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
        QueryByRegionEmailTypeAndPlazaId(SharedComponents.Region, SharedComponents.EmailType, "ALL"));

        foreach (TollEmailDistribution emaildistributionZFO in SharedComponents.StandardDistributionZFO)
        {
            SharedComponents.To = emaildistributionZFO.Email_distribution_to;
            SharedComponents.Cc = emaildistributionZFO.Email_distribution_cc;
        }

        string Subject = "SunWatch ZFO Alert - " + SharedComponents.Plaza.ToUpper() + " / " + SharedComponents.Lane.ToUpper();
        string Body = "<font size=5>" + "<b>" + "****SunWatch ZFO Alert****" + "</b>" + "</font>" + "<br>" + "<br>" +
        "<font size=4>" + "<b>" + "Plaza: " + "</b>" + SharedComponents.Plaza + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Lane(s): " + "</b>" + SharedComponents.Lane + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Requestor: " + "</b>" + SharedComponents.Requestor + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Reason: " + "</b>" + SharedComponents.Reason + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Start Date/Time: " + "</b>" + SharedComponents.StartDate + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "End Date/Time: " + "</b>" + SharedComponents.EndDate + "</font>" + "<br>";

        try
        {
            SharedComponents.Mail = (Outlook.MailItem)SharedComponents.ObjApp.CreateItemFromTemplate(SharedComponents.Template);
            SharedComponents.Mail.To = SharedComponents.To;
            SharedComponents.Mail.CC = SharedComponents.Cc;
            SharedComponents.Mail.Subject = Subject;
            SharedComponents.Mail.HTMLBody = Body;
            SharedComponents.Mail.Display();
            SharedComponents.Mail = null;
            SharedComponents.Lane = null;
        }

        catch (Exception ex)
        {
            DisplayAlert("Alert", "Close MOE, make sure Outlook is running, and try again. " + ex.Message, "close");
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

            SharedComponents.TollPlazaQueryByRegionName = SharedComponents.TollPlazaRepo.
            QueryByRegionName(SharedComponents.Region);

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

        if (selectedIndex != -1)
        {
            selectLane.ItemsSource = null;
            SharedComponents.TollLane.Clear();
            var Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            SharedComponents.PlazaId = Int32.Parse(Split[0]);

            if (selectPlaza.Items[selectedIndex].ToString().Equals("3331 Celebration Osceola PKWY NBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3331 Celebration Osceola PKWY SBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3112 CR 470 NBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3112 CR 470 SBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3311 Celebration US 192 NBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3311 Celebration US 192 SBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3410 Osceola PKWY B NBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3410 Osceola Parkway B SBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3120 SR 50 NBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3120 SR 50 SBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3421 Kissimmee Park Rd East NBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3421 Kissimmee Park Rd East SBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3511 Seidel Road NBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3511 Seidel Road SBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3521 Western Beltway US 192 NBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3521 Western Beltway US 192 SBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3531 Sinclair Road NBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3531 Sinclair Road SBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8111 SR 434 NBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8111 SR 434 SBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8121 Red Bug Lake Road NBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8121 Red Bug Lake Road NW SBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8131 Aloma Ave (SR426) NBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8131 Aloma Ave (SR 426) SBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }


            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8151 Lake Mary Blvd (CR 427) NBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8151 Lake Mary Blvd (CR 427) SBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }


            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8171 CR 46A NBOn"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "NB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8171 CR 46A SBOff"))
            {
                SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection = 
                SharedComponents.TollLaneRepo.QueryByPlazaIdAndLaneDirection(SharedComponents.PlazaId, "SB");

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else
            {
                SharedComponents.TollLanesQueryByPlazaId = 
                SharedComponents.TollLaneRepo.QueryByPlazaId(SharedComponents.PlazaId);

                foreach (TollLane tollLane in SharedComponents.TollLanesQueryByPlazaId)
                {
                    SharedComponents.TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }

            }

            SharedComponents.TollLane.Sort();
            selectLane.ItemsSource = SharedComponents.TollLane;
        }
    }
    private void SelectLane_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //DisplayAlert("Check",sender.ToString(),"Close");

        if (e.CurrentSelection.Count == 0)
            return;

        else
        {
            SharedComponents.TollLaneList.Clear();
            for (int i = 0; i < e.CurrentSelection.Count; i++)
            {
                SharedComponents.TollLaneList.Add(e.CurrentSelection[i].ToString());
                //DisplayAlert("Check", TollLane[i], "Close");
            }
        }
    }
}



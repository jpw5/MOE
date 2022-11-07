using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class DuressAlarmPage : ContentPage
{
    public Outlook.Application objApp = new();
    public Outlook.MailItem mail = null;
    public string Template = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");

    public TollPlazaRepository TollPlazaRepo = new();
    public TollLaneRepository TollLaneRepo = new();
    public TollEmailDistributionRepository TollEmailDistributionRepo = new();

    public TableQuery<TollLane> tollLanesQueryByPlazaId;
    public TableQuery<TollLane> tollLanesQueryByPlazaIdAndLaneDirection;
    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;
    public TableQuery<TollEmailDistribution> tollEmailDistributionQueryByRegionAndEmailType;
    public TableQuery<TollEmailDistribution> StandardDistributionDuress;

    public string Region;
    public int PlazaId;

    public string Plaza;
    public string Roadway;
    public string Lane;
    public string Alarm;
    public string PlazaSupervisor;
    public string DuressReason;
    public string Date;

    public string EmailType = "Duress";
    public string To;
    public string Cc;
    public string Subject;
    public string Body;

    public DuressAlarmPage(DuressAlarmPageViewModel duressAlarmPageViewModel)
    {
        InitializeComponent();
        BindingContext = duressAlarmPageViewModel;
    }

    private void DuressAlarmEmailButton_Pressed(object sender, EventArgs e)
    {
        if (selectPlaza.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose a Plaza", "Close");
            return;
        }

        else if (selectLane.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose Lane", "Close");
            return;
        }

        else if (selectDuressReason.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose Alarm Reason", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(Alarm))
        {
            DisplayAlert("Alert", "Choose Alarm", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectPlazaSupervisor.Text))
        {
            DisplayAlert("Alert", "Enter Plaza Supervisor", "Close");
            return;
        }

        Region = selectRegion.SelectedItem.ToString();
        var Split = selectPlaza.SelectedItem.ToString().Split(" ", 2);
        PlazaId = Int32.Parse(Split[0]);
        tollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(PlazaId);
        foreach (TollPlaza plaza in tollPlazaQueryByPlazaId)
        {
            Plaza = plaza.Plaza_id + " " + plaza.Plaza_name;
            Roadway = plaza.Plaza_roadway;
        }

        Region = selectRegion.SelectedItem.ToString();
        Lane = selectLane.SelectedItem.ToString();
        DuressReason = selectDuressReason.SelectedItem.ToString();
        Date = selectDate.Text;
        PlazaSupervisor = selectPlazaSupervisor.Text;
        DuressReason = selectDuressReason.SelectedItem.ToString();

        StandardDistributionDuress =
        TollEmailDistributionRepo.QueryByRegionEmailTypeAndPlazaId(Region, EmailType, PlazaId.ToString());

        To = "";
        Cc = "";
        foreach (TollEmailDistribution emaildistributionDuress in StandardDistributionDuress)
        {
            To = emaildistributionDuress.Email_distribution_to;
            Cc = emaildistributionDuress.Email_distribution_cc;
        }

        string Subject = "Duress Alarm at " + Plaza.ToUpper() + " / " + Lane.ToUpper();
        string Body = "<font size=5>" + "<b>" + "****SunWatch Duress Alarm****" + "</b>" + "</font>" + "<br>" + "<br>" +
        "<font size=4>" + "<b>" + "Plaza: " + "</b>" + Plaza + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Roadway: " + "</b>" + Roadway + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Lane(s): " + "</b>" + Lane + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Date/Time: " + "</b>" + Date + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Alarm: " + "</b>" + Alarm + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Supervisor: " + "</b>" + PlazaSupervisor + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Reason: " + "</b>" + DuressReason + "</font>" + "<br>";

        try
        {
            mail = (Outlook.MailItem)objApp.CreateItemFromTemplate(Template);
            mail.To = To;
            mail.CC = Cc;
            mail.Subject = Subject;
            mail.HTMLBody = Body;
            mail.Display();
            mail = null;
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
            Region = selectRegion.Items[selectedIndex];
            plazas.Clear();
            tollPlazaQueryByRegionName = TollPlazaRepo.QueryByRegionName(Region);
            foreach (TollPlaza tollPlaza in tollPlazaQueryByRegionName)
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
        List<string> lanes = new();
        if (selectedIndex != -1)
        {
            selectLane.ItemsSource.Clear();
            lanes.Clear();
            var Split = selectPlaza.Items[selectedIndex].Split(" ", 2);
            PlazaId = Int32.Parse(Split[0]);


            if (selectPlaza.Items[selectedIndex].ToString().Equals("3331 Celebration Osceola PKWY NBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3331 Celebration Osceola PKWY SBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3112 CR 470 NBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3112 CR 470 SBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3311 Celebration US 192 NBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3311 Celebration US 192 SBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3410 Osceola PKWY B NBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3410 Osceola Parkway B SBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3120 SR 50 NBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3120 SR 50 SBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3421 Kissimmee Park Rd East NBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3421 Kissimmee Park Rd East SBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3511 Seidel Road NBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3511 Seidel Road SBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3521 Western Beltway US 192 NBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3521 Western Beltway US 192 SBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3531 Sinclair Road NBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("3531 Sinclair Road SBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8111 SR 434 NBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8111 SR 434 SBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8121 Red Bug Lake Road NBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8121 Red Bug Lake Road NW SBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8131 Aloma Ave (SR426) NBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8131 Aloma Ave (SR 426) SBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }


            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8151 Lake Mary Blvd (CR 427) NBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8151 Lake Mary Blvd (CR 427) SBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }


            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8171 CR 46A NBOn"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "NB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else if (selectPlaza.Items[selectedIndex].ToString().Equals("8171 CR 46A SBOff"))
            {
                tollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, "SB");
                foreach (TollLane tollLane in tollLanesQueryByPlazaIdAndLaneDirection)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else
            {
                tollLanesQueryByPlazaId = TollLaneRepo.QueryByPlazaId(PlazaId);
                foreach (TollLane tollLane in tollLanesQueryByPlazaId)
                {
                    lanes.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }

            }

            lanes.Sort();
            foreach (string tollLane in lanes)
            {
                selectLane.ItemsSource.Add(tollLane);
            }

        }
    }
    private void VaultRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Alarm = "Vault Under Duress";
    }

    private void LaneRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Alarm = "Lane Duress Alarm";
    }
}
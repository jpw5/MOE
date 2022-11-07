using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class PriorityOneMafPage : ContentPage
{
    public Outlook.Application objApp = new();
    public Outlook.MailItem mail = null;
    public string Template = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");

    public TollPlazaRepository TollPlazaRepo = new();
    public TollLaneRepository TollLaneRepo = new();
    public TollTechnicianRepository TollTechnicianRepo = new();
    public TollBomitemRepository TollBomitemRepo = new();
    public TollEmailDistributionRepository TollEmailDistributionRepo = new();

    public TableQuery<TollLane> tollLanesQueryByPlazaId;
    public TableQuery<TollLane> tollLanesQueryByPlazaIdAndLaneDirection;
    public TableQuery<TollPlaza> tollPlazaQueryByRegionName;
    public TableQuery<TollPlaza> tollPlazaQueryByPlazaId;
    public TableQuery<TollTechnician> tollTechnicianQueryByRegion;
    public TableQuery<TollBomitem> tollBomitemQueryByLaneType;
    public TableQuery<TollEmailDistribution> tollEmailDistributionQueryByRegionEmailTypeAndPlazaId;
    public TableQuery<TollEmailDistribution> StandardDistributionP1PlazaId;
    public TableQuery<TollEmailDistribution> StandardDistributionP1;

    public string Region;
    public int PlazaId;

    public string Plaza;
    public string Roadway;
    public string Lane;
    public string LaneType;
    public string Bomitem;
    public string Problem;
    public string ActionTaken;
    public string Technician;
    public string MAFNumber;
    public string Date;

    public string EmailType = "P1";
    public string To;
    public string Cc;
    public string Subject;
    public string Body;

    public PriorityOneMafPage(PriorityOneMafPageViewModel priorityOneMafPageViewModel)
    {
        InitializeComponent();
        BindingContext = priorityOneMafPageViewModel;
    }

    private void PriorityOneEmail_Button_Pressed(object sender, EventArgs e)
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

        else if (selectBomitem.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose Bomitem", "Close");
            return;
        }

        else if (selectTechnician.SelectedItem == null)
        {
            DisplayAlert("Alert", "Choose Technician", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectMafNumber.Text))
        {
            DisplayAlert("Alert", "Enter MAF#", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectProblem.Text))
        {
            DisplayAlert("Alert", "Enter Problem", "Close");
            return;
        }

        else if (string.IsNullOrEmpty(selectActionTaken.Text))
        {
            DisplayAlert("Alert", "Enter Action Taken", "Close");
            return;
        }

        Plaza = selectPlaza.SelectedItem.ToString();
        var Split = Plaza.Split(" ", 2);
        PlazaId = Int32.Parse(Split[0]);
        tollPlazaQueryByPlazaId = TollPlazaRepo.QueryByPlazaId(PlazaId);
        foreach (TollPlaza plaza in tollPlazaQueryByPlazaId)
        {
            Roadway = plaza.Plaza_roadway;
        }

        Region = selectRegion.SelectedItem.ToString();
        Lane = selectLane.SelectedItem.ToString();
        Bomitem = selectBomitem.SelectedItem.ToString();
        Technician = selectTechnician.SelectedItem.ToString();
        Date = selectDate.Text;
        MAFNumber = selectMafNumber.Text;
        Problem = selectProblem.Text;
        ActionTaken = selectActionTaken.Text;

        To = "";
        Cc = "";
        StandardDistributionP1 = TollEmailDistributionRepo.
        QueryByRegionEmailTypeAndPlazaId(Region, EmailType, "ALL");

        StandardDistributionP1PlazaId = TollEmailDistributionRepo.
        QueryByPlazaId(Roadway);

        if (StandardDistributionP1PlazaId.Any())
        {
            foreach (TollEmailDistribution tollEmailDistribution in StandardDistributionP1PlazaId)
            {
                To = tollEmailDistribution.Email_distribution_to;
                Cc = tollEmailDistribution.Email_distribution_cc;
            }
        }

        else
        {
            foreach (TollEmailDistribution tollEmailDistribution in StandardDistributionP1)
            {
                To = tollEmailDistribution.Email_distribution_to;
                Cc = tollEmailDistribution.Email_distribution_cc;
            }
        }

        Subject = "Priority 1 - " + Plaza.ToUpper() + " / " + Lane.ToUpper();
        Body = "<font size=5>" + "<b>" + "****SunWatch Priority 1 MAF****" + "</b>" + "</font>" + "<br>" + "<br>" +
        "<font size=4>" + "<b>" + "Plaza: " + "</b>" + Plaza + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Roadway: " + "</b>" + Roadway + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Lane: " + "</b>" + Lane + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "BOM Item: " + "</b>" + Bomitem + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Problem: " + "</b>" + Problem + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Action Taken: " + "</b>" + ActionTaken + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Technician: " + "</b>" + Technician + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "Date/Time Contacted: " + "</b>" + Date + "</font>" + "<br>" +
        "<font size=4>" + "<b>" + "MAF#: " + "</b>" + MAFNumber + "</font>";

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
        List<string> technicians = new();

        if (selectedIndex != -1)
        {
            selectPlaza.ItemsSource.Clear();
            selectTechnician.ItemsSource.Clear();
            plazas.Clear();
            technicians.Clear();
            Region = selectRegion.Items[selectedIndex];

            tollPlazaQueryByRegionName = TollPlazaRepo.QueryByRegionName(Region);
            foreach (TollPlaza tollPlaza in tollPlazaQueryByRegionName)
            {
                plazas.Add(tollPlaza.Plaza_id + " " + tollPlaza.Plaza_name);

            }

            plazas.Sort();
            foreach (string tollPlaza in plazas)
            {
                selectPlaza.ItemsSource.Add(tollPlaza);
            }

            tollTechnicianQueryByRegion = TollTechnicianRepo.QueryTechnicianByRegion(Region);
            foreach (TollTechnician tollTechnician in tollTechnicianQueryByRegion)
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
    private void SelectLane_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectBomitem.ItemsSource.Clear();
        List<string> bomitem = new();
        int selectedIndex = selectLane.SelectedIndex;

        if (selectedIndex != -1)
        {
            var Split = selectLane.Items[selectedIndex].Split(" ", 2);
            LaneType = (Split[1]);
        }

        if (LaneType == "ADM")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }

        else if (LaneType == "DED")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }

        else if (LaneType == "ORT")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }

        else if (LaneType == "COAPM")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }

        else if (LaneType == "MB")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }
        else if (LaneType == "ME")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }
        else if (LaneType == "MX")
        {
            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }

            tollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
            foreach (TollBomitem tollBomitem in tollBomitemQueryByLaneType)
            {
                selectBomitem.ItemsSource.Add(tollBomitem.Bomitem_name);
            }
        }
    }
}


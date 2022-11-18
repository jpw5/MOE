using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Master_of_Emails.Components
{
    public class SharedComponents
    {
        //Declares instances of the PlazaRepository classes used by other classes.
        public TollPlazaRepository TollPlazaRepo = new();
        public TollLaneRepository TollLaneRepo = new();
        public TollTechnicianRepository TollTechnicianRepo = new();
        public TollBomitemRepository TollBomitemRepo = new();
        public TollEmailDistributionRepository TollEmailDistributionRepo = new();

        //Instanciates variables for the TableQueries used by other classes.
        public TableQuery<TollPlaza> TollPlazaQueryByRegionName { get; set; }
        public TableQuery<TollPlaza> TollPlazaQueryByPlazaId { get; set; }
        public TableQuery<TollPlaza> TollPlazaQueryByPlazaName { get; set; }
        public TableQuery<TollLane> TollLanesQueryByPlazaId { get; set; }
        public TableQuery<TollLane> TollLanesQueryByPlazaIdAndLaneDirection { get; set; }
        public TableQuery<TollTechnician> TollTechnicianQueryByRegion { get; set; }
        public TableQuery<TollBomitem> TollBomitemQueryByLaneType { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionZFO { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionDuress { get; set; }
        public TableQuery<TollEmailDistribution> TollEmailDistributionQueryByRegionAndEmailType { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionIncon { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionP1 { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionP1PlazaId { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionScadaInfinity { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionScadaAll { get; set; }


        //Variables used by the emailer forms.
        public List<string> TollLaneList = new();
        public string Region { get; set; }
        public string Plaza { get; set; }
        public int PlazaId { get; set; }
        public string PlazaName { get; set; }
        public string Roadway { get; set; }
        public string Lane { get; set; }
        public string Requestor { get; set; }
        public string Reason { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Alarm { get; set; }
        public string PlazaSupervisor { get; set; }
        public string DuressReason { get; set; }

        public string MilePost { get; set; }
        public string AffectedArea { get; set; }
        public string Who { get; set; }
        public string AmountCut { get; set; }
        public string FiberTechnician { get; set; }
        public string ReportedBy { get; set; }
        public string PhoneNumber { get; set; }

        public string Duration { get; set; }
        public string Units { get; set; }
        public string Bomitem { get; set; }
        public string Technician { get; set; }
        public string MafNumber { get; set; }
        public string Problem { get; set; }
        public string ActionTaken { get; set; }

        public string SelectedHours { get; set; }
        public string Contact { get; set; }
        public string BuildingNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public string Temperature { get; set; }
        public string PlazaCompany { get; set; }
        
        //Variable definitions for the sending of the email
        public Outlook.Application ObjApp = new();
        public Outlook.MailItem Mail { get; set; } = null;
        public string Template { get; set; } = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");
        public string EmailTypeZFO { get; set; } = "ZFO";
        public string EmailTypeDuress { get; set; } = "Duress";
        public string EmailTypeIncon { get; set; } = "Incon";
        public string EmailTypeP1 { get; set; } = "P1";
        public string EmailTypeSCADA { get; set; } = "SCADA";
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        //Method to retreive the assoicated lanes of a plaza. Returned in List<string> form.
        public List<string> GetLanes()
        {

            List<string> TollLane = new();
            var Split = Plaza.Split(" ");
            string Direction = Split.Last();

            if
             (
             Direction.Equals("NBOn") || Direction.Equals("SBOff") || Direction.Equals("NBOff") ||
             Direction.Equals("SBOn") || Direction.Equals("WBOff") || Direction.Equals("EBOn") ||
             Direction.Equals("EBOff") || Direction.Equals("WBOn")
             )

            {
                TollLanesQueryByPlazaIdAndLaneDirection = TollLaneRepo.QueryByPlazaIdAndLaneDirection(PlazaId, Direction);
                foreach (TollLane tollLane in TollLanesQueryByPlazaIdAndLaneDirection)
                {
                    TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }

            else
            {
                TollLanesQueryByPlazaId = TollLaneRepo.QueryByPlazaId(PlazaId);

                foreach (TollLane tollLane in TollLanesQueryByPlazaId)
                {
                    TollLane.Add(tollLane.Lane_number.ToString() + " " + tollLane.Lane_type);
                }
            }
            return TollLane;
        }

        //Method to retreive the assoicated Bomitem of a lane. Returned in List<string> form.
        public List<string> GetBomitem(string Lane)
        {
            List<string> TollBomitems = new();
            var Split = Lane.Split(" ");
            string LaneType = (Split[1]);

            if(LaneType.Equals("DED") || LaneType.Equals("COAPM") || LaneType.Equals("MB") || LaneType.Equals("ME") || 
               LaneType.Equals("MX"))
            {
                TollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType("ALL");
                foreach (TollBomitem tollBomitem in TollBomitemQueryByLaneType)
                {
                    TollBomitems.Add(tollBomitem.Bomitem_name);
                }

                TollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
                foreach (TollBomitem tollBomitem in TollBomitemQueryByLaneType)
                {
                    TollBomitems.Add(tollBomitem.Bomitem_name);
                }
            }

            else if (LaneType == "ADM")
            {
                TollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
                foreach (TollBomitem tollBomitem in TollBomitemQueryByLaneType)
                {
                    TollBomitems.Add(tollBomitem.Bomitem_name);
                }
            }

            else if (LaneType == "ORT")
            {
                TollBomitemQueryByLaneType = TollBomitemRepo.QueryByLaneType(LaneType);
                foreach (TollBomitem tollBomitem in TollBomitemQueryByLaneType)
                {
                    TollBomitems.Add(tollBomitem.Bomitem_name);
                }
            }
            return TollBomitems;
        }

        //Method to check if inputs in the ZFO emailer are not empty.
        public static bool CheckZFOInputs(object Region, object Plaza, List<string> TollLaneList, string Requestor, 
         string Reason, string StartDate, string EndDate)

        {
            if
             (
             Region == null || 
             Plaza == null || 
             !TollLaneList.Any() || 
             string.IsNullOrEmpty(Requestor) ||       
             string.IsNullOrEmpty(Reason) || 
             string.IsNullOrEmpty(StartDate) || 
             string.IsNullOrEmpty(EndDate)
             )

            {
                return false;
            }

            return true;
        }
        //Method to check if inputs in the Duress Alarm emailer are not empty.
        public static bool CheckDuressAlarmInputs(object Region, object Plaza, object Lane, object DuressReason, 
         string Alarm, string PlazaSupervisor, string StartDate)

        {
            if
            (
             Region == null ||
             Plaza == null ||
             Lane == null ||
             DuressReason == null ||
             string.IsNullOrEmpty(Alarm) ||
             string.IsNullOrEmpty(PlazaSupervisor) ||
             string.IsNullOrEmpty(StartDate)
         )

            {
                return false;
            }

            return true;
        }

        //Method to check if inputs in the Fiber Alert emailer are not empty.
        public static bool CheckFiberAlertInputs(object Region, string MilePost, string AffectedArea, string Who, 
         string AmountCut, string FiberTechnician, string ReportedBy, string PhoneNumber)

        {
            if
            (
             Region == null ||
             string.IsNullOrEmpty(MilePost) ||
             string.IsNullOrEmpty(AffectedArea) ||
             string.IsNullOrEmpty(Who) ||
             string.IsNullOrEmpty(AmountCut) ||
             string.IsNullOrEmpty(FiberTechnician) ||
             string.IsNullOrEmpty(ReportedBy)||
             string.IsNullOrEmpty(PhoneNumber) 
            )

              {
               return false;
              }

            return true;
        }
        //Method to check if inputs in the Incon Alert emailer are not empty.
        public static bool CheckInconAlertInputs(object Region, object Plaza, List<string> TollLaneList, string Requestor,
        string PhoneNumber, string Reason, string StartDate, string IncidentOrESR, string Duration, string Units)

        {
            if
            (
             Region == null ||
             Plaza == null ||
             !TollLaneList.Any() ||
             string.IsNullOrEmpty(Requestor) ||
             string.IsNullOrEmpty(PhoneNumber) ||
             string.IsNullOrEmpty(Reason) ||
             string.IsNullOrEmpty(StartDate) ||
             string.IsNullOrEmpty(IncidentOrESR) ||
             string.IsNullOrEmpty(Duration) ||
             string.IsNullOrEmpty(Units)
            )

             {
             return false;
             }

            return true;
        }
        //Method to check if inputs in the Priority 1 MAF emailer are not empty.
        public static bool CheckPriorityOneInputs(object Region, object Plaza, object Lane, object Bomitem,
        object Technician, string MafNumber, string Problem, string ActionTaken)

        {
            if
            (
             Region == null ||
             Plaza == null ||
             Lane == null ||
             Bomitem == null ||
             Technician == null ||
             string.IsNullOrEmpty(MafNumber) ||
             string.IsNullOrEmpty(Problem) ||
             string.IsNullOrEmpty(ActionTaken)
            )

            {
                return false;
            }

            return true;
        }
        //Method to check if inputs in the SCADA emailer are not empty.
        public static bool CheckScadaInputs(object Region, object Plaza, string SelectedHours, string Contact,
        string PhoneNumber, string ScadaAlarm, string BuildingNumber, string WorkOrderNumber, string StartDate,
        string Temperature)

        {
            if
            (
             Region == null ||
             Plaza == null ||
             string.IsNullOrEmpty(SelectedHours) ||
             string.IsNullOrEmpty(Contact) ||
             string.IsNullOrEmpty(PhoneNumber) ||
             string.IsNullOrEmpty(ScadaAlarm) ||
             string.IsNullOrEmpty(BuildingNumber) ||
             string.IsNullOrEmpty(WorkOrderNumber) ||
             string.IsNullOrEmpty(StartDate) ||
             string.IsNullOrEmpty(Temperature)
            )

            {
                return false;
            }

            return true;
        }

        
    }
}

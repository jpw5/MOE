using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using SQLite;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Master_of_Emails
{
    public class SharedComponents
    {
        public TollPlazaRepository TollPlazaRepo = new();
        public TollLaneRepository TollLaneRepo = new();
        public TollTechnicianRepository TollTechnicianRepo = new();
        public TollBomitemRepository TollBomitemRepo = new();
        public TollEmailDistributionRepository TollEmailDistributionRepo = new();

        public TableQuery<TollPlaza> TollPlazaQueryByRegionName { get; set; }
        public TableQuery<TollPlaza> TollPlazaQueryByPlazaId { get; set; }
        public TableQuery<TollLane> TollLanesQueryByPlazaId { get; set; }
        public TableQuery<TollLane> TollLanesQueryByPlazaIdAndLaneDirection { get; set; }
        public TableQuery<TollTechnician> TollTechnicianQueryByRegion { get; set; }
        public TableQuery<TollBomitem> TollBomitemQueryByLaneType { get; set; }
        public TableQuery<TollEmailDistribution> StandardDistributionZFO { get; set; }
        public TableQuery<TollEmailDistribution> TollEmailDistributionQueryByRegionAndEmailType { get; set; }

        public List<string> TollLane = new();
        public List<string> TollLaneList = new();
        public string Region { get; set; }
        public int PlazaId { get; set; }
        public string Plaza { get; set; }
        public string Lane { get; set; }
        public string Requestor { get; set; }
        public string Reason { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public Outlook.Application ObjApp = new();
        public Outlook.MailItem Mail { get; set; } = null;
        public string Template { get; set; } = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");
        public string EmailTypeZFO { get; set; } = "ZFO";
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public List<string> GetLanes(string Plaza, int PlazId)
        {

            List<string> TollLane = new();
            var Split= Plaza.Split(" ");
            String Direction = Split.Last();


            if 
             (
             Direction.Equals("NBOn") || Direction.Equals("SBOff") || Direction.Equals("NBOff") || 
             Direction.Equals("SBOn") || Direction.Equals("WBOff") || Direction.Equals("EBOn")    ||
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


    }


}

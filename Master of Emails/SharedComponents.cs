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
        public string EmailType { get; set; } = "ZFO";
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

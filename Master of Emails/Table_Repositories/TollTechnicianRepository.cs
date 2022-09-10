﻿using Master_of_Emails.Database;
using Master_of_Emails.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.Table_Repositories
{
  
    public class TollTechnicianRepository
    {
        public DB DB;
        public static SQLiteConnection DatabaseConnection { get; private set; }

        private static void Init()
        {
            DatabaseConnection = DB.DatabaseInit();
            DatabaseConnection.CreateTable<TollTechnician>();
        }

        public void AddTechnician(string NewTechnicianKnId, string NewTechnicianName, 
        string NewTechnicianPhoneNumber, string NewTechnicianEmail, string NewTechnicianRegion)
        {
            Init();
            var technician= new TollTechnician
            {
                Technician_email = NewTechnicianEmail,
                Technician_kn_id= NewTechnicianKnId,
                Technician_name= NewTechnicianName,
                Technician_phone_number= NewTechnicianPhoneNumber,
                Technician_region= NewTechnicianRegion
            };
            DatabaseConnection.Insert(technician);
        }

        public void DeleteTechnician(string Technician_kn_id)
        {
            Init();
            DatabaseConnection.Delete<TollTechnician>(Technician_kn_id);
        }

        public List<TollTechnician> GetTechnician()
        {
            Init();
            return DatabaseConnection.Table<TollTechnician>().ToList();
        }

        public TableQuery<TollTechnician> QueryTechnicianByName(string TechnicianName)
        {
            Init();
            return DatabaseConnection.Table<TollTechnician>().Where(value => value.Technician_name.
            Equals(TechnicianName));
        }

        public TableQuery<TollTechnician> QueryTechnicianByRegion(string TechnicianRegion)
        {
            Init();
            return DatabaseConnection.Table<TollTechnician>().Where(value => value.Technician_region.
            Equals(TechnicianRegion));
        }


    }



   
}

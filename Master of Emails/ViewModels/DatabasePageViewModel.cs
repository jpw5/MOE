using CommunityToolkit.Mvvm.ComponentModel;
using Master_of_Emails.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_of_Emails.ViewModels; 

    public partial class DatabasePageViewModel: ObservableObject
    {
    
    public DB DB;
    [ObservableProperty]
    public string newRegion;


        public DatabasePageViewModel()
        {
            if (DB.DatabaseConnection == null)
                DB.DatabaseInit();
        }





    }


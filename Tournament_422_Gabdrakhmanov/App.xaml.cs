using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tournament_422_Gabdrakhmanov.Components;

namespace Tournament_422_Gabdrakhmanov
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TournamentDB_422_GabdraxmanovEntities db = new TournamentDB_422_GabdraxmanovEntities();
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsAndInvitesLibrary.DataAccess;

namespace EventsAndInvitesLibrary
{
    public class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections()
        {
            SQLConnection sql = new SQLConnection();
            Connection = sql;
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}

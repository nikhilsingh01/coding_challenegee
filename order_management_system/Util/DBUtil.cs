using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Util
{
    internal class DBUtil 
    {
        private static IConfiguration _iConfiguration;
        //constructor
        static DBUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _iConfiguration = builder.Build();
        }

        public static string GetConnectionString()
        {
            return _iConfiguration.GetConnectionString("LocalConnString");
        }

    }
}

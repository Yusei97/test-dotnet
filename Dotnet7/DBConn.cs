using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Dotnet7
{
    public class DBConn
    {
        //private static string localConnectionString = "Data Source=localhost; Initial Catalog=TestDB; uid=sa; password=P@ssw0rd; Connect Timeout=30000; TrustServerCertificate=false; Encrypt=false;";
        private static string localConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=K@buto123;";

        private readonly static string ConfigurationFilePath = @"C:\Config\Configuration.xml";

        public static string connString
        {
            get
            {
                
                return localConnectionString;

            }
        }

    }
}

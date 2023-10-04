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
        private static string localConnectionString = "";

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

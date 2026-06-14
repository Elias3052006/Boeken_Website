using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataLayer.DataBaseContext
{
    public class DatabaseContext
    {
        //een instantie aan van je configuratie
        private static readonly AppConfiguration _config = new AppConfiguration();

        //Maakt een nieuw verbindingsobject aan met de connection string. De verbinding is nog niet open, alleen aangemaakt.
        public  MySqlConnection GetConnection()
        {
            // We geven nu een MySqlConnection terug in plaats van een SqlConnection
            return new MySqlConnection(_config.sqlconnectionstring);
        }
    }
}

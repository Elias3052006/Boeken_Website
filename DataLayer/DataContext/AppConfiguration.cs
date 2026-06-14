using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DataLayer.DataBaseContext
{
    public class AppConfiguration
    {
        //constructor
        public  AppConfiguration()
        {
            //Dit een hulpobject aan om configuratiebestanden te lezen.
            var configBuilder = new ConfigurationBuilder();
            //Dit zoekt het volledige pad naar appsettings.json op de schijf.
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            //Laadt het JSON bestand in. false betekent: het bestand moet bestaan, anders crasht de app.
            configBuilder.AddJsonFile(path, false);
            //Verwerkt het bestand tot een bruikbaar configuratie object.
            var root = configBuilder.Build();
            //Navigeert naar dit stukje in appsettings.json en slaat de waarde op:
            var appsettings = root.GetSection("ConnectionStrings:DefaultConnection");
            sqlconnectionstring = appsettings.Value;
        }
        public string sqlconnectionstring { get; set; }
    }
}

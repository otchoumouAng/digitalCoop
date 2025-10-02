using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;


namespace Tms.Components.Data
{
    
        internal class DataConfigSection : System.Configuration.ConfigurationSection
        {

            static DataConfigSection _Config = null;
            public DataConfigSection()
            {
            }


            [ConfigurationProperty("database")]
            public string DataBase
            {
                get { return Convert.ToString(this["database"]); }
                set { this["database"] = value; }
            }


            [ConfigurationProperty("server")]
            public string Server
            {
                get { return Convert.ToString(this["server"]); }
                set { this["server"] = value; }
            }

            [ConfigurationProperty("language")]
            public string Language
            {
                get { return Convert.ToString(this["language"]); }
                set { this["language"] = value; }
            }


            [ConfigurationProperty("others")]
            public string Others
            {
                get { return Convert.ToString(this["others"]); }
                set { this["others"] = value; }
            }


            [ConfigurationProperty("user")]
            public string UserName
            {
                get { return Convert.ToString(this["user"]); }
                set { this["user"] = value; }
            }

            [ConfigurationProperty("password")]
            public string Password
            {
                get { return Convert.ToString(this["password"]); }
                set { this["password"] = value; }
            }

            public static DataConfigSection GetConfigSection()
            {                
                return (DataConfigSection)ConfigurationManager.GetSection("DataParameters");
            }            
                   
    }
    
}

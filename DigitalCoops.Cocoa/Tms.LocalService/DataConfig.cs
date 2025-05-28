
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using System.IO;
using System.Reflection;
using Tms.LocalService;
using Tms.Components.Data;

namespace Tms.LocalService
{
    public class DataConfig
    {
        private static string _DataBase = null;
        private static string _Server = null;
        private static string _Language = null;


        private static string _UserName = null;
        private static string _Password = null;

        private static string _Others = null;

        const string c_Salt = "ttt";

        static DataConfigSection _CS = null;
        public DataConfig()
        {
            if (_CS == null)
            {
                _CS = DataConfigSection.GetConfigSection();
            }

            if ((_CS != null))
            {
                _DataBase = _CS.DataBase;
                _Server = _CS.Server;
                _Language = _CS.Language;
                _Others = _CS.Others;

                _UserName = _CS.UserName;
                _Password = _CS.Password;
            }

        }


        public string DataBase
        {
            get
            {
                try
                {
                    DataEncryption Crpt = new DataEncryption();
                    string strWithSalt = Crpt.Decrypter(_DataBase);
                    return strWithSalt.Substring(c_Salt.Length, strWithSalt.Length - c_Salt.Length);
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                DataEncryption Crpt = new DataEncryption();
                _DataBase = Crpt.Crypter(c_Salt + value);
            }
        }


        public string Server
        {
            get
            {
                try
                {
                    DataEncryption Crpt = new DataEncryption();
                    string strWithSalt = Crpt.Decrypter(_Server);
                    return strWithSalt.Substring(c_Salt.Length, strWithSalt.Length - c_Salt.Length);
                    return strWithSalt;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                DataEncryption Crpt = new DataEncryption();
                _Server = Crpt.Crypter(c_Salt + value);
            }
        }


        public string Language
        {
            get
            {
                try
                {
                    DataEncryption Crpt = new DataEncryption();
                    string strWithSalt = Crpt.Decrypter(_Language);
                    return strWithSalt.Substring(c_Salt.Length, strWithSalt.Length - c_Salt.Length);
                    return strWithSalt;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                DataEncryption Crpt = new DataEncryption();
                _Language = Crpt.Crypter(c_Salt + value);
            }
        }


        public string Others
        {
            get
            {
                try
                {
                    DataEncryption Crpt = new DataEncryption();
                    string strWithSalt = Crpt.Decrypter(_Others);
                    return strWithSalt.Substring(c_Salt.Length, strWithSalt.Length - c_Salt.Length);
                    return strWithSalt;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                DataEncryption Crpt = new DataEncryption();
                _Others = Crpt.Crypter(c_Salt + value);
            }
        }



        public string UserName
        {
            get
            {
                try
                {
                    DataEncryption Crpt = new DataEncryption();
                    string strWithSalt = Crpt.Decrypter(_UserName);
                    return strWithSalt.Substring(c_Salt.Length, strWithSalt.Length - c_Salt.Length);
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                DataEncryption Crpt = new DataEncryption();
                _UserName = Crpt.Crypter(c_Salt + value);
            }
        }



        public string Password
        {
            get
            {
                try
                {
                    DataEncryption Crpt = new DataEncryption();
                    string strWithSalt = Crpt.Decrypter(_Password);
                    return strWithSalt.Substring(c_Salt.Length, strWithSalt.Length - c_Salt.Length);
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                DataEncryption Crpt = new DataEncryption();
                _Password = Crpt.Crypter(c_Salt + value);
            }
        }


        public void Save()
        {
            System.Configuration.Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.Sections.Remove("DataParameters");
            DataConfigSection cs = new DataConfigSection();
            cs.DataBase = _DataBase;
            cs.Server = _Server;
            cs.Language = _Language;
            cs.Others = _Others;

            cs.UserName = _UserName;
            cs.Password = _Password;
            conf.Sections.Add("DataParameters", cs);
            conf.Save();
            _CS = cs;
            UpdateConfigFileIntoUserAppData();
        }        

        public void UpdateConfigFileIntoUserAppData()
        {
            try
            {
                System.Configuration.Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string chemin = conf.FilePath;
                //chemin = System.Reflection.Assembly.GetExecutingAssembly().Location + ".Config"
                string cheminAppDataCurUser = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TMS";
                if (!Directory.Exists(cheminAppDataCurUser))
                {
                    Directory.CreateDirectory(cheminAppDataCurUser);
                }
                cheminAppDataCurUser = cheminAppDataCurUser + "\\App.Config";

                if (File.Exists(chemin))
                {
                    File.Copy(chemin, cheminAppDataCurUser, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Pas très solide: à revoir
        public bool IsTestEnvironment()
        {
            return ((Server.ToUpper().IndexOf("001") >= 0) | (Server.ToUpper().IndexOf("569W") >= 0));
        }

    }
}

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Tms.Components.Settings
{
    public class SettingsManager
    {
        const string cAppControlsSettings = "AppControlsSettings.config";
        private static string mFile;

        private static XMLSettingsFileManager conf = new XMLSettingsFileManager();
        //String used to concatenate settings

        private const string _ConcatString = Constants.vbTab;
        public static string ConfigFile
        {
            get { return mFile; }
            set
            {
                mFile = value;
                conf.cfgFile = mFile;
            }
        }

        public static void ReinitAppControlsSettingsParams()
        {
            //ConfigFile = Application.StartupPath & "\" & "AppControlsSettings.config"
            //ConfigFile = My.Computer.FileSystem.CurrentDirectory & "\" & "AppControlsSettings.config" 
            //Détermination de l'emplacement du fichier AppControlsSettings.config
            const string cTmsDirectory = "Tms";
            string repertoire = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + cTmsDirectory;
            if (!Directory.Exists(repertoire))
            {
                Directory.CreateDirectory(repertoire);
            }
            string pathAppCS = repertoire + "\\" + cAppControlsSettings;
            if (!File.Exists(pathAppCS))
            {
                File.Delete(pathAppCS);
            }

            if (!File.Exists(pathAppCS))
            {
                string defAppCS = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + cAppControlsSettings;
                if (File.Exists(defAppCS))
                {
                    File.Copy(defAppCS, pathAppCS);
                }
            }
            if (File.Exists(pathAppCS))
            {
                ConfigFile = pathAppCS;
            }
            else
            {
                throw new Exception("Problème lors de la création du fichier " + cAppControlsSettings);
            }
        }



        static SettingsManager()
        {
            ReinitAppControlsSettingsParams();
        }

        public static bool SaveOverviewSettings(string key, List<Setting> mSettings)
        {
            string strSettings = "";
            //Setting mSetting = new Setting();
            string sSection = "OverviewSettings";

            try
            {
                //Build Settings string by concatenating all settings object from mSettings
                foreach (Setting mSetting in mSettings)
                {
                    strSettings += mSetting.ToString() + _ConcatString;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Constants.vbCrLf + "SettingsManager:SaveOverviewSettings");
            }
            //Save in config file and return True/False
            return conf.SetValue("//" + sSection + "//add[@key='" + key + "']", strSettings);
        }

        public static List<Setting> GetOverviewSettings(string key)
        {
            string strSettings = null;
            List<Setting> mSettings = new List<Setting>();
            string sSection = "OverviewSettings";

            try
            {
                //Retreive full row of settings from setting xml file
                strSettings = (string)conf.GetValue("//" + sSection.ToString() + "//add[@key='" + key.ToString() + "']", typeof(string));
                ExtractSettings(strSettings, mSettings);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Constants.vbCrLf + "SettingsManager:GetOverviewSettings");
            }

            return mSettings;
        }

        //Recursive sub to convert settings string (strSettings) to settings object (colSettings)
        private static void ExtractSettings(string strSettings, List<Setting> colSettings)
        {
            Setting mSetting = default(Setting);
            int intPosition = 0;
            try
            {
                intPosition = Strings.InStr(strSettings, _ConcatString);
                if (intPosition > 0)
                {
                    mSetting = new Setting();
                    mSetting.InString(Strings.Mid(strSettings, 1, intPosition - 1));
                    colSettings.Add(mSetting);
                    ExtractSettings(Strings.Mid(strSettings, intPosition + 1), colSettings);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Constants.vbCrLf + "SettingsManager:ExtractSettings");
            }
        }

    }

}

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Xml;
using System.IO;

namespace Tms.Components.Settings
{
    public class XMLConfig : AppSettingsReader
    {

        // Methods

        public XMLConfig()
        {
        }

        public  string GetValue(string key)
        {
            return Convert.ToString(this.GetValue(key, typeof(string)));
        }

        public new object GetValue(string key, Type sType)
        {
            XmlDocument doc = new XmlDocument();
            object ro = string.Empty;
            this.loadDoc(doc);
            string sNode = key.Substring(0, key.LastIndexOf("//"));
            try
            {
                this.node = doc.SelectSingleNode(sNode);
                if (((this.node != null)))
                {
                    XmlElement element1 = (XmlElement)this.node.SelectSingleNode(key);
                    if (((element1 != null)))
                    {
                        ro = element1.GetAttribute("value");
                    }
                }
                if (((!object.ReferenceEquals(sType, typeof(string)))))
                {
                    if ((object.ReferenceEquals(sType, typeof(bool))))
                    {
                        if ((ro.Equals("True") || ro.Equals("False")))
                        {
                            return Convert.ToBoolean(ro);
                        }
                        return false;
                    }
                    if ((object.ReferenceEquals(sType, typeof(int))))
                    {
                        return Convert.ToInt32(ro);
                    }
                    if ((object.ReferenceEquals(sType, typeof(double))))
                    {
                        return Convert.ToDouble(ro);
                    }
                    if ((object.ReferenceEquals(sType, typeof(DateTime))))
                    {
                        return Convert.ToDateTime(ro);
                    }
                }
                return Convert.ToString(ro);
            }
            catch
            {
                return string.Empty;
            }

        }
        private void loadDoc(XmlDocument doc)
        {
            doc.Load(this._cfgFile);
        }
        public bool removeElement(string key)
        {
            XmlDocument doc = new XmlDocument();
            this.loadDoc(doc);
            try
            {
                string sNode = key.Substring(0, key.LastIndexOf("//"));
                this.node = doc.SelectSingleNode(sNode);
                if ((this.node == null))
                {
                    return false;
                }
                XmlNode node1 = this.node.SelectSingleNode(key);
                this.node.RemoveChild(node1);
                this.saveDoc(doc, this._cfgFile);
                return true;
            }
            catch (Exception exception1)
            {
                Console.WriteLine(exception1.Message);
                return false;
            }
        }
        private void saveDoc(XmlDocument doc, string docPath)
        {
            if (!this._cfgFile.Equals("web.config"))
            {
                try
                {
                    XmlTextWriter writer = new XmlTextWriter(docPath, null);
                    writer.Formatting = Formatting.Indented;
                    doc.WriteTo(writer);
                    writer.Flush();
                    writer.Close();
                }
                catch
                {
                }
            }

        }
        public bool SetValue(string key, string val)
        {
            XmlDocument doc = new XmlDocument();
            this.loadDoc(doc);
            try
            {
                string sNode = key.Substring(0, key.LastIndexOf("//"));
                this.node = doc.SelectSingleNode(sNode);
                if ((this.node == null))
                {
                    return false;
                }
                XmlElement elt = (XmlElement)this.node.SelectSingleNode(key);
                if (((elt != null)))
                {
                    elt.SetAttribute("value", val);
                }
                else {
                    sNode = key.Substring((key.LastIndexOf("//") + 2));
                    XmlElement entry = doc.CreateElement(sNode.Substring(0, sNode.IndexOf("[@")).Trim());
                    sNode = sNode.Substring((sNode.IndexOf("'") + 1));
                    entry.SetAttribute("key", sNode.Substring(0, sNode.IndexOf("'")));
                    entry.SetAttribute("value", val);
                    this.node.AppendChild(entry);
                }
                this.saveDoc(doc, this._cfgFile);
                return true;
            }
            catch
            {
                return false;
            }

        }

        // Properties
        public string cfgFile
        {
            get { return this._cfgFile; }
            set { this._cfgFile = value; }
        }

        // Fields
        private string _cfgFile;

        private XmlNode node;
        #region "Plus simplement pour une Section section OutookBarSettings"
        public bool SetValueForOutookBarSettingsSection(string key, string val)
        {
            return SetValue("//OutookBarSettings//add[@key='" + key + "']", val);
        }
        public object GetValueForOutookBarSettingsSection(string key, Type sType)
        {
            return GetValue("//OutookBarSettings//add[@key='" + key + "']", sType);
        }

        #endregion
    }

    public class AppControlsSettingsConfig
    {
        private static string mFile;
        private static XMLConfig conf = new XMLConfig();
        public static string ConfigFile
        {
            get { return mFile; }
            set
            {
                mFile = value;
                conf.cfgFile = mFile;
            }
        }

        static AppControlsSettingsConfig()
        {
            ConfigFile = SettingsManager.ConfigFile;

        }

        public static bool SetValue(string key, string val)
        {
            return conf.SetValue("//AppControlsSettings//add[@key='" + key + "']", val);
        }
        public static object GetValue(string key, Type sType)
        {
            return conf.GetValue("//AppControlsSettings//add[@key='" + key + "']", sType);
        }
        public static string GetValue(string key)
        {
            return (string)conf.GetValue("//AppControlsSettings//add[@key='" + key + "']", typeof(string));
        }


    }

}

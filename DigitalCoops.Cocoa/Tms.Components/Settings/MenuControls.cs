using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Ext.Net;
using System.Reflection;
using System.ComponentModel;
using System.Web.UI;

using Newtonsoft.Json;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;
using Tms.Components.Data;

namespace Tms.Components.Settings
{
    
    public class MenuControls
    {
        #region "Declarations"

        private enum Modules
        {
            None = 0,
            Administration = 1,
            Purchases = 2,
            Sales = 3
        }
                
        #endregion

        #region "Methods"
        //****Creation des Controles de Menu Ext.Net ****
        public Ext.Net.MenuPanel Create_ExtMenuPanel(string strID)
        {
            Ext.Net.MenuPanel oMenuPanel = new Ext.Net.MenuPanel();
            oMenuPanel.Frame = true;
            oMenuPanel.ID = strID;

            return oMenuPanel;
        }


        // Présentation choisie : Accordéon
        public Ext.Net.Panel Create_ExtAccordionPanel(string strID, string strTitle, Ext.Net.Icon iIcon, bool bCollapsible, bool bCollapsed, string strLayout)
        {
            Ext.Net.Panel oPanel = new Ext.Net.Panel();
            oPanel.ID = strID;
            oPanel.Title = strTitle;
            if (iIcon != Icon.Cancel)
            {
                oPanel.Icon = iIcon;
            }
            oPanel.Collapsible = bCollapsible;
            oPanel.Collapsed = bCollapsed;
            //oPanel.BodyPadding = 2;
            //oPanel.Margin = 2;
            oPanel.AnimCollapse = false;
            if (!string.IsNullOrEmpty(strLayout))
            {
                oPanel.Layout = strLayout;
            }
            oPanel.Frame = false;
            /* ajouter par Angoua pour changer l'arrière plan du panel */            
            oPanel.BodyCls = "x-panel-function";            

            return oPanel;
        }



        public Ext.Net.Panel Create_ExtPanel(string strID, string strTitle, Ext.Net.Icon iIcon, bool bCollapsible, bool bCollapsed, string strLayout)
        {
            Ext.Net.Panel oPanel = new Ext.Net.Panel();
            oPanel.ID = strID;
            oPanel.Title = strTitle;
            if (iIcon != Icon.Cancel)
            {
                oPanel.Icon = iIcon;
            }
            oPanel.Collapsible = bCollapsible;
            oPanel.Collapsed = bCollapsed;
            oPanel.BodyPadding = 2;
            oPanel.Margin = 2;
            oPanel.AnimCollapse = false;
            if (!string.IsNullOrEmpty(strLayout))
            {
                oPanel.Layout = strLayout;
            }
            oPanel.Frame = false;
            /* ajouter par Angoua pour changer l'arrière plan du panel */
            oPanel.RemoveBodyCls("x-panel-body-default-framed");
            oPanel.AddBodyCls("x-panel-function");
            

            /**
             * Ajout de la fonctionnalité qui permet de pouvoir fermer les autres onglets
             * ouverts afin de gagner de l'espace
             */
            oPanel.Listeners.BeforeExpand.Handler = "Ext.net.Bus.publish('App.collapseExpand',{item : this} )";

            return oPanel;
        }
        public Ext.Net.MenuItem Create_ExtMenuItem(string strID, string strText, Ext.Net.Icon iIcon)
        {
            Ext.Net.MenuItem oMenuItem = new Ext.Net.MenuItem();
            oMenuItem.ID = strID;
            oMenuItem.Text = strText;
            oMenuItem.Icon = iIcon;
            oMenuItem.SetActive(true);
            oMenuItem.Cls = "clsModule";

            return oMenuItem;
        }

        public void Select_ExtMenuItem(Ext.Net.MenuPanel oMenuPanel, Ext.Net.MenuItem oMenuItem, ClientScriptManager csScriptManager)
        {
            // Define the name and type of the client scripts on the page.
            String csName = "clientScript";
            Type csType = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            //ClientScriptManager cs = Page.ClientScript;
            ClientScriptManager cs = csScriptManager;

            // Check to see if the startup script is already registered.
            //if (!cs.IsStartupScriptRegistered(csType, csName))
            //{
            //    String csText = "Ext.onReady(function () { Ext.getCmp('" + oMenuPanel.ID + "').setSelection(Ext.getCmp('" + oMenuItem.ID + "')); });";
            //    cs.RegisterStartupScript(csType, csName, csText, true);
            //}
            String csText = "Ext.onReady(function () { Ext.getCmp('" + oMenuPanel.ID + "').setSelection(Ext.getCmp('" + oMenuItem.ID + "')); });";
            X.Js.AddScript(csText);
        }
        //public Ext.Net.LinkButton Create_ExtLinkButton(string strID, string strTitle, Ext.Net.Icon iIcon, string strUrl)
        //{
        //    Ext.Net.LinkButton oLinkButton = new Ext.Net.LinkButton();
        //    oLinkButton.ID = strID;
        //    oLinkButton.Text = strTitle;
        //    oLinkButton.Margin = 2;
        //    if (iIcon != Icon.Cancel)
        //    {
        //        oLinkButton.Icon = iIcon;
        //    }
        //    oLinkButton.OnClientClick = strUrl;
        //    oLinkButton.Shadow = true;
        //    oLinkButton.ShadowMode = ShadowMode.Sides;
        //    oLinkButton.Cls = "clsLinkButton";

        //    return oLinkButton;
        //}
        //public Ext.Net.LinkButton Create_ExtLinkButton(string strID, string strTitle, Ext.Net.Icon iIcon, Ext.Net.ComponentDirectEvent.DirectEventHandler Event)
        //{
        //    Ext.Net.LinkButton oLinkButton = new Ext.Net.LinkButton();
        //    oLinkButton.ID = strID;
        //    oLinkButton.Text = strTitle;
        //    oLinkButton.Margin = 2;
        //    oLinkButton.Icon = iIcon;
        //    //oLinkButton.OnClientClick = strUrl;
        //    oLinkButton.Shadow = true;
        //    oLinkButton.ShadowMode = ShadowMode.Sides;
        //    oLinkButton.Cls = "clsLinkButton";
        //    oLinkButton.DirectEvents.Click.Event += Event;

        //    return oLinkButton;
        //}

        public Ext.Net.Button Create_ExtMenuButton(string strID, string strTitle)
        {
            Ext.Net.Button oButton = new Ext.Net.Button();
            oButton.ID = strID;
            oButton.Text = strTitle;
            /*ajouter par Angoua pour gérer le survol des boutons menu */
            oButton.OverCls = "hover";

            return oButton;
        }
        public Ext.Net.Menu Create_ExtMenu(string strID)
        {
            Ext.Net.Menu oMenu = new Ext.Net.Menu();
            oMenu.ID = strID;

            /* Ajout de la propriété des css pour gérer l'arrière plan des menus déroulants. 
             *voir le fichier Styles/Site.css */
            oMenu.BodyCls = "x-Menu-body";
            oMenu.ShowSeparator = false;
            return oMenu;
        }
        
        public Ext.Net.MenuItem Create_ExtMenuItem(string strID, string strText, string strUrl)
        {
            Ext.Net.MenuItem oMenuItem = new Ext.Net.MenuItem();
            oMenuItem.ID = strID;
            oMenuItem.Text = strText;
            oMenuItem.OnClientClick = strUrl;

            /* classe qui permet de changer la couleur de la police du menuItem. 
             * voir fichier Styles/Site.css */
            oMenuItem.Cls = "x-btn-MenuItem";


            return oMenuItem;
        }


        public void OpenTab(string tabPanel, string id, string url, string menuItem, string menutitle, ClientScriptManager csScriptManager)
        {
            String csName = "clientScript";
            Type csType = this.GetType();

            ClientScriptManager cs = csScriptManager;

            if (!cs.IsStartupScriptRegistered(csType, csName))
            {
                String csText = "var addTab = ";
                csText += "function (" + tabPanel + ", " + id + ", " + url + ", " + menuItem + ", " + menutitle + ") ";
                csText += "{var tab = " + tabPanel + ".getComponent(" + id + "); ";
                csText += "if (!tab) {tab = " + tabPanel + ".add({id: " + id + ", title: " + menutitle + ", closable: true, menuItem: " + menuItem + ", loader: { url: " + url + ", renderer: 'frame', loadMask: {showMask: true, msg: 'Loading ' + url + '...'} } }); } tabPanel.setActiveTab(tab);}";
                cs.RegisterStartupScript(csType, csName, csText, true);
            }
        }


        public void OpenReportTab()
        {
            //X.AddScript("top.addReportTab(parent.window.Ext.getCmp('tabPanelContent'), 'idSituationConnais', 'Situation des Camions à Quai', 'Sourcing/FrmReportViewer.aspx');");        
            X.Js.Call("top.addReportTab", "parent.window.Ext.getCmp('tabPanelContent')", "idSituationConnais", "Situation des Camions à Quai", "Sourcing/FrmReportViewer.aspx");
        }


        public void OpenPDFTab(object data_to_sent)
        {
            //X.Js.Call("top.addReportTab", "parent.window.Ext.getCmp('tabPanelContent')", "idSituationConnais", "Situation des Camions à Quai", "Sourcing/FrmPDFViewer.aspx");
            MessageBus.Default.Publish("Msg.DisplayReport", JsonConvert.SerializeObject(data_to_sent, Formatting.Indented));
        }


        public void OpenReportTab(object data_to_sent)
        {
            MessageBus.Default.Publish("Msg.DisplayReport", JsonConvert.SerializeObject(data_to_sent, Formatting.Indented));
        }


        public bool GeneratePdfFromReport(ReportDocument mReport, string idReport, string libReport , string mPath)
        {

            try
            {
                bool mGenerated = false;
                
                //string ExportPath;
                
                //String fname;
                
                //fname = idReport + ".pdf";
                                
                // Export the report
                //AssignConnection(mReport);
                mReport.ExportToDisk(ExportFormatType.PortableDocFormat, mPath);
                
                mGenerated = true;

                return mGenerated;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }


        private ConnectionInfo ConInfo = new ConnectionInfo();
        public void AssignConnection(ReportDocument mReport)
        {
            DataConfig mConfig = new DataConfig();
            ConInfo.DatabaseName = mConfig.DataBase;
            ConInfo.ServerName = mConfig.Server;
            ConInfo.IntegratedSecurity = true;
            ConInfo.Type = ConnectionInfoType.SQL;

            ReportClass _ReportDocument;

            if (mReport is ReportClass)
            {
                _ReportDocument = (ReportClass)mReport;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in _ReportDocument.Database.Tables)
                {
                    TableLogOnInfo logOnInfo = table.LogOnInfo;
                    logOnInfo.ConnectionInfo = ConInfo;
                    table.ApplyLogOnInfo(logOnInfo);
                    table.LogOnInfo.ConnectionInfo.ServerName = ConInfo.ServerName;
                    table.Location = ConInfo.DatabaseName + ".dbo." + table.Location.Substring(table.Location.LastIndexOf(".") + 1);
                }
            }

            foreach (CrystalDecisions.CrystalReports.Engine.Section section in mReport.ReportDefinition.Sections)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.ReportObject repObj in section.ReportObjects)
                {
                    if (repObj.Kind == ReportObjectKind.SubreportObject)
                    {
                        CrystalDecisions.CrystalReports.Engine.SubreportObject SubReport = (SubreportObject)repObj;
                        CrystalDecisions.CrystalReports.Engine.ReportDocument RepDocument = SubReport.OpenSubreport(SubReport.Name);
                        if (object.Equals(RepDocument, null))
                        {
                            foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in RepDocument.Database.Tables)
                            {
                                CrystalDecisions.Shared.TableLogOnInfo logOnInfo = oTable.LogOnInfo;
                                logOnInfo.ConnectionInfo = ConInfo;
                                oTable.ApplyLogOnInfo(logOnInfo);
                                oTable.Location = ConInfo.DatabaseName + ".dbo." + oTable.Location.Substring(oTable.Location.LastIndexOf(".") + 1);
                            }
                        }
                    }
                }
            }
        }
        
        public void ShowReportBeforePrint(ReportDocument mReport, string idReport, string libReport, bool showPrintButton, bool showExportButton)
        {
            // Declarations
            //cwaReportInfos mReportInformations = new cwaReportInfos();
            //mReportInformations.Report = mReport;

            //Page.Session.Remove("ReportInfos");
            //Page.Session["ReportInfos"] = mReportInformations;

            //string id = idReport;
            //string lib = libReport;

            //mReportInformations.ShowPrintButton = showPrintButton;
            //mReportInformations.ShowExportButton = showExportButton;

            //MenuControls oWebMenuControls = new MenuControls();
            //oWebMenuControls.OpenReportTab(id + "&" + lib);



        }



        //************************************************


        #endregion
    }

   
}

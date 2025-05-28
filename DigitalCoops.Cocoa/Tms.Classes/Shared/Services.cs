using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
//using Ext.Net;
using Newtonsoft.Json;
using ServiceLib;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Web.Compilation;
using Tms.Classes;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Settings;


public class Services : IService
{

    // Permet de télécharger les états crystal reports du serveur web vers le client
    string IService.DownLoadFile(byte[] binaryData)
    {
        //string downLoadFolder = @"C:\Impressions";
        string downLoadFolder = string.Empty;
        string mDossierEtat = "Etats";

        // Recuperation du chemin du dossier de l'utilisateur
        string repertoireEtat = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), mDossierEtat);
        downLoadFolder = repertoireEtat;

        if (!Directory.Exists(downLoadFolder))
            Directory.CreateDirectory(downLoadFolder);

        string extension = ".rpt";

        string pathFile = String.Empty;

        string fileName = Guid.NewGuid().ToString() + extension;
        pathFile = System.IO.Path.Combine(downLoadFolder, fileName);

        try
        {
            File.WriteAllBytes(pathFile, binaryData);
            return pathFile;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // Liste les imprimantes installées sur le client
    List<string> IService.PrintersInstalled()
    {
        try
        {
            List<string> mListe = new List<string>();

            foreach (string oItem in PrinterSettings.InstalledPrinters)
            {
                mListe.Add(oItem);
            }

            return mListe;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // Liste les bacs disponiibles sur l'imprimante par defaut du client
    List<string> IService.TraysInstalled()
    {
        try
        {
            PrinterSettings mPrinterSettings = new PrinterSettings();

            List<string> mListe = new List<string>();

            foreach (System.Drawing.Printing.PaperSource rpSource in mPrinterSettings.PaperSources)
            {
                mListe.Add(rpSource.SourceName.Trim());
            }

            return mListe;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // Obtenir l'imprimante par défaut du client
    string IService.GetDefaultPrinter()
    {
        string mDefaultPrinter = string.Empty;

        PrinterSettings oPs = new PrinterSettings();

        foreach (string oItem in PrinterSettings.InstalledPrinters)
        {
            oPs.PrinterName = oItem;

            if (oPs.IsDefaultPrinter)
            {
                mDefaultPrinter = oItem;
                break;
            }
        }

        return mDefaultPrinter;
    }

    // Imprimer automatiquement l'état (impression directe vers l'imprimante)
    string IService.DirectPrintReport(string pathFile, string mListeParametre, string printerName, int nCopies, bool collated, int startPageN, int endPageN, string trayName)
    {
        try
        {
            //ReportDocument _Report = new ReportDocument();
            //XtraReport _Report = new DevExpress.XtraReports.UI.XtraReport();

            //// Charger le fichier RPT
            //_Report.Load(pathFile);
            //_Report.

            //// Connect to  DB
            //ChangeLogOnInfos(_Report);


            //// Deserializer la collection des parametres               
            //List<StringReportParameter> mParametres = new List<StringReportParameter>();
            //mParametres = JsonConvert.DeserializeObject<List<StringReportParameter>>(mListeParametre);

            //if (mParametres.Count > 0)
            //{
            //    //Application des parametres à l'état                    
            //    foreach (StringReportParameter oItem in mParametres)
            //    {
            //        _Report.SetParameterValue(oItem.Name, oItem.Value);
            //    }


            //    // Impression  
            //    _Report.PrintOptions.PrinterName = printerName;

            //    //Le bac à utiliser ?
            //    if (!string.IsNullOrEmpty(trayName))
            //    {
            //        try
            //        {
            //            System.Drawing.Printing.PrinterSettings mPrinterSettings = new System.Drawing.Printing.PrinterSettings();
            //            foreach (System.Drawing.Printing.PaperSource rpSource in mPrinterSettings.PaperSources)
            //            {
            //                if (rpSource.SourceName.Trim().Equals(trayName))
            //                {
            //                    _Report.PrintOptions.CustomPaperSource = rpSource;
            //                    break;
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            //use automatic tray
            //            throw ex;
            //        }
            //    }

            //    _Report.PrintToPrinter(nCopies, collated, startPageN, endPageN);

            //    // Suppression du fichier temporaire de l'état coté client
            //    File.Delete(pathFile);
            //}

            return string.Empty;

        }
        catch (Exception ex)
        {
            throw ex;
            return ex.Message;
        }
    }

    string IService.DirectPrintTicket(string ParamList, string Report)
    {
        try
        {
            XtraReport report = null;
            switch (Report)
            {
                case EnumReportDefinition.TICKETPESEECAISSE:
                    report = new TicketPeseeCaisse() as XtraReport;
                    break;
                case EnumReportDefinition.TICKETPESEENORMAL:
                    report = new TicketPeseeNormal() as XtraReport;
                    break;
                case EnumReportDefinition.BONDELIVRAISON:
                    report = new rptBonDeLivraison() as XtraReport;
                    break;
                case EnumReportDefinition.FACTURE:
                    report = new rptInvoice() as XtraReport;
                    break;
                case EnumReportDefinition.PAYEMENT:
                    report = new rptVoucherDelivery() as XtraReport;
                    break;
                case EnumReportDefinition.PAYEMENTTRANSPORT:
                    report = new rptVoucherTransport() as XtraReport;
                    break;
                default:
                    break;
            }

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            List<ReportParam> _ListParamReport = new List<ReportParam>();
            _ListParamReport = Ext.Net.JSON.Deserialize<List<ReportParam>>(ParamList, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            Parameter param = new DevExpress.XtraReports.Parameters.Parameter();
            foreach (var item in _ListParamReport)
            {
                param.Name = item.Name;
                param.Value = item.Value;
                param.Visible = false;
                report.Parameters.Add(param);
                param = new Parameter();
            }

            report.ShowPrintMarginsWarning = false;
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.PrintingSystem.StartPrint +=
                new PrintDocumentEventHandler(printingSystem_StartPrint);
            pt.Print();
            return string.Empty;

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    string IService.DirectPrintTicketDevXRpt(string reportName, string parameters)
    {
        try
        {
            //XtraReport report = new XtraReport();
            TicketPeseeCaisse report = new TicketPeseeCaisse();
            report.DataSource = DevExpressReportDs.SetDataSource(report);
            //XtraReport report = XtraReport.FromFile(reportName, true);            
            //object instance = Activator.CreateInstance(BuildManager.GetType(reportName, true));
            //report = (XtraReport)instance;
            //string reportFilePath = @"C:\Temp\Report1.repx";
            // Load a report's layout from XML. 
            //if (System.IO.File.Exists(reportName))
            //{
            //    report.LoadLayoutFromXml(reportName);
            //}
            //else {
            //    System.Console.WriteLine("The source file does not exist.");
            //}
            //XtraReport report = null;
            // XtraReport report = reportName as XtraReport;
            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            List<ReportParam> re = new List<ReportParam>();
            re = Ext.Net.JSON.Deserialize<List<ReportParam>>(parameters, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            Parameter param = new DevExpress.XtraReports.Parameters.Parameter();
            List<Parameter> mList = new List<Parameter>();

            foreach (var item in re)
            {
                param.Name = item.Name;
                param.Value = item.Value;
                param.Visible = false;
                report.Parameters.Add(param);
                param = new Parameter();
            }

            report.ShowPrintMarginsWarning = false;
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.PrintingSystem.StartPrint +=
                new PrintDocumentEventHandler(printingSystem_StartPrint);
            pt.Print();

            //if (File.Exists(reportName))
            //{
            //    File.Delete(reportName);
            //}

            return string.Empty;

        }
        catch (Exception ex)
        {
            //throw ex;
            return ex.Message;
        }
    }


    private void printingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
    {
        // Set the printer name.
        //e.PrintDocument.PrinterSettings.PrinterName =
        //    PrinterSettings.InstalledPrinters[0];       
        foreach (string name in PrinterSettings.InstalledPrinters)
        {
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = name;
            if (ps.IsDefaultPrinter)
                e.PrintDocument.PrinterSettings.PrinterName = name;
        }
    }

    //Obtenir l'imprimante du  ticket configurée (stockée dans un fichier xml coté client)    
    string IService.GetTicketPrinter()
    {
        try
        {

            string mCurrentPrinterName = string.Empty;
            List<Setting> mSettings = SettingsManager.GetOverviewSettings("WebPrinters.Ticket");


            foreach (Setting mSetting in mSettings)
            {
                if (mSetting.Key == "TicketPrinter") mCurrentPrinterName = (string)mSetting.Value;
            }

            return mCurrentPrinterName;

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    // Obtenir l'imprimante code barre configurée (stockée dans un fichier xml coté client)   
    string IService.GetBarCodePrinter()
    {
        try
        {

            string mCurrentPrinterName = string.Empty;
            //List<Setting> mSettings = SettingsManager.GetOverviewSettings("WebPrinters.Barcode");

            //foreach (Setting mSetting in mSettings)
            //{
            //    if (mSetting.Key == "BarCodePrinter") mCurrentPrinterName = (string)mSetting.Value;
            //}

            return mCurrentPrinterName;

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    // Obtenir le bac de l'imprimante (stockée dans un fichier xml coté client)  
    string IService.GetTrayName()
    {
        try
        {
            string mCurrentPrinterName = string.Empty;
            List<Setting> mSettings = SettingsManager.GetOverviewSettings("WebPrinters.Trays");


            foreach (Setting mSetting in mSettings)
            {
                if (mSetting.Key == "Tray") mCurrentPrinterName = (string)mSetting.Value;
            }

            return mCurrentPrinterName;

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    // Stocker le nom de l'imprimante  ticket sélectionnée dans un fichier xml coté client
    void IService.SaveTicketPrinter(string mCurrentPrinterName)
    {
        try
        {
            List<Setting> mSettings = new List<Setting>();
            Setting mSetting;

            mSetting = new Setting();
            mSetting.Key = "TicketPrinter";
            mSetting.Value = mCurrentPrinterName;
            mSettings.Add(mSetting);

            SettingsManager.SaveOverviewSettings("WebPrinters.Ticket", mSettings);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }


    // Stocker le nom de l'imprimante code barre sélectionnée dans un fichier xml coté client
    void IService.SaveBarCodePrinter(string mCurrentPrinterName)
    {
        try
        {
            //List<Setting> mSettings = new List<Setting>();
            //Setting mSetting;

            //mSetting = new Setting();
            //mSetting.Key = "BarCodePrinter";
            //mSetting.Value = mCurrentPrinterName;
            //mSettings.Add(mSetting);

            //SettingsManager.SaveOverviewSettings("WebPrinters.Barcode", mSettings);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }


    // Stocker le nom du bac sélectionnée dans un fichier xml coté client
    void IService.SaveTrayName(string mCurrentPrinterName)
    {
        try
        {
            List<Setting> mSettings = new List<Setting>();
            Setting mSetting;

            mSetting = new Setting();
            mSetting.Key = "Tray";
            mSetting.Value = mCurrentPrinterName;
            mSettings.Add(mSetting);

            SettingsManager.SaveOverviewSettings("WebPrinters.Trays", mSettings);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    // Obtenir le poids capturé coté client
    int IService.GetWeight()
    {
        try
        {
            Bascule mDevice = new Bascule(Environment.MachineName);
            //decimal mPoidsInitial = 0;
            int mPoidsInitial = mDevice.fnRead();
            return mPoidsInitial;
        }
        catch (Exception ex)
        {
            //throw ex; 
            throw new Exception("Erreur:" + ex.Message);
            return -2;
        }
    }

    int IService.GetWeightTest(string values)
    {
        try
        {

            //DataConfig mConfig = new DataConfig();
            //Bascule mDevice = Ext.Net.JSON.Deserialize<Bascule>(values, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            //decimal mPoidsInitial = 0;
            Bascule mDevicee = new Bascule(Environment.MachineName);
            Bascule mDevice = new Bascule();
            mDevice.Protocole.ID = int.Parse(values);
            mDevice.Designation = "test";
            int mPoidsInitial = mDevice.fnRead();
            return mPoidsInitial;
        }
        catch (Exception ex)
        {
            //throw ex;
            return -1;
        }
    }

    //private void ChangeLogOnInfos(ReportDocument mReport)
    //{
    //    CwaConfig mConfig = new CwaConfig();
    //    ConnectionInfo ConInfo = new ConnectionInfo();
    //    ConInfo.DatabaseName = mConfig.DataBase;
    //    ConInfo.ServerName = mConfig.Server;
    //    ConInfo.IntegratedSecurity = true;
    //    ConInfo.Type = ConnectionInfoType.SQL;

    //    foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in mReport.Database.Tables)
    //    {
    //        CrystalDecisions.Shared.TableLogOnInfo logOnInfo = oTable.LogOnInfo;
    //        logOnInfo.ConnectionInfo = ConInfo;
    //        oTable.ApplyLogOnInfo(logOnInfo);
    //        //
    //        oTable.Location = ConInfo.DatabaseName + ".dbo." + oTable.Location.Substring(oTable.Location.LastIndexOf(".") + 1);
    //    }

    //    foreach (CrystalDecisions.CrystalReports.Engine.Section section in mReport.ReportDefinition.Sections)
    //    {
    //        foreach (CrystalDecisions.CrystalReports.Engine.ReportObject repObj in section.ReportObjects)
    //        {
    //            if (repObj.Kind == ReportObjectKind.SubreportObject)
    //            {
    //                CrystalDecisions.CrystalReports.Engine.SubreportObject SubReport = (SubreportObject)repObj;
    //                CrystalDecisions.CrystalReports.Engine.ReportDocument RepDocument = SubReport.OpenSubreport(SubReport.Name);
    //                foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in RepDocument.Database.Tables)
    //                {
    //                    CrystalDecisions.Shared.TableLogOnInfo logOnInfo = oTable.LogOnInfo;
    //                    logOnInfo.ConnectionInfo = ConInfo;
    //                    oTable.ApplyLogOnInfo(logOnInfo);
    //                    //
    //                    oTable.Location = ConInfo.DatabaseName + ".dbo." + oTable.Location.Substring(oTable.Location.LastIndexOf(".") + 1);
    //                }
    //            }
    //        }
    //    }
    //}



    // Obtenir la nom de la machine
    string IService.GetMachineName()
    {
        return Environment.MachineName;
    }


}

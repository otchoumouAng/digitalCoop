using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Tms2017.MVC.Controllers
{
    public class ReportController : Controller
    {

        private string conn = ConfigurationManager.ConnectionStrings["TMS2017_ReportServer"].ConnectionString;

        // GET: Report
        public ActionResult Index()
        {           
            return View();
        }

        public ReportController()
        {
            
        }
        
        BanqueListe report = new BanqueListe();
        //ReportWithParam reportwithparam = new ReportWithParam();


        public ActionResult DocumentViewerPartial()
        {
            //report.DataSource = BindToData();
            report.DataSource = DevExpressReportDs.SetDataSource(report);
            return PartialView("_DocumentViewerPartial", report);
        }

        public ActionResult ViewBalanceReportExport()
        {
            report.DataSource = DevExpressReportDs.SetDataSource(report);
            return DocumentViewerExtension.ExportTo(report, Request);
        }

        //public ActionResult DocumentViewerWithParamPartial()
        //{
        //    //report.Parameters["yourParameter1"].Value = 1; 
        //    reportwithparam.DataSource = DevExpressReportDs.SetDataSource(reportwithparam);
        //    reportwithparam.Parameters["banqueID"].Value = 1;
        //    reportwithparam.Parameters["banqueID"].Visible = false;
        //    return PartialView("DocumentViewerWithParamPartial", reportwithparam);
        //}
        

        public ActionResult ReportWithParam()
        {
            return View();
        }

        public ActionResult ExportToExcel(string ItemList)
        {
            try
            {
                XslCompiledTransform xt = new XslCompiledTransform();
                //var submitData = new Ext.Net.SubmitHandler(data);
                StoreSubmitDataEventArgs submitData = new StoreSubmitDataEventArgs(ItemList, null);
                XmlNode xml = submitData.Xml;

                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";

                this.Response.AddHeader("Content-Disposition", "attachment; filename=submittedData.xls");
                xt.Load(Server.MapPath("~/Resources/Excel.xsl"));
                xt.Transform(xml, null, this.Response.OutputStream);
                this.Response.End();                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Export : Error",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();

        }

        public ActionResult ExpoToExcel(string ItemList)
        {
            Ext.Net.SubmitHandler submitData = new Ext.Net.SubmitHandler(ItemList);
            XmlNode xml = submitData.Xml;

            XslCompiledTransform xtCsv = new XslCompiledTransform();
            xtCsv.Load(Server.MapPath("/Resources/xsl/Csv.xsl"));
            StringBuilder s = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                ConformanceLevel = ConformanceLevel.Auto
            };
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(s, settings);
            xtCsv.Transform(xml, writer);

            var result = new FileContentResult(Encoding.UTF8.GetBytes(s.ToString()), "application/octet-stream");
            result.FileDownloadName = "Temp.csv";
            return result;
        }

        public ActionResult ExportListToExcel(string ItemList, string format = "xls")
        {
            Ext.Net.SubmitHandler submitData = new Ext.Net.SubmitHandler(ItemList);
            XmlNode xml = submitData.Xml;
            string contentType = string.Empty;
            byte[] resultFileContent = System.Text.Encoding.UTF8.GetBytes(xml.OuterXml);

            switch (format)
            {
                case "xml":
                    contentType = "application/xml";
                    break;

                case "xls":
                case "csv":
                    XslCompiledTransform transform = new XslCompiledTransform();

                    if (format == "xls")
                    {
                        contentType = "application/vnd.ms-excel";
                        transform.Load(Server.MapPath("~/Resources/Excel.xsl"));
                    }
                    else
                    {
                        contentType = "application/octet-stream";
                        transform.Load(Server.MapPath("~/Resources/xsl/Csv.xsl"));
                    }

                    MemoryStream m = new MemoryStream(resultFileContent);
                    XPathDocument xpathDoc = new XPathDocument(new StreamReader(m));
                    StringBuilder resultString = new StringBuilder();
                    System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(resultString);
                    transform.Transform(xpathDoc, writer);
                    resultFileContent = Encoding.UTF8.GetBytes(resultString.ToString());
                    break;
            }

            var result = new FileContentResult(resultFileContent, contentType);
            result.FileDownloadName = string.Format("TempoMillenium.{0}", format);
            return result;
        }

        //public ActionResult ExporToExcel(string ItemList)
        //{
            
        //    return result;
        //}

    }
}
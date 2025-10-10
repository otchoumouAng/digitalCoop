using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tms.Components.Settings;

namespace Tms.Components.Reports
{
    public class Reporting
    {
        ReportInfos mReportInformations;

        ReportDocument _Report;

        MenuControls oWebMenuControls;

        string idReport;

        string libReport;

        string folderName = "PDF";

        public HttpContextBase httpCtx;
        public HttpResponse responseCtx;

        string filePath;

        Dictionary<string, string> _Parameters;

        public Dictionary<string, string> Parameters
        {
            get
            {
                return _Parameters;
            }

            set
            {
                _Parameters = value;
            }
        }

        public string IdReport
        {
            get
            {
                return idReport;
            }
        }

        public string LibReport
        {
            get
            {
                return libReport;
            }

            set
            {
                libReport = value;
            }
        }

        public ReportDocument Report
        {
            get
            {
                return _Report;
            }

            set
            {
                _Report = value;
            }
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }
        }

        public Reporting()
        {
            mReportInformations = new ReportInfos();

            oWebMenuControls = new MenuControls();

            idReport = Guid.NewGuid().ToString();

            libReport = string.Empty;          

            Parameters = new Dictionary<string, string>();

        }

        public bool GeneratePDF()
        {
            if (_Report == null)
                throw new Exception(this.GetType().FullName + " : Please define initialize property <Report>.");

            filePath = SetPDFReportPath();

            ConnectReport(Report);

            AssignParameters();

            return oWebMenuControls.GeneratePdfFromReport(_Report, idReport, libReport, filePath);
        }

        private string SetPDFReportPath()
        {
            // Création du repertoire des fichiers PDF
            string folderName = "PDF";

            string folderPath = System.IO.Path.Combine(httpCtx.Server.MapPath(@"~/Content/"), folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            // Génération du rapport sous format PDF              
            string mPath = folderPath + @"\" + idReport + ".pdf";

            filePath = mPath;

            return mPath;
        }
        
        private string SetExcelReportPath()
        {
            // Création du repertoire des fichiers PDF
            string folderName = "XLS";

            string folderPath = System.IO.Path.Combine(httpCtx.Server.MapPath(@"~/Content/"), folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            // Génération du rapport sous format PDF              
            string mPath = folderPath + @"\" + idReport + ".xls";

            filePath = mPath;

            return mPath;
        }

        private void AssignParameters()
        {
            if (_Parameters.Count > 0)
            {
                foreach (var item in _Parameters)
                {
                    _Report.SetParameterValue(item.Key, item.Value);
                }
            }
        }

        private void ConnectReport(ReportDocument mReport)
        {
            oWebMenuControls.AssignConnection(mReport);
        }

        public Stream ExportToPDFStream()
        {
            if (Report == null)
                throw new Exception(this.GetType().FullName + " : Please define initialize property <Report>.");


            filePath = SetPDFReportPath();

            ConnectReport(Report);

            AssignParameters();

            Stream stream = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);           

            return stream;
        }


        public void ExportToHttpStream()
        {
            if (Report == null)
                throw new Exception(this.GetType().FullName + " : Please define initialize property <Report>.");


            filePath = SetPDFReportPath();

            ConnectReport(Report);

            AssignParameters();                         

            Report.ExportToHttpResponse(new ExportOptions {  ExportFormatType = ExportFormatType.PortableDocFormat}, responseCtx, true, "attachment");

        }


        public Stream ExportToExcelStream()
        {
            if (Report == null)
                throw new Exception(this.GetType().FullName + " : Please define initialize property <Report>.");

            filePath = SetExcelReportPath();

            ConnectReport(Report);

            AssignParameters();

            Stream stream = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);

            return stream;
        }

    }
}


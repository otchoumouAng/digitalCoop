using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using Ext.Net;
using CwaWebComponents;
using System.Net;
using Tms.Components.Settings;
using Tms.Components.Reports;

namespace Tms2017.Reports
{
    public partial class FrmPDFViewer : BasePage
    {
        

        #region "Methods"
        

        #endregion

        #region "Events"

        protected void Page_Init(object sender, EventArgs e)
        {
            var mReportInfos = (ReportInfos)Session["ReportInfos"];                 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Queue<string> mQueue = new Queue<string>();

                if (Session["QueueFileName"] != null)
                    mQueue = (Queue<string>)Session["QueueFileName"];

                if (mQueue.Count > 0)
                {
                    string strFileName = mQueue.Dequeue();

                    DisplayPDF(strFileName);

                    DeleteFile(strFileName);

                    Session["QueueFileName"] = mQueue;
                } 
            }
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            
        }

        private void DisplayPDF(string fileName)
        {
            try
            {
                string FilePath = fileName;//Server.MapPath(fileName);
                System.Net.WebClient User = new System.Net.WebClient();
                Byte[] FileBuffer = User.DownloadData(FilePath);
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }
                
                
                DeleteFile(FilePath);
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message + "FrmPDFViewer:Page_Load").Show();
            }

        }

        private static void DeleteFile(string FilePath)
        {
            // Delete file                
            System.IO.File.Delete(FilePath);
        }


        public void SetQueueNameSession(Queue<string> mQueue)
        {
            Page.Session["QueueFileName"] = mQueue;
        }

        public Queue<string> GetQueueNameSession(Queue<string> mQueue)
        {
            if (Page.Session["QueueFileName"] != null)
                mQueue = (Queue<string>)Session["QueueFileName"];
            else
                Session["QueueFileName"] = new Queue<string>();
            return mQueue;
        }

        

        #endregion
    }
}
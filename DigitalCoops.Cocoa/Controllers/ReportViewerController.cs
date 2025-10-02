using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Components.Reports;
using Tms2017.MVC.Reports;

namespace Tms2017.MVC.Controllers
{
    public class ReportViewerController : Controller
    {
        // GET: ReportViewer
        public ActionResult Index()
        {
            ViewBag.UserID = (Guid)Session["userID"] == null ? Guid.Empty : (Guid)Session["userID"];
            return View();
        }



        public ActionResult OnDisplayReport()
        {
            string FilePath = (string)Session["report_path"];

            if (!String.IsNullOrEmpty(FilePath))
            {
                FilePathResult mFile = new FilePathResult(FilePath, "application/pdf");               

                return mFile;               
            }

            return Content("An error occured during the report generation.");
        }

        [NonAction]
        private void DeleteFile(string FilePath)
        {
            // Delete file                
            System.IO.File.Delete(FilePath);
        }

        public Ext.Net.MVC.PartialViewResult RenderPartialListReportItems(string moduleID)
        {
            //List<Models.Module> mItems = Models.Module.GetAll();
            if (string.IsNullOrEmpty(moduleID))
            {
                return null;
            }
            Fonction mClass = new Fonction();
            Guid IdModule = Guid.Parse(moduleID);
            Guid IDUtilisateur = (Guid)Session["userID"];                        

            var mList = new Tms.Classes.Security.Fonction().fnSelectReport(IDUtilisateur, (Guid)IdModule);

            List<Tms.Classes.Security.Fonction> listFonction = new List<Tms.Classes.Security.Fonction>();

            for (int i = 0; i < mList.Count; i++)
            {               
                listFonction.Add(mList[i] as Tms.Classes.Security.Fonction);
            }

            return new Ext.Net.MVC.PartialViewResult
            {
                ViewName = "PartialViewsReportItems",
                ContainerId = "PanelMenuReport",
                Model = listFonction,
                ClearContainer = true,
                RenderMode = Ext.Net.RenderMode.AddTo
            };
            //return PartialView("PartialViewReportItems", listFonction);
        }
    }
}
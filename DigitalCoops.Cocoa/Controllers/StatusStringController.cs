using Ext.Net;
using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class StatusStringController : Controller
    {
        // GET: Statut
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadForFinancing()
        {
            List<StatusString> mList = new List<StatusString>();
            StatusString mStatut = new StatusString();
            mStatut.ID = "%%";
            mStatut.Description = "{Tous}";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "NA";
            mStatut.Description = "Non Approuvé";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "AP";
            mStatut.Description = "Approuvé";
            mList.Add(mStatut);

            return this.Store(mList);
        }



        public ActionResult LoadForSuppliers()
        {
            List<StatusString> mList = new List<StatusString>();
            StatusString mStatut = new StatusString();
            mStatut.ID = "-1";
            mStatut.Description = "{Tous}";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "0";
            mStatut.Description = "Enabled";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "1";
            mStatut.Description = "Not Enabled";
            mList.Add(mStatut);

            return this.Store(mList);
        }


        public ActionResult LoadForSuppliersApproval()
        {
            List<StatusString> mList = new List<StatusString>();
            StatusString mStatut = new StatusString();
            mStatut.ID = "-1";
            mStatut.Description = "{Tous}";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "1";
            mStatut.Description = "Approuvé";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "0";
            mStatut.Description = "Pending";
            mList.Add(mStatut);

            return this.Store(mList);
        }



        public ActionResult LoadForWeighings()
        {
            List<StatusString> mList = new List<StatusString>();
            StatusString mStatut = new StatusString();
            mStatut.ID = "-1";
            mStatut.Description = "{Tous}";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "0";
            mStatut.Description = "First Weight";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "1";
            mStatut.Description = "Completed";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "2";
            mStatut.Description = "Manual";
            mList.Add(mStatut);

            mStatut = new StatusString();
            mStatut.ID = "3";
            mStatut.Description = "Cancelled";
            mList.Add(mStatut);

            return this.Store(mList);
        }


    }
}
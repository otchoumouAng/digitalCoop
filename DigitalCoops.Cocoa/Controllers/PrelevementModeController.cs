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
    public class PrelevementModeController : Controller
    {
        // GET: PrelevementMode
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new PrelevementMode().fnSelect();           
            return this.Store(mList);

        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new PrelevementMode().fnSelect();
            PrelevementMode mclass = new PrelevementMode();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);
            
            return this.Store(mList);

        }

    }
}
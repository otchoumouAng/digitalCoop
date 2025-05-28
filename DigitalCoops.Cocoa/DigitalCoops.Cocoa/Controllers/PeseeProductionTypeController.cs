using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class PeseeProductionTypeController : BaseController
    {
        // GET: PeseeProductionType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadActivePeseeProductionType()
        {
            List<DataPersist> mliste = new PeseeProductionType().fnSelect(0);

            return this.Store(mliste);
        }

        public ActionResult LoadActivePeseeProductionTypeAll()
        {
            List<DataPersist> mliste = new PeseeProductionType().fnSelect(0);

            PeseeProductionType lType = new PeseeProductionType();

            lType.ID = -1;
            lType.Designation = "{Tous}";

            mliste.Insert(0, lType);
            lType = mliste[0] as PeseeProductionType;

            return this.Store(mliste);
        }
    }
}
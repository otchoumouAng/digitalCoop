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
    public class ProvenanceFevesController : BaseController
    {
        // GET: ProvenanceFeves
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadActiveProvenanceFeves()
        {
            List<DataPersist> mliste = new ProvenanceFeves().fnSelect(0);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveProvenanceFevesAll()
        {
            List<DataPersist> mliste = new ProvenanceFeves().fnSelect(0);

            ProvenanceFeves ProvenanceFeves = new ProvenanceFeves();

            ProvenanceFeves.ID = -1;
            ProvenanceFeves.Designation = "{Tous}";

            mliste.Insert(0, ProvenanceFeves);
            ProvenanceFeves = mliste[0] as ProvenanceFeves;

            return this.Store(mliste);
        }
    }
}
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
    public class UsineController : Controller
    {
        // GET: Usine
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadActiveUsine()
        {
            List<DataPersist> mliste = new Usine().fnSelect(0);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveUsineAll()
        {
            List<DataPersist> mliste = new Usine().fnSelect(0);

            Usine Usine = new Usine();

            Usine.ID = -1;
            Usine.Nom = "{Tous}";

            mliste.Insert(0, Usine);
            Usine = mliste[0] as Usine;

            return this.Store(mliste);
        }
    }
}
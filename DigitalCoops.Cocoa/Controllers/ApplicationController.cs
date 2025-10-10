using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class ApplicationController : Controller
    {        
        public ActionResult LoadApplication()
        {
            List<DataPersist> listeApplication = (new Application()).fnSelect(0);
            return this.Store(listeApplication);
        }                        
    }
}
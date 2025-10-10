using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tms2017.MVC.Helper;

namespace Tms2017.MVC.Controllers
{
    public class BaseController : Controller
    {
        //protected override void Initialize(RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);

        //    const string culture = "fr-FR";
        //    CultureInfo ci = CultureInfo.GetCultureInfo(culture);

        //    Thread.CurrentThread.CurrentCulture = ci;            
        //    Thread.CurrentThread.CurrentUICulture = ci;

        //}

        protected override void ExecuteCore()
        {
            try
            {
                int culture = 0;
                if (this.Session == null || this.Session["CurrentCulture"] == null)
                {

                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                    this.Session["CurrentCulture"] = culture;
                }
                else
                {
                    culture = (int)this.Session["CurrentCulture"];
                }
                // calling CultureHelper class properties for setting  
                CultureHelper.CurrentCulture = culture;

                base.ExecuteCore();
            }
            catch (Exception Ex)
            {
                //throw new NotImplementedException();
            }
            
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }
    }
}

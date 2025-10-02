using DigitalCoops.Cocoa;
using System.Web;
using System.Web.Mvc;

namespace Tms2017.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());            
        }
    }
}

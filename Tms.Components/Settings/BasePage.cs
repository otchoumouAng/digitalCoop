using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Globalization;

namespace Tms.Components.Settings
{
    public class BasePage : Page
    {
        protected override void InitializeCulture()
        {
            string lang = string.Empty;
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];
            CultureInfo Cul;
            lang = HttpSession.CurrentSessionLanguage();
            Cul = CultureInfo.CreateSpecificCulture(lang);


            System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
            System.Threading.Thread.CurrentThread.CurrentCulture = Cul;

            base.InitializeCulture();
        }
    }
}

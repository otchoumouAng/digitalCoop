using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tms.Components.Settings
{
    public class HttpSession
    {

        public static string CurrentSessionLanguage()
        {
            try
            {
                // get the session variable 'langue' which give the language in 
                // which we want component must be displayed
                //System.Web.HttpContext con = System.Web.HttpContext.Current;
                //string langue = con.Session["langue"].ToString();
                //if (con == null || con.Session["langue"] == null)
                //    return "fr-FR";
                //else
                //    return langue;

                return "en-US";
            }
            catch (Exception)
            {
                return "fr-FR";
            }
        }
        public HttpSession()
        {
        }
    }
}

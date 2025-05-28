using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Shared;

namespace Tms2017.MVC.Controllers
{
    public class ImprimanteController : Controller
    {

        public ImprimanteController()
        {
            string cultureName = "en";
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }
        // GET: Imprimante
        public ActionResult Index()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            ViewData["HiddenIpAddress"] = System.Web.HttpContext.Current.Request.UserHostAddress;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Imprimante" , ViewData = ViewData};
        }

        public ActionResult Select()
        {
            // get ip address
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            //string ip = "154.232.144.153";
            List<Imprimante> PrintersList = new List<Imprimante>();

            string selectedPrinter = string.Empty;

            try
            {
                // initialize services
                ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                // find the selected printer
                selectedPrinter = proxy.GetTicketPrinter();

                // enumerate printers list
                List<string> printersNameList = proxy.PrintersInstalled();

                // create Imprimante list of objects
                bool isSelected = false;

                foreach (string item in printersNameList)
                {
                    isSelected = item.Equals(selectedPrinter);

                    PrintersList.Add(new Imprimante { NomImprimante = item, IsSelected = isSelected });
                }
                
            }
            catch (Exception ex)
            {
                
            }

            return this.Store(PrintersList);
        }


        public ActionResult SelectPrinter(string selectedPrinter)
        {
            try
            {
                if(selectedPrinter.Equals(string.Empty))
                {
                    CloseWindow();
                    return this.Direct();
                }
                else
                {
                    Imprimante mPrinter  = JSON.Deserialize<Imprimante>(selectedPrinter, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                    try
                    {
                        // get ip address
                        string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                        // initialize services
                        ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                        // save the selected printer
                        proxy.SaveTicketPrinter(mPrinter.NomImprimante);

                        // enumerate printers list
                        List<string> printersNameList = proxy.PrintersInstalled();                      

                    }
                    catch (Exception ex)
                    {

                    }
                }
                        
                CloseWindow();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        private static void CloseWindow()
        {
            X.GetCmp<Window>("frmImprimante").Hide();
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Unmask();
        }
    }
}
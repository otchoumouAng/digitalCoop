using Ext.Net;
using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Shared;

namespace Tms2017.MVC.Controllers
{
    public class ParametresController : Controller
    {
        // GET: Parametres

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult onEdit()
        {            

            Parametres mParam = new Parametres();
            mParam.fnGet();

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormParametres", Model = mParam, };
        }

        public ActionResult onEditParam()
        {

            Parametres mParam = new Parametres();
            mParam.fnGet();

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormParametres", Model = mParam, };
        }

        public ActionResult SubmitFormMethod()
        {
            try
            {
                Parametres mClass = new Parametres();
                mClass.IsNew = false;
                
                mClass = MapFormToObject(mClass);                

                bool result = mClass.fnUpdate();

                if (result)
                {
                    X.Toast("Success ! Parameters Updated!", "Parameters : Modifier", ToastAlign.Bottom);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Parameter : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private Parametres MapFormToObject(Parametres mClass)
        {
                        
            
            mClass.Campagne = X.GetCmp<ComboBox>("cmbDefaultCampagne").RawText.ToString();
            //string siteid = X.GetCmp<ComboBox>("cmbDefSite").SelectedItem.Value.ToString();
            //mClass.Site = Int16.Parse(siteid);
            mClass.Site = Int16.Parse(X.GetCmp<ComboBox>("cmbDefSite").SelectedItem.Value.ToString());

            mClass.Exportateur = new Exportateur();
            mClass.Exportateur.ID = int.Parse(X.GetCmp<ComboBox>("cmbDefaultExportateur").SelectedItem.Value.ToString());

            mClass.Recolte = new Recolte();
            mClass.Recolte.ID = int.Parse(X.GetCmp<ComboBox>("cmbDefaultCropQuality").RawValue.ToString());

            mClass.Laboratoire = new Laboratoire();
            mClass.Laboratoire.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefaultLaboratoire").RawValue.ToString());

            mClass.PrixNegocie = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtTolerancePrixNegocie").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtTolerancePrixNegocie").RawText) : 5;
            mClass.MaxCodeAnalyseGenere = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtMaxCodeGenerated").RawText) ? int.Parse(X.GetCmp<NumberField>("TxtMaxCodeGenerated").RawText) : 3;
            //mClass.NomSociete = X.GetCmp<TextField>("TxtNomSociete").Text;
            mClass.AdresseSociete = X.GetCmp<TextField>("TxtAdresseSociete").Text;
            mClass.TelSociete = X.GetCmp<TextField>("TxtTel1Societe").Text;
            mClass.Tel2Societe = X.GetCmp<TextField>("TxtTel2Societe").Text;            

            return mClass;

        }

        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

    }
}
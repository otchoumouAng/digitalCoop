using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business.Sites;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Settings;

namespace Tms2017.MVC.Controllers
{
    public class PaiementChequeAgenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";


        // GET: PaiementChequeAgence
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadListPayment(string ItemExecMode, string ItemPrefinancementID)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemPrefinancementID))
            {

                if (ItemExecMode == "AddNew")
                {


                }
                else
                {
                    myList = new PaiementChequeAgence().fnSelect(Guid.Parse(ItemPrefinancementID), -1);
                }

                PaiementChequeAgence mClass = new PaiementChequeAgence();
                if (myList.Count > 0)
                    mClass = myList[0] as PaiementChequeAgence;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListPaiementChequeAgence");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }


        public ActionResult OnAdd()
        {
            PaiementChequeAgenceViewModel mclass = new PaiementChequeAgenceViewModel();
            try
            {

                mclass._PaiementChequeAgence = new PaiementChequeAgence();
                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "PaiementChequeAgence_Detail", Model = mclass };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            PaiementChequeAgence mClass = JSON.Deserialize<PaiementChequeAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            PaiementChequeAgenceViewModel mViewModel = new PaiementChequeAgenceViewModel();


            try
            {
                //ici

                if (mClass.ID == Guid.Empty)
                    return HttpNotFound();

                mViewModel._PaiementChequeAgence = mClass;
                mViewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                return new Ext.Net.MVC.PartialViewResult { ViewName = "PaiementChequeAgence_Detail", Model = mViewModel };

            }
            catch (Exception ex)
            {
                return JavaScript(X.MessageBox.Alert("Error", ex.Message).ToScript());
            }


        }

        public ActionResult OnRemove(string ItemSelected, string ItemCount)
        {
            try
            {
                PaiementChequeAgence mClass = JSON.Deserialize<PaiementChequeAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                Store mstore = X.GetCmp<Store>("storeListPaiementChequeAgence");
                if (mClass.IsNew)
                {                                    
                    ModelProxy mProxy = mstore.GetById(mClass.ID);
                    mProxy.Drop();
                    return this.Direct();
                }
                else
                {
                    mClass.fnGet(mClass.ID);
                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("Remove : Retirer Payment failed.");

                    mClass.UtilisateurModification = (string)Session["userName"];
                    bool result = mClass.fnRemove();

                    if (result)
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);
                        mProxy.Drop();
                    }

                    return this.Direct();                   
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payment Cheque : Retirer payment cheque",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitOnAdd(string ItemExecMode, string ItemPaiementChequeID, string ItemDate, string ItemMontant, string ItemCommentaire)
        {
            try
            {

                PaiementChequeAgence mPaie = new PaiementChequeAgence();
                string exec = ItemExecMode;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(ItemExecMode);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mPaie.IsNew = true;
                    DateTime date = DateTime.Parse(ItemDate.ToString());

                    mPaie.Date = date;                    

                    mPaie.Montant = decimal.Parse(ItemMontant.Replace(" ", ""));
                    mPaie.Commentaire = ItemCommentaire;
                    X.GetCmp<Window>("PaiementChequeAgence_Detail").Close();
                    Store mstore = X.GetCmp<Store>("storeListPaiementChequeAgence");
                    //Update saving           
                    mPaie.ID = Guid.NewGuid();

                    mstore.Add(mPaie);

                }
                else
                {

                    mPaie.ID = Guid.Parse(ItemPaiementChequeID);
                    mPaie.IsNew = false;

                    if (mPaie == null || mPaie.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Payment cheque load failed.");

                    /*****Mapping*******/

                    DateTime date = DateTime.Parse(ItemDate.ToString());

                    mPaie.Date = date;
                    
                    mPaie.Montant = decimal.Parse(ItemMontant.Replace(" ", ""));
                    mPaie.Commentaire = ItemCommentaire;

                    X.GetCmp<Window>("PaiementChequeAgence_Detail").Close();
                    Store mstore = X.GetCmp<Store>("storeListPaiementChequeAgence");

                    /***Update saving****/
                    ModelProxy mProxy = mstore.GetById(mPaie.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mPaie);

                    mProxy.Commit();

                    mProxy.EndEdit();


                }


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payment Cheque : SubmitOnAdd",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }


            return this.Direct();
        }



        #region "Private Methods"
        private EnumsDefinition.eExecMode GetFormExecMode(object hidAction)
        {
            try
            {
                if (hidAction != null)
                {
                    if (hidAction.ToString().Equals(ADD_NEW))
                        return EnumsDefinition.eExecMode.AddNew;
                    else if (hidAction.ToString().Equals(APPROVE))
                        return EnumsDefinition.eExecMode.Approve;
                    else if (hidAction.ToString().Equals(CONSULT))
                        return EnumsDefinition.eExecMode.Consult;
                    else if (hidAction.ToString().Equals(DEFAULT))
                        return EnumsDefinition.eExecMode.Default;
                    else if (hidAction.ToString().Equals(UPDATE))
                        return EnumsDefinition.eExecMode.Update;
                }
                return EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("SubmitOnAdd : GetFormExecMode", ex.Message).Show();
                return EnumsDefinition.eExecMode.Default;
            }
        }

        private void DeselectGridRows()
        {
            X.GetCmp<RowSelectionModel>("rowSelectionPaiementChequeAgence").DeselectAll();

        }


        #endregion
    }
}
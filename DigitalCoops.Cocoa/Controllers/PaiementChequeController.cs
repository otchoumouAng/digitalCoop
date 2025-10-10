using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business.Sales;
using Tms.Classes.Shared;
using Tms.Components.Settings;

namespace Tms2017.MVC.Controllers
{   
    public class PaiementChequeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";


        // GET: PaiementCheque
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OnAdd()
        {
            PaiementChequeViewModel mclass = new PaiementChequeViewModel();
            try
            {
              
                mclass._PaiementCheque = new PaiementCheque();
                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "PaiementCheque_Detail", Model = mclass};
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            PaiementCheque mClass = JSON.Deserialize<PaiementCheque>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            PaiementChequeViewModel mViewModel = new PaiementChequeViewModel();


            try
            {
                

                if (mClass.ID == Guid.Empty)
                    return HttpNotFound();

                mViewModel._PaiementCheque = mClass;
                mViewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                return new Ext.Net.MVC.PartialViewResult { ViewName = "PaiementCheque_Detail", Model = mViewModel};

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

                PaiementCheque mClass = JSON.Deserialize<PaiementCheque>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mClass.IsNew)
                {
                    Store mstore = X.GetCmp<Store>("storeListPaiementCheque");
                    GridPanel mStore = X.GetCmp<GridPanel>("grpDetailPaiementCheque");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.Drop();
                    int mCount = int.Parse(ItemCount);
                    DeselectGridRows();
                }
                else {
                    GridPanel mStore = X.GetCmp<GridPanel>("grpDetailPaiementCheque");

                    Store mstore = X.GetCmp<Store>("storeListPaiementCheque");
                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    if (mClass.Desactive)
                        mClass.Desactive = false;
                    else
                        mClass.Desactive = true;

                    mstore.Add(mClass);
                    mProxy.Commit();
                    DeselectGridRows();

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

        public ActionResult SubmitOnAdd(string ItemExecMode, string ItemPaiementChequeID, string ItemBanqueID, string ItemMontant, string ItemRefCheque)
        {
            try
            {

                PaiementCheque mPaie = new PaiementCheque();
                string exec = ItemExecMode;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(ItemExecMode);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mPaie.IsNew = true;
                    if(!string.IsNullOrEmpty(ItemBanqueID) && ItemBanqueID != "null")
                    {
                        int banqueID = int.Parse(ItemBanqueID);
                        mPaie.Banque = new Banque(banqueID); 
                    }
                   
                    mPaie.Montant  = decimal.Parse(ItemMontant.Replace(" ",""));
                    mPaie.ReferenceCheque = ItemRefCheque;
                    X.GetCmp<Window>("PaiementCheque_Detail").Close();
                    Store mstore = X.GetCmp<Store>("storeListPaiementCheque");
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

                    if (!string.IsNullOrEmpty(ItemBanqueID) && ItemBanqueID != "null")
                    {
                        int banqueID = int.Parse(ItemBanqueID);
                        mPaie.Banque = new Banque(banqueID);
                    }

                    mPaie.Montant = decimal.Parse(ItemMontant.Replace(" ", ""));
                    mPaie.ReferenceCheque = ItemRefCheque;

                    X.GetCmp<Window>("PaiementCheque_Detail").Close();
                    Store mstore = X.GetCmp<Store>("storeListPaiementCheque");

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
            X.GetCmp<RowSelectionModel>("rowSelectionPaiementCheque").DeselectAll();

        }
    

    #endregion
    }
}
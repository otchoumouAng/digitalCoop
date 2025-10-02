using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class CompteBancaireController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: CompteBancaire
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{CC70621E-8388-405D-A9D5-7E1130397F60}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9E808D96-134A-4E87-B4C9-D7B9E30997B9}")))
                X.GetCmp<Button>("btnNewCompteBancaire").Enable();
            else
                X.GetCmp<Button>("btnNewCompteBancaire").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F663846F-C832-402D-9B85-F6EB1E6132EF}")))
            //    X.GetCmp<MenuItem>("mnuPrintLot").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintLot").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{10A0D076-47CF-4899-B270-6713DC647C9B}")))
                X.GetCmp<MenuItem>("mnuExportListCompteBancaire").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportListCompteBancaire").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5AF017C5-55C3-42E5-AC72-11E6DFC8C87A}")))
                X.GetCmp<Hidden>("cbhiddenPermModify").SetValue(true);
            else
                X.GetCmp<Hidden>("cbhiddenPermModify").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{DA090D69-AAE6-41C3-9FE0-3DC56DD3B1F3}")))
                X.GetCmp<Hidden>("cbhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("cbhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{AEDFACCD-C8C6-40A5-A112-9A5C3EC6E374}")))
                X.GetCmp<Hidden>("cbhiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("cbhiddenPermActiver").SetValue(false);

            #endregion

            return View();
        }

        public ActionResult Load(string ItemBanqueID, string ItemExportateurID)
        {
            int banque = int.Parse(ItemBanqueID);
            int exp = int.Parse(ItemExportateurID);
            List<DataPersist> mList = new CompteBancaire().fnSelect(banque,exp);
            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            CompteBancaireViewModel CompteBancaireVm = new CompteBancaireViewModel();

            CompteBancaireVm._CompteBancaire = new CompteBancaire();
            CompteBancaireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCompteBancaire", Model = CompteBancaireVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            CompteBancaireViewModel CompteBancaireVm = new CompteBancaireViewModel();

            CompteBancaireVm._CompteBancaire = JSON.Deserialize<CompteBancaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            CompteBancaireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCompteBancaire", Model = CompteBancaireVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            CompteBancaireViewModel CompteBancaireVm = new CompteBancaireViewModel();

            CompteBancaireVm._CompteBancaire = JSON.Deserialize<CompteBancaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            CompteBancaireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCompteBancaire", Model = CompteBancaireVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                CompteBancaire exportateur = new CompteBancaire();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    exportateur.IsNew = true;
                else
                {
                    exportateur.IsNew = false;

                    exportateur.fnGet(int.Parse(GetFormValue("TxtCompteBancaireID")));

                    if (exportateur == null || exportateur.ID == 0)
                        throw new Exception("SubmitFormMethod : Exportateur, load failed.");
                }

                exportateur = MapFormToObject(exportateur);

                bool result = exportateur.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCompteBancaire");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, exportateur);
                        X.GetCmp<RowSelectionModel>("rowSelectionCompteBancaire").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(exportateur.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(exportateur);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormCompteBancaire").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Exportateur : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus)
        {
            int status = -1;
            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }
            var liste = new CompteBancaire().fnSelect(status);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                CompteBancaire exportateur = JSON.Deserialize<CompteBancaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = exportateur.fnGet(exportateur.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Exportateur loading failed.");
                //(string)Session["userName"];
                exportateur.UtilisateurModification = (string)Session["userName"];

                if (exportateur.Desactive)
                    result = exportateur.fnActivate();
                else
                    result = exportateur.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Exportateur, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCompteBancaire");

                    ModelProxy mProxy = mstore.GetById(exportateur.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(exportateur);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Exportateur : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CompteBancaireCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeCompteBancaire");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("CompteBancaireCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private CompteBancaire MapFormToObject(CompteBancaire mCompte)
        {
            mCompte.Numero = X.GetCmp<TextField>("TxtNumeroCompteBancaire").Text;

            Exportateur exportateur = new Exportateur();
            exportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            exportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mCompte.Exportateur = exportateur;

            Banque mBanque = new Banque();
            mBanque.ID = int.Parse(GetFormValue("cmbBanque"));
            mBanque.Nom = X.GetCmp<ComboBox>("cmbBanque").SelectedItem.Text.ToString();
            mCompte.Banque = mBanque;

            mCompte.UtilisateurCreation = (string)Session["userName"];
            mCompte.UtilisateurModification = (string)Session["userName"];

            return mCompte;
        }

        #region Method
        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

        private Tms.Components.Settings.EnumsDefinition.eExecMode GetFormExecMode(object hidAction)
        {
            try
            {
                if (hidAction != null)
                {
                    if (hidAction.ToString().Equals(ADD_NEW))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
                    else if (hidAction.ToString().Equals(APPROVE))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
                    else if (hidAction.ToString().Equals(CONSULT))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
                    else if (hidAction.ToString().Equals(DEFAULT))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
                    else if (hidAction.ToString().Equals(UPDATE))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    else if (hidAction.ToString().Equals(EXTEND))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        private int GetCritriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }

        private int GetCritriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionCompteBancaire").DeselectAll();
        }


        #endregion

    }
}
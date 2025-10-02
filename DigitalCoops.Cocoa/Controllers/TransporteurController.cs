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
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class TransporteurController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Transporteur
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{C6E79B2E-FA1D-42F8-A11D-CD68B71F1A36}", UserName) == false)
                X.GetCmp<Button>("btnNewTransporteur").Disable();
            else
                X.GetCmp<Button>("btnNewTransporteur").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{FDA396C6-B566-4F94-9F7D-8624C3164454}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListTransporteur").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListTransporteur").Enable();

            X.GetCmp<Hidden>("TrshiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C6E79B2E-FA1D-42F8-A11D-CD68B71F1A36}", UserName));
            X.GetCmp<Hidden>("TrshiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C602CF92-FF4B-4444-9F3C-AD7CD2FBDB91}", UserName));
            X.GetCmp<Hidden>("TrshiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{6D35ECBC-FB19-4C11-A938-927CBF7E11AA}", UserName));
            X.GetCmp<Hidden>("TrshiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3E1BE2FF-1EBC-4179-98BC-6CE26205829B}", UserName));
            X.GetCmp<Hidden>("TrshiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FDA396C6-B566-4F94-9F7D-8624C3164454}", UserName));
            X.GetCmp<Hidden>("TrshiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D60FE9B8-3882-4690-9E87-82F799572C4A}", UserName));


            return View();
        }

        public ActionResult LoadTransporteur()
        {
            List<DataPersist> mList = new Transporteur().fnSelect(0);

            Transporteur mclass = new Transporteur();

            if (mList.Count > 0)
                mclass = mList[0] as Transporteur;

            return this.Store(mList);
        }

        public ActionResult LoadTransporteurWithPendingInvoice()
        {
            List<DataPersist> mList = new Transporteur().fnSelectWithPendingInvoice();

            Transporteur mclass = new Transporteur();

            if (mList.Count > 0)
                mclass = mList[0] as Transporteur;

            return this.Store(mList);
        }


        public ActionResult LoadTransporteurAll()
        {
            List<DataPersist> mList = new Transporteur().fnSelect();

            Transporteur mclass = new Transporteur();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Transporteur;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            TransporteurViewModel TransporteurVm = new TransporteurViewModel();

            TransporteurVm._Transporteur = new Transporteur();
            TransporteurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransporteur", Model = TransporteurVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            TransporteurViewModel TransporteurVm = new TransporteurViewModel();

            TransporteurVm._Transporteur = JSON.Deserialize<Transporteur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            TransporteurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransporteur", Model = TransporteurVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            TransporteurViewModel TransporteurVm = new TransporteurViewModel();

            TransporteurVm._Transporteur = JSON.Deserialize<Transporteur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            TransporteurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransporteur", Model = TransporteurVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Transporteur transporteur = new Transporteur();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    transporteur.IsNew = true;
                else
                {
                    transporteur.IsNew = false;

                    transporteur.fnGet(int.Parse(GetFormValue("TxtTransporteurID")));

                    if (transporteur == null || transporteur.ID == 0)
                        throw new Exception("SubmitFormMethod : Transporter, load failed.");
                }

                transporteur = MapFormToObject(transporteur);

                bool result = transporteur.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeTransporteur");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, transporteur);
                        X.GetCmp<RowSelectionModel>("rowSelectionTransporteur").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(transporteur.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(transporteur);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormTransporteur").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transporter : Data Validation",
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
            var liste = new Transporteur().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Transporteur transporteur = JSON.Deserialize<Transporteur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = transporteur.fnGet(transporteur.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Transporter, loading failed.");
                //(string)Session["userName"];
                transporteur.UtilisateurModification = (string)Session["userName"];

                if (transporteur.Desactive)
                    result = transporteur.fnActivate();
                else
                    result = transporteur.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Transporter, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeTransporteur");

                    ModelProxy mProxy = mstore.GetById(transporteur.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(transporteur);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transporter : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("TransporteurCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeTransporteur");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("TransporteurCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Transporteur MapFormToObject(Transporteur transporteur)
        {
            transporteur.Nom = X.GetCmp<TextField>("TxtNomTransporteur").Text;
            transporteur.Adresse = X.GetCmp<TextField>("TxtAdresseTransporteur").Text;
            transporteur.TelephoneFixe = X.GetCmp<TextField>("TxtTelephoneFixeTransporteur").Text;
            transporteur.TelephoneMobile = X.GetCmp<TextField>("TxtTelephoneMobileTransporteur").Text;
            transporteur.Fax = X.GetCmp<TextField>("TxtFaxTransporteur").Text;
            transporteur.Email = X.GetCmp<TextField>("TxtEmailTransporteur").Text;
            transporteur.Taux = decimal.Parse(X.GetCmp<NumberField>("TxtTauxTransporteur").RawValue.ToString());
            transporteur.UtilisateurCreation = (string)Session["userName"];
            transporteur.UtilisateurModification = (string)Session["userName"];

            return transporteur;
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
            X.GetCmp<RowSelectionModel>("rowSelectionTransporteur").DeselectAll();
        }


        #endregion

    }
}
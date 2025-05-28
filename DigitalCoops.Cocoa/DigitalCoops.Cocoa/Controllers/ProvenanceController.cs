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
using Tms.Classes.Shared.stock;
using Tms.Components.Data;
using Tms2017.MVC.Models;

namespace Tms2017.MVC.Controllers
{
    public class ProvenanceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Provenance
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{A82C8B12-CF2F-4A32-95FE-248CC41D950B}", UserName) == false)
                X.GetCmp<Button>("btnNewProvenance").Disable();
            else
                X.GetCmp<Button>("btnNewProvenance").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{CF8A9D41-3D57-45B5-97D8-98D71FE9948A}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListOrigin").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListOrigin").Enable();

            X.GetCmp<Hidden>("OrghiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A82C8B12-CF2F-4A32-95FE-248CC41D950B}", UserName));
            X.GetCmp<Hidden>("OrghiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4B4F8A53-B8DE-4568-A7F0-9DB31821CE0D}", UserName));
            X.GetCmp<Hidden>("OrghiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{ED704F2A-362F-4FB3-99D6-6D9E7DA63726}", UserName));
            X.GetCmp<Hidden>("OrghiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4F37C79B-9874-49D6-8A83-5605343463CF}", UserName));
            X.GetCmp<Hidden>("OrghiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{CF8A9D41-3D57-45B5-97D8-98D71FE9948A}", UserName));
            X.GetCmp<Hidden>("OrghiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DD8EB4C9-6FF3-46E2-9080-42D316DFD80B}", UserName));

            return View();
        }

        public ActionResult LoadProvenance()
        {
            List<DataPersist> mList = new Provenance().fnSelect();

            Provenance mclass = new Provenance();

            if(mList.Count > 0)
               mclass = mList[0] as Provenance;

            return this.Store(mList);
        }

        public ActionResult LoadProvenanceForProduction()
        {
            List<DataPersist> mList = new Provenance().fnSelect(0,2);            
            return this.Store(mList);
        }


        public ActionResult LoadProvenanceByLivraisonType(int? TypeLivraison)
        {
            LivraisonType mTypeLivraison = new LivraisonType();

            List<DataPersist> mList;
            mList = new List<DataPersist>();
            try
            {

                if(!TypeLivraison.HasValue)
                    return this.Store(mList);

                bool result = mTypeLivraison.fnGet(TypeLivraison);

                if (!result || (mTypeLivraison.ID == 0))
                    throw new Exception(this.GetType().FullName + " : LoadProvenanceByLivraisonType : failed load TypeLivraison.");

                // Si sens du type de livraison est sortie alors 
                // Provenance est site par defaut sinon 
                if (mTypeLivraison.Sens.Equals(LivraisonType.SORTIE))
                {
                    // sélectionner le site par defaut comme Provenance
                    Provenance mClass = new Provenance();

                    result = mClass.fnGetDefaultSite();

                    if (!result || (mClass.ID == 0))
                        throw new Exception(this.GetType().FullName + " : LoadProvenanceByLivraisonType : failed load default site.");

                    mList.Add(mClass);                   
                }
                else
                {
                    mList = new Provenance().fnSelect(0, mTypeLivraison.ProvenanceType.ID);
                }


                Provenance mclass = new Provenance();

                if (mList.Count > 0)
                    mclass = mList[0] as Provenance;
            }
            catch (Exception ex )
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Store(mList);
        }


        public ActionResult LoadProvenanceAll()
        {
            List<DataPersist> mList = new Provenance().fnSelect();

            Provenance mclass = new Provenance();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Provenance;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProvenanceViewModel ProvenanceVm = new ProvenanceViewModel();

            ProvenanceVm._Provenance = new Provenance();
            ProvenanceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProvenance", Model = ProvenanceVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProvenanceViewModel ProvenanceVm = new ProvenanceViewModel();

            ProvenanceVm._Provenance = JSON.Deserialize<Provenance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProvenanceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProvenance", Model = ProvenanceVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProvenanceViewModel ProvenanceVm = new ProvenanceViewModel();

            ProvenanceVm._Provenance = JSON.Deserialize<Provenance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProvenanceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProvenance", Model = ProvenanceVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Provenance provenance = new Provenance();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    provenance.IsNew = true;
                else
                {
                    provenance.IsNew = false;
                    
                    provenance.fnGet(int.Parse(GetFormValue("TxtProvenanceID")));

                    if (provenance == null || provenance.ID == 0)
                        throw new Exception("SubmitFormMethod : Origin load failed.");
                }

                provenance = MapFormToObject(provenance);

                bool result = provenance.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProvenance");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, provenance);
                        X.GetCmp<RowSelectionModel>("rowSelectionProvenance").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(provenance.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(provenance);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProvenance").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Origin : Data Validation",
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
            var liste = new Provenance().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Provenance provenance = JSON.Deserialize<Provenance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = provenance.fnGet(provenance.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Origin loading failed.");

                provenance.UtilisateurModification = (string)Session["userName"];

                if (provenance.Desactive)
                    result = provenance.fnActivate();
                else
                    result = provenance.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Origin, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProvenance");

                    ModelProxy mProxy = mstore.GetById(provenance.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(provenance);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Origin : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ProvenanceCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProvenance");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProvenanceCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Provenance MapFormToObject(Provenance provenance)
        {
            provenance.Nom = X.GetCmp<TextField>("TxtDesignationProvenance").Text;
            provenance.ProvenanceType = new ProvenanceType();
            provenance.ProvenanceType.ID = int.Parse(X.GetCmp<ComboBox>("CmbProvenanceType").SelectedItem.Value);
            provenance.ProvenanceType.Designation = X.GetCmp<ComboBox>("CmbProvenanceType").SelectedItem.Text;

            provenance.PoleProvenance = new PoleProvenance();
            provenance.PoleProvenance.ID = int.Parse(X.GetCmp<ComboBox>("CmbPoleProvenance").SelectedItem.Value);
            provenance.PoleProvenance.Designation = X.GetCmp<ComboBox>("CmbPoleProvenance").SelectedItem.Text;

            provenance.UtilisateurCreation = (string)Session["userName"];
            provenance.UtilisateurModification = (string)Session["userName"];

            return provenance;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProvenance").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion


    }
}
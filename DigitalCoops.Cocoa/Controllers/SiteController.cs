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
    public class SiteController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Site
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{913E99BB-12C5-46A8-9072-10C94BCB984A}", UserName) == false)
                X.GetCmp<Button>("btnNewSite").Disable();
            else
                X.GetCmp<Button>("btnNewSite").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{0550EEAE-D8D8-4D2A-975F-43298A0CF8E9}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListSite").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListSite").Enable();

            X.GetCmp<Hidden>("SitehiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{913E99BB-12C5-46A8-9072-10C94BCB984A}", UserName));
            X.GetCmp<Hidden>("SitehiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{38914A46-14F7-465F-B09D-7DAB61EE8F69}", UserName));
            X.GetCmp<Hidden>("SitehiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A4F2936E-7FBC-4699-BB13-31081636D05D}", UserName));
            X.GetCmp<Hidden>("SitehiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{48AFC910-D060-40AE-BC10-CD452FF602F4}", UserName));
            X.GetCmp<Hidden>("SitehiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0550EEAE-D8D8-4D2A-975F-43298A0CF8E9}", UserName));
            X.GetCmp<Hidden>("SitehiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B051E1A8-CA20-44DB-AE4C-B0683766B1A3}", UserName));

            return View();
        }

        public ActionResult LoadSite()
        {
            List<DataPersist> mList = new Site().fnSelect(0);

            Site mclass = new Site();

            if(mList.Count > 0)
            mclass = mList[0] as Site;

            return this.Store(mList);
        }


        public ActionResult LoadSiteAll()
        {
            List<DataPersist> mList = new Site().fnSelect(0);

            Site mclass = new Site();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Site;

            return this.Store(mList);
        }

        public ActionResult LoadAllSiteByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            Site mclass = new Site();

            if (HasAllAccess)
            {
                mList = new Site().fnSelect(0);
                
                mclass.ID = -1;
                mclass.Nom = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as Site;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();
                
                mList.Insert(0, mclass);
                mclass = mList[0] as Site;
            }
            //= new Site().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSiteByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            Site mclass = new Site();

            if (HasAllAccess)
            {
                mList = new Site().fnSelect(0);                
                mclass = mList[0] as Site;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as Site;
            }
            //= new Site().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificSite()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverSite = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            Site mclass = new Site();

            bool result = mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as Site;

            if (CanConsultOverSite)
            {
                mclass = new Site();
                result = mclass.fnGetDefaultSite();
                mList.Insert(1, mclass);

                mclass = new Site();
                mclass.ID = -1;
                mclass.Nom = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as Site;
            }
            
            //= new Site().fnSelect(0);

            return this.Store(mList);

        }


        public ActionResult SelectToPrice(string ItemPrice)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemPrice) && (Guid.Parse(ItemPrice) != Guid.Empty))
            {
                id = Guid.Parse(ItemPrice);
                var listeSite = new Site().fnSelectToPrice(id);
                return this.Store(listeSite);
            }
            else
            {
                var listAllSite = new Site().fnSelect(0);
                return this.Store(listAllSite);
            }
        }

        public ActionResult SelectToPriceGood()
        {
            string UserName = (string)Session["userName"];
            var listeSite = new Site().fnSelectAvailableForPrice(UserName);
                return this.Store(listeSite);
           
        }
        //public ActionResult SelectToPrice(string ItemSite)
        //{
        //    if (!string.IsNullOrEmpty(ItemSite))
        //    {
        //        int id = Int32.Parse(ItemSite);
        //        var listeSite = new Site().fnSelectToPrice(id);
        //        return this.Store(listeSite);
        //    }
        //    else
        //    {
        //        var listAllSite = new Site().fnSelect(0);
        //        return this.Store(listAllSite);
        //    }
        //}

        public ActionResult OnAdd()
        {
            

            SiteViewModel SiteVm = new SiteViewModel();

            SiteVm._Site = new Site();
            SiteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSite", Model = SiteVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            

            SiteViewModel SiteVm = new SiteViewModel();

            SiteVm._Site = JSON.Deserialize<Site>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            SiteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSite", Model = SiteVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            

            SiteViewModel SiteVm = new SiteViewModel();

            SiteVm._Site = JSON.Deserialize<Site>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            SiteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSite", Model = SiteVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Site site = new Site();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    site.IsNew = true;
                else
                {
                    site.IsNew = false;

                    site.fnGet(int.Parse(GetFormValue("TxtSiteID")));

                    if (site == null || site.ID == 0)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                site = MapFormToObject(site);

                bool result = site.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeSite");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, site);
                        X.GetCmp<RowSelectionModel>("rowSelectionSite").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(site.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(site);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormSite").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Data Validation",
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
            var liste = new Site().fnSelect(status);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Site site = JSON.Deserialize<Site>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = site.fnGet(site.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                site.UtilisateurModification = (string)Session["userName"];

                if (site.Desactive)
                    result = site.fnActivate();
                else
                    result = site.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeSite");

                    ModelProxy mProxy = mstore.GetById(site.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(site);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("SiteCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeSite");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("SiteCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Site MapFormToObject(Site site)
        {
            site.PrefixeSite = int.Parse(X.GetCmp<TextField>("TxtPrefixe").Text);
            site.Nom = X.GetCmp<TextField>("TxtNomSite").Text;

            site.Provenance = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautProvenance").Text))
            {
                site.Provenance = new Provenance();
                site.Provenance.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Value);
                site.Provenance.Nom = X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Text;
            }

            site.Destination = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautDestination").Text))
            {
                site.Destination = new Destination();
                site.Destination.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Value);
                site.Destination.Nom = X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Text;
            }
            //(string)Session["userName"]
            site.UtilisateurCreation = (string)Session["userName"];
            site.UtilisateurModification = (string)Session["userName"];

            return site;
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
            X.GetCmp<RowSelectionModel>("rowSelectionSite").DeselectAll();
        }


        #endregion

    }
}
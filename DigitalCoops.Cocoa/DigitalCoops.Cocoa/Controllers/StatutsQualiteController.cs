using DevExpress.XtraReports.UI;
using DigitalCoops.Cocoa.Models;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;
using Tms2017.MVC.Controllers;

namespace Cooperative.Controllers
{
    public class StatutsQualiteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: StatutsQualite
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{34caff7a-0f0d-40ee-92b3-9de7f7d50b39}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dd88803b-5f9c-41f2-881c-91ecedc61708}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();



            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dd88803b-5f9c-41f2-881c-91ecedc61708}")))
                X.GetCmp<Hidden>("arhiddenPermModify").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermModify").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ace59756-d945-4d53-92b1-49e1ee69520c}")))
                X.GetCmp<Hidden>("arhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{30ee1bab-7d3c-4fcb-88a9-628db185f061}")))
                X.GetCmp<Hidden>("arhiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermApprove").SetValue(false);

            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("StatutsQualiteCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemAnnee, string ItemSemaine, int ItemProduitID, int ItemProduitTypeID, string ItemStatut)

        {
            // Get filter criteria values
            string Annee = string.IsNullOrEmpty(ItemAnnee) ? "-1" : ItemAnnee;
            string Semaine = string.IsNullOrEmpty(ItemSemaine) ? "-1" : ItemSemaine;
            int produitId = ItemProduitID;
            int produitTypeId = ItemProduitTypeID;
            string statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;

            // Get filtered list using all criteria
            var mListe = (new StatutsQualite()).fnSelect(Annee, Semaine, produitId, produitTypeId, statut);


            return this.Store(mListe);
        }


        public ActionResult OnRefresh(string ItemAnnee, string ItemSemaine, int ItemProduitID, int ItemProduitTypeID, string ItemStatut)
        {
            try
            {


                Store mstore = X.GetCmp<Store>("storeListeStatutsQualite");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemAnnee",ItemAnnee),
                                    new Ext.Net.Parameter("ItemSemaine",ItemSemaine),
                                    new Ext.Net.Parameter("ItemProduitID",ItemProduitID),
                                    new Ext.Net.Parameter("ItemProduitTypeID",ItemProduitTypeID),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                X.GetCmp<FormPanel>("StatutsQualiteCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - DemandeEtiquette : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new StatutsQualite().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new StatutsQualite().fnSelect();
            return this.Store(mList);
        }

      
        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new StatutsQualite().fnSelect();
            StatutsQualite mclass = new StatutsQualite();

            mclass.ID = System.Guid.NewGuid();
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PermissionPalette();

            StatutsQualiteViewModel StatutsQualiteVm = new StatutsQualiteViewModel();

            StatutsQualiteVm._StatutsQualite = new StatutsQualite();
            StatutsQualiteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStatutsQualite", Model = StatutsQualiteVm, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            StatutsQualiteViewModel StatutsQualiteVm = new StatutsQualiteViewModel();

            StatutsQualiteVm._StatutsQualite = JSON.Deserialize<StatutsQualite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            StatutsQualiteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStatutsQualite", Model = StatutsQualiteVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            StatutsQualiteViewModel StatutsQualiteVm = new StatutsQualiteViewModel();

            StatutsQualiteVm._StatutsQualite = JSON.Deserialize<StatutsQualite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            StatutsQualiteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStatutsQualite", Model = StatutsQualiteVm };

        } 
        
        public ActionResult OnListQAStatus(string selectedIds)

        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            StatutsQualiteViewModel StatutsQualiteVm = new StatutsQualiteViewModel();

            StatutsQualiteVm._StatutsQualite = new StatutsQualite();
            StatutsQualiteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            ViewData["selectedIds"] = selectedIds;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStatutsQualiteListQAStatus", Model = StatutsQualiteVm, ViewData = ViewData };
        }

       

        public ActionResult SubmitFormMethod()
        {

            try
            {
                StatutsQualite StatutsQualite = new StatutsQualite();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    StatutsQualite.IsNew = true;
                else
                {
                    StatutsQualite.IsNew = false;

                    StatutsQualite.fnGet(int.Parse(GetFormValue("TxtStatutsQualiteID")));

                    if (StatutsQualite == null)
                        throw new Exception("SubmitFormMethod : StatutsQualite load failed.");
                }

                StatutsQualite = MapFormToObject(StatutsQualite);

                bool result = StatutsQualite.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeStatutsQualite");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, StatutsQualite);
                        X.GetCmp<RowSelectionModel>("rowSelectionStatutsQualite").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(StatutsQualite.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(StatutsQualite);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormStatutsQualite").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "StatutsQualite : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnPrintList()
        {
            StatutsQualite mclass = new StatutsQualite();
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            //ViewData["Produit"] = mParam.Produit.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "StatutsQualite_Print", ViewData = ViewData };
        }

        //public ActionResult OnPrintStatutsQualiteList()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        //    try
        //    {
        //        XtraReport report = null;

        //        report = new rptStatutsQualitesList() as XtraReport;

        //        report.DataSource = DevExpressReportDs.SetDataSource(report);
        //        report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
        //        report.Parameters["paramCampagneText"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

        //        report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
        //        report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

        //        report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
        //        report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

        //        report.Parameters["paramStatutsQualiteType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbTypeStatutsQualite").SelectedItem.Value);
        //        report.Parameters["paramStatutsQualiteTypeText"].Value = X.GetCmp<ComboBox>("cmbTypeStatutsQualite").SelectedItem.Text;

        //        report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
        //        report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

        //        report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value);
        //        report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

        //        Session["report"] = report;


        //        return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/StatutsQualite_GestionStock/ViewList', this, 'List Of StatutsQualites',''),App.StatutsQualite_GestionStock_Print.doClose()", Guid.NewGuid(), BaseUrl));
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Livraison : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //        return this.Direct();
        //    }
        //}


        public ActionResult ViewList()
        {
            XtraReport report = null;
            report = Session["report"] as XtraReport;
            //report = new rptStatutsQualitesList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramStatutsQualiteType"].Value = int.Parse(Session["paramStatutsQualiteType"].ToString());
            //report.Parameters["paramStatutsQualiteTypeText"].Value = Session["paramStatutsQualiteTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


        private StatutsQualite MapFormToObject(StatutsQualite mClass)
        {
          


            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }


        private StatutsQualite MapFormToObjectQAStatus(StatutsQualite mClass)

        {
          
          QAStatus qAStatus = new QAStatus();
          qAStatus.ID = int.Parse(X.GetCmp<ComboBox>("cmbQAStatusID").SelectedItem.Value);
          mClass.QAStatus = qAStatus;

          mClass.PaletteIDAsString = X.GetCmp<TextField>("selectedIds").Text;


            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }




        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                StatutsQualite StatutsQualite = JSON.Deserialize<StatutsQualite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = StatutsQualite.fnGet(StatutsQualite.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : StatutsQualite loading failed.");
                //(string)Session["userName"];
                StatutsQualite.UtilisateurModification = (string)Session["userName"];

                if (StatutsQualite.Desactive)
                    result = StatutsQualite.fnActivate();
                else
                    result = StatutsQualite.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : StatutsQualite, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeStatutsQualite");

                    ModelProxy mProxy = mstore.GetById(StatutsQualite.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(StatutsQualite);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "StatutsQualite : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }



        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                StatutsQualite StatutsQualite = JSON.Deserialize<StatutsQualite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = StatutsQualite.fnGet(StatutsQualite.ID);

                if (!result)
                    throw new Exception("OnCancel : Movement Sheet loading failed.");

                StatutsQualite.UtilisateurModification = (string)Session["userName"];

                if (StatutsQualite.Desactive)
                    result = StatutsQualite.fnActivate();
                else
                    result = StatutsQualite.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Production - StatutsQualite, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeStatutsQualite");

                    ModelProxy mProxy = mstore.GetById(StatutsQualite.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(StatutsQualite);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - StatutsQualite : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        [HttpPost]
        public ActionResult UpdateFormMethod()
        {
            try
            {
                StatutsQualite mClass = new StatutsQualite();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtStatutsQualiteId")));

                    if (mClass == null)
                        throw new Exception("UpdateFormMethod : Production - StatutsQualite - load failed.");

                }

                mClass = MapFormToObject(mClass);
                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeStatutsQualite");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowStatutsQualite").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormStatutsQualite").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - StatutsQualite : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }



         [HttpPost]
        public ActionResult UpdatePickedPaletteForm(string ItemSelected, string ItemQAStatusID)

        {
            try
            {
                StatutsQualite mClass = new StatutsQualite();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

               
                mClass = MapFormToObjectQAStatus(mClass);
                List<Guid> ListID = JSON.Deserialize<List<Guid>>(mClass.PaletteIDAsString);
                

                foreach (var item in ListID)
                {
                    mClass.PaletteID = item;
                    result = mClass.fnUpdatePickedPalette();
                }

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storePaletteListe_QA");
                    mStore.Reload();
                    
                    X.GetCmp<Window>("FormStatutsQualiteListQAStatus").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Statuts Qualite List QAStatus : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

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
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormStatutsQualite").Close();
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }


        private int GetCriteriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }



private void PermissionPalette()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool PAhiddenPermModify = HasAccess.fnGetUserAccessStatus("{3C067A90-AF15-4170-A253-2BB39575591E}", UserName);

            ViewData["PAhiddenPermModify"] = PAhiddenPermModify;
        }




    }
}
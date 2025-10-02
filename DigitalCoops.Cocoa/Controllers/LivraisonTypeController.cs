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
    public class LivraisonTypeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FournisseurType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{92CCDEA7-323B-4DB9-9100-1D94863D1B89}", UserName) == false)
                X.GetCmp<Button>("btnNewLivraisonType").Disable();
            else
                X.GetCmp<Button>("btnNewLivraisonType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{B5F93D80-7FC0-4BF8-A47E-65035CF5EBCD}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportLivraisonType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportLivraisonType").Enable();

            X.GetCmp<Hidden>("LthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{92CCDEA7-323B-4DB9-9100-1D94863D1B89}", UserName));
            X.GetCmp<Hidden>("LthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{986BAF2D-7AB7-4310-809A-C75F523F1459}", UserName));
            X.GetCmp<Hidden>("LthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{83A6041C-7F81-4980-97C4-B50208B5ECFD}", UserName));
            X.GetCmp<Hidden>("LthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{439DBBDC-762F-4B2B-A6F2-2CB8FBB2CC85}", UserName));
            X.GetCmp<Hidden>("LthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B5F93D80-7FC0-4BF8-A47E-65035CF5EBCD}", UserName));
            X.GetCmp<Hidden>("LthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7D7F70FC-4D0B-43F7-830F-C3A70516792B}", UserName));

            return View();
        }

        public ActionResult LoadLivraisonType()
        {
            List<DataPersist> myListe = new LivraisonType().fnSelect(0);
            LivraisonType mclass = new LivraisonType();

            //mclass.ID = -1;
            //mclass.Designation = "{Tous}";

           //if(myListe.Count > 0)
           //    mclass = myListe[0] as LivraisonType;

            return this.Store(myListe);

        }

        public ActionResult LoadLivraisonTypeAll()
        {
            List<DataPersist> myListe = new LivraisonType().fnSelect(0);
            LivraisonType mclass = new LivraisonType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as LivraisonType;

            return this.Store(myListe);

        }

        public ActionResult LoadLivraisonTypeSite()
        {
            List<DataPersist> myListe = new LivraisonType().fnSelect(0,1);
            LivraisonType mclass = new LivraisonType();

            return this.Store(myListe);
        }

        public ActionResult LoadLivraisonTypeSiteAll()
        {
            List<DataPersist> myListe = new LivraisonType().fnSelect(0, 1);
            LivraisonType mclass = new LivraisonType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as LivraisonType;

            return this.Store(myListe);
        }

        public ActionResult OnAdd()
        {
           

            LivraisonTypeViewModel LivraisonTypeVm = new LivraisonTypeViewModel();

            LivraisonTypeVm._LivraisonType = new LivraisonType();
            LivraisonTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLivraisonType", Model = LivraisonTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
           

            LivraisonTypeViewModel LivraisonTypeVm = new LivraisonTypeViewModel();

            LivraisonTypeVm._LivraisonType = JSON.Deserialize<LivraisonType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LivraisonTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLivraisonType", Model = LivraisonTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
           

            LivraisonTypeViewModel LivraisonTypeVm = new LivraisonTypeViewModel();

            LivraisonTypeVm._LivraisonType = JSON.Deserialize<LivraisonType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LivraisonTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLivraisonType", Model = LivraisonTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                LivraisonType livraisontype = new LivraisonType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    livraisontype.IsNew = true;
                else
                {
                    livraisontype.IsNew = false;

                    livraisontype.fnGet(int.Parse(GetFormValue("TxtLivraisonTypeID")));

                    if (livraisontype == null || livraisontype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type De Livraison load failed.");
                }

                livraisontype = MapFormToObject(livraisontype);

                bool result = livraisontype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLivraisonType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, livraisontype);
                        X.GetCmp<RowSelectionModel>("rowSelectionLivraisonType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(livraisontype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(livraisontype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormLivraisonType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type De Livraison : Data Validation",
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
            var liste = new LivraisonType().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                LivraisonType LivraisonType = JSON.Deserialize<LivraisonType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = LivraisonType.fnGet(LivraisonType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type De Livraison loading failed.");
                //(string)Session["userName"];
                LivraisonType.UtilisateurModification = (string)Session["userName"];

                if (LivraisonType.Desactive)
                    result = LivraisonType.fnActivate();
                else
                    result = LivraisonType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type De Livraison, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLivraisonType");

                    ModelProxy mProxy = mstore.GetById(LivraisonType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(LivraisonType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type De Livraison : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("LivraisonTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeLivraisonType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("LivraisonTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private LivraisonType MapFormToObject(LivraisonType livraisontype)
        {
            livraisontype.Designation = X.GetCmp<TextField>("TxtDesignationLivraisonType").Text;

            livraisontype.Produit = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbProduit").SelectedItem.Text))
            {
                livraisontype.Produit = new Produit();
                livraisontype.Produit.ID = int.Parse(X.GetCmp<ComboBox>("CmbProduit").SelectedItem.Value);
                livraisontype.Produit.Designation = X.GetCmp<ComboBox>("CmbProduit").SelectedItem.Text;
            }
            
            livraisontype.Sens = X.GetCmp<ComboBox>("CmbSensLivraisonType").SelectedItem.Value;

            livraisontype.FournisseurGroupe = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbFournisseurGroupe").SelectedItem.Text))
            {
                livraisontype.FournisseurGroupe = new FournisseurGroupe();
                livraisontype.FournisseurGroupe.ID = int.Parse(X.GetCmp<ComboBox>("CmbFournisseurGroupe").SelectedItem.Value);
                livraisontype.FournisseurGroupe.Designation = X.GetCmp<ComboBox>("CmbFournisseurGroupe").SelectedItem.Text;
            }

            livraisontype.SacType = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbSacType").SelectedItem.Value))
            {
                livraisontype.SacType = new SacType();
                livraisontype.SacType.ID = int.Parse(X.GetCmp<ComboBox>("CmbSacType").SelectedItem.Value);
                livraisontype.SacType.Designation = X.GetCmp<ComboBox>("CmbSacType").SelectedItem.Text;
            }

            livraisontype.ProvenanceType = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbProvenanceType").SelectedItem.Value))
            {
                livraisontype.ProvenanceType = new ProvenanceType();
                livraisontype.ProvenanceType.ID = int.Parse(X.GetCmp<ComboBox>("CmbProvenanceType").SelectedItem.Value);
                livraisontype.ProvenanceType.Designation = X.GetCmp<ComboBox>("CmbProvenanceType").SelectedItem.Text;
            }

            livraisontype.DestinationType = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDestinationType").SelectedItem.Value))
            {
                livraisontype.DestinationType = new DestinationType();
                livraisontype.DestinationType.ID = int.Parse(X.GetCmp<ComboBox>("CmbDestinationType").SelectedItem.Value);
                livraisontype.DestinationType.Designation = X.GetCmp<ComboBox>("CmbDestinationType").SelectedItem.Text;
            }            

            livraisontype.EstAchat = bool.Parse(X.GetCmp<Checkbox>("ChkEstAchat").Value.ToString());
            livraisontype.EstEmpotage = bool.Parse(X.GetCmp<Checkbox>("ChkEstEmpotage").Value.ToString());
            livraisontype.AutoriseCoupage = bool.Parse(X.GetCmp<Checkbox>("ChkAutoriseCoupage").Value.ToString());
            livraisontype.AutoriseAnalyse = bool.Parse(X.GetCmp<Checkbox>("ChkAutoriseAnalyse").Value.ToString());
            livraisontype.AutoriseRefaction = bool.Parse(X.GetCmp<Checkbox>("ChkAutoriseRefaction").Value.ToString());
            livraisontype.SaisieCertification = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieCertification").Value.ToString());
            livraisontype.SaisieNumLot = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieNumLot").Value.ToString());
            livraisontype.SaisieNumPlomb = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieNumPlomb").Value.ToString());
            livraisontype.SaisieNumConteneur = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieNumConteneur").Value.ToString());
            livraisontype.SaisieNumOT = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieNumOT").Value.ToString());
            livraisontype.SaisieNumBS = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieNumBS").Value.ToString());
            livraisontype.SaisieTransitaire = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieTransitaire").Value.ToString());
            livraisontype.SaisieNumExterne = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieNumExterne").Value.ToString());
            livraisontype.SaisieNumBL = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieNumBL").Value.ToString());
            //livraisontype.SaisieCentreAchat = bool.Parse(X.GetCmp<Checkbox>("ChkSaisieCentreAchat").Value.ToString());
            livraisontype.AfficheEcartPoids = bool.Parse(X.GetCmp<Checkbox>("ChkAfficheEcartPoids").Value.ToString());
            livraisontype.AfficheResultatAnalyse = bool.Parse(X.GetCmp<Checkbox>("ChkAfficheResultatAnalyse").Value.ToString());
            livraisontype.AffichePoidsMoyenSacs = bool.Parse(X.GetCmp<Checkbox>("ChkAffichePoidsMoyenSacs").Value.ToString());
            //livraisontype.GenereMvtSacherie = bool.Parse(X.GetCmp<Checkbox>("ChkGenereMvtSacherie").Value.ToString());
            livraisontype.CalculeVGM = bool.Parse(X.GetCmp<Checkbox>("ChkCalculeVGM").Value.ToString());

            livraisontype.VisibleEnAgence = bool.Parse(X.GetCmp<Checkbox>("ChkEstVisibleSurSite").Value.ToString());
            livraisontype.SaisieNumTransfert = bool.Parse(X.GetCmp<Checkbox>("ChkEstTransfert").Value.ToString());
            //(string)Session["userName"];
            livraisontype.UtilisateurCreation = (string)Session["userName"];
            livraisontype.UtilisateurModification = (string)Session["userName"];

            return livraisontype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionLivraisonType").DeselectAll();
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
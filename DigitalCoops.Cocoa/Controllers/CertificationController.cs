using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Net.MVC;
using Tms.Components.Data;
using Tms.Classes.Shared;
using Ext.Net;
using Newtonsoft.Json;
using Tms.Classes.Security;

namespace Tms2017.MVC.Controllers
{
    public class CertificationController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: Certification
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{37DC438C-BA0B-4261-A5AB-6F51B9945BE4}", UserName) == false)
                X.GetCmp<Button>("btnNewCertification").Disable();
            else
                X.GetCmp<Button>("btnNewCertification").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{484c34a2-e750-4247-aee3-78b4a6dd0a77}", UserName) == false)
                X.GetCmp<Button>("btnUpdateCertifContrat").Disable();
            else
                X.GetCmp<Button>("btnUpdateCertifContrat").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{ED0E954E-4FFC-4858-8974-A1C8A0D90C1A}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListCertification").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListCertification").Enable();

            X.GetCmp<Hidden>("CrthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{37DC438C-BA0B-4261-A5AB-6F51B9945BE4}", UserName));
            X.GetCmp<Hidden>("CrthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{63AD7885-365B-4604-942B-6340B37E6A98}", UserName));
            X.GetCmp<Hidden>("CrthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{09DC0161-1C6B-491D-9111-4CEB33214D26}", UserName));
            X.GetCmp<Hidden>("CrthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{EBC7B71F-0FD5-49B0-85D2-C651BB2ED78F}", UserName));
            X.GetCmp<Hidden>("CrthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{ED0E954E-4FFC-4858-8974-A1C8A0D90C1A}", UserName));
            X.GetCmp<Hidden>("CrthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5EFDE88F-DA73-4D5C-9306-5F89381219D0}", UserName));
            
            return View();
        }

        public ActionResult LoadCertification()
        {
            List<DataPersist> mList = new Certification().fnSelect(0);

            Certification mclass = new Certification();

            return this.Store(mList);
        }


        public ActionResult LoadCertificationAll()
        {
            List<DataPersist> mList = new Certification().fnSelect(0);

            Certification mclass = new Certification();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);

            mclass = new Certification();

            mclass.ID = 0;
            mclass.Designation = "No";

            mList.Insert(1, mclass);

            mclass = mList[0] as Certification;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            CertificationViewModel certificationVm = new CertificationViewModel();

            certificationVm._Certification = new Certification();
            certificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCertification", Model = certificationVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            CertificationViewModel certificationVm = new CertificationViewModel();

            certificationVm._Certification = JSON.Deserialize<Certification>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            certificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCertification", Model = certificationVm };

        }

        public ActionResult OnEditSaleBonus()
        {            
            Parametres mParam = new Parametres(0);
            CertificationViewModel certificationVm = new CertificationViewModel();
            
            certificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            certificationVm._BonusCertification = mParam.ContratVenteBonusCertificationValue;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCertificationBonus", Model = certificationVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            CertificationViewModel certificationVm = new CertificationViewModel();

            certificationVm._Certification = JSON.Deserialize<Certification>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            certificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCertification", Model = certificationVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Certification certification = new Certification();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    certification.IsNew = true;
                else
                {
                    certification.IsNew = false;

                    certification.fnGet(int.Parse(GetFormValue("TxtCertificationID")));

                    if (certification == null || certification.ID == 0)
                        throw new Exception("SubmitFormMethod : Certification load failed.");
                }

                certification = MapFormToObject(certification);

                bool result = certification.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCertification");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, certification);
                        X.GetCmp<RowSelectionModel>("rowSelectionCertification").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(certification.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(certification);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormCertification").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certification : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult UpdateCertification()
        {

            try
            {
                Certification certification = new Certification();

                //certification = MapFormToObject(certification);
                if (X.GetCmp<TextField>("txtBonus").Text != string.Empty) certification.Bonus = decimal.Parse(X.GetCmp<TextField>("txtBonus").Text);
                bool result = certification.fnUpdateBonusCertification();

                if (result)
                {                    
                    X.GetCmp<Window>("FormCertificationBonus").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certification : Data Validation",
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
            var listeCertification = new Certification().fnSelect(status);            

            return this.Store(listeCertification);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Certification certification = JSON.Deserialize<Certification>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = certification.fnGet(certification.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Certification loading failed.");

                certification.UtilisateurModification = (string)Session["userName"];                

                if (certification.Desactive)
                    result = certification.fnActivate();
                else
                    result = certification.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Certification, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCertification");

                    ModelProxy mProxy = mstore.GetById(certification.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(certification);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certification : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CertificationCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeCertification");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("CertificationCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Certification MapFormToObject(Certification certification)
        {
            certification.Designation = X.GetCmp<TextField>("TxtDesignationCertification").Text;

            certification.UtilisateurCreation = (string)Session["userName"];
            certification.UtilisateurModification = (string)Session["userName"];

            return certification;
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
            X.GetCmp<RowSelectionModel>("rowSelectionCertification").DeselectAll();
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
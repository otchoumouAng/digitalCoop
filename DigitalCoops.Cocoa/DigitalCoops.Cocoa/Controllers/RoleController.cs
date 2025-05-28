using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class RoleController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Role
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{335C416A-A4CB-4DCE-8678-91E5B640C2B9}", UserName);
            
            if (HasAccess.fnGetUserAccessStatus("{FB848D31-8674-4376-AE29-5E769556150E}", UserName) == false)
                X.GetCmp<Button>("btnNewRole").Disable();
            else
                X.GetCmp<Button>("btnNewRole").Enable();

            if (HasAccess.fnGetUserAccessStatus("{952387FB-9561-401B-A750-B3C12A42BF3D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListRole").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListRole").Enable();

            X.GetCmp<Hidden>("RohiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{FB848D31-8674-4376-AE29-5E769556150E}", UserName));
            X.GetCmp<Hidden>("RohiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{2DE35FCE-7CFF-4BA2-B8FC-2192740B91C9}", UserName));
            X.GetCmp<Hidden>("RohiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{8E4E6416-81AC-4A00-A40A-D66777DB7111}", UserName));
            X.GetCmp<Hidden>("RohiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{32C29486-6AAA-4FEE-B1A2-DE64E0F712E8}", UserName));
            X.GetCmp<Hidden>("RohiddenPermPrintRoleList").SetValue(HasAccess.fnGetUserAccessStatus("{38DDD9EE-F5BB-4C06-8556-47FE775FF501}", UserName));
            X.GetCmp<Hidden>("RohiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{952387FB-9561-401B-A750-B3C12A42BF3D}", UserName));
            X.GetCmp<Hidden>("RohiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{335C416A-A4CB-4DCE-8678-91E5B640C2B9}", UserName));

            
            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HavPermissionRemoveFunction = HasAccess.fnGetUserAccessStatus("{AE8B247D-28E8-476A-81B7-B49D9689DE10}", UserName);
            bool HavPermissionAddFunction = HasAccess.fnGetUserAccessStatus("{C14C57AA-1C54-4267-97ED-D93721B5C0D7}", UserName);

            ViewData["HavPermissionRemoveFunction"] = HavPermissionRemoveFunction;
            ViewData["HavPermissionAddFunction"] = HavPermissionAddFunction;

            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RoleViewModel roleVm = new RoleViewModel();

            roleVm._Role = new Role();
            roleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRole", Model = roleVm, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HavPermissionRemoveFunction = HasAccess.fnGetUserAccessStatus("{AE8B247D-28E8-476A-81B7-B49D9689DE10}", UserName);
            bool HavPermissionAddFunction = HasAccess.fnGetUserAccessStatus("{C14C57AA-1C54-4267-97ED-D93721B5C0D7}", UserName);

            ViewData["HavPermissionRemoveFunction"] = HavPermissionRemoveFunction;
            ViewData["HavPermissionAddFunction"] = HavPermissionAddFunction;

            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RoleViewModel roleVm = new RoleViewModel();

            roleVm._Role = JSON.Deserialize<Role>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            roleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRole", Model = roleVm, ViewData = ViewData };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RoleViewModel roleVm = new RoleViewModel();

            roleVm._Role = JSON.Deserialize<Role>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            roleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRole", Model = roleVm };

        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Role role = new Role();
                RoleFonction rolefonction;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    role.IsNew = true;
                else
                {
                    role.IsNew = false;

                    role.fnGet(Guid.Parse(GetFormValue("TxtRoleID")));

                    if (role == null || role.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Role load failed.");
                }

                bool result = false;
                bool resultrolefonction = true;
                role = MapFormToObject(role);

                _db = role.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = role.fnUpdate(mtran);

                if (result)
                {                    
                    List<Fonction> ItemFonction = JSON.Deserialize<List<Fonction>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemFonction.Count > 0)
                    {

                        for (int i = 0; i < ItemFonction.Count(); i++)
                        {
                            rolefonction = new RoleFonction();
                            rolefonction.Role = new Role();
                            rolefonction.Fonction = new Fonction();

                            rolefonction.SetDataSource(_db);

                            rolefonction.Role.ID = role.ID;
                            rolefonction.Fonction.ID = ItemFonction[i].ID;
                            rolefonction.IsNew = ItemFonction[i].IsNew;

                            //(string)Session["userName"];
                            rolefonction.UtilisateurCreation = (string)Session["userName"];
                            rolefonction.UtilisateurModification = (string)Session["userName"];
                            if (rolefonction.IsNew)
                            {
                                resultrolefonction = rolefonction.fnUpdate(mtran);
                            }



                            if (!resultrolefonction)
                            {
                                break;
                            }
                        }
                    }
                    if (!resultrolefonction)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);

                }


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRole");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, role);
                        X.GetCmp<RowSelectionModel>("rowSelectionRole").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(role.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(role);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormRole").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Role : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnCancel()
        {
            return View();
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
            var listeRole = new Role().fnSelect(status);

            // Paging
            //int start = parameters.Start;

            //int limit = parameters.Limit;

            //if ((start + limit) > listeRole.Count)
            //{
            //    limit = listeRole.Count - start;
            //}

            //List<DataPersist> rangePlants = (start < 0 || limit < 0) ? listeRole : listeRole.GetRange(start, limit);
            //return this.Store(new Paging<DataPersist>(rangePlants, listeRole.Count));
            return this.Store(listeRole);
            
        }

        public ActionResult SelectFonction(string ItemRole)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemRole))
            {
                id = Guid.Parse(ItemRole);
            }
            var listeRole = new RoleFonction().fnSelect(id);
            return this.Store(listeRole);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Role mClass = JSON.Deserialize<Role>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Role loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];                

                if (mClass.IsDisable)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : cancel role failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRole");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Role : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeRole");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("RoleCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("RoleCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult RemoveFunctionToRole(string ItemSelected)
        {
            try
            {

                List<RoleFonction> fonctions = JSON.Deserialize<List<RoleFonction>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                RoleFonction mClass;

                if (fonctions.Count() > 0)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRoleFonction");

                    foreach (var item in fonctions)
                    {
                        if (item.IsNew)
                        {
                            ModelProxy _proxy = mstore.GetById(item.ID);
                            _proxy.Drop();

                            return this.Direct();
                        }
                        else
                        {
                            mClass = new RoleFonction();
                            mClass.fnGet(item.ID);
                            if (mClass == null || mClass.ID == Guid.Empty)
                                throw new Exception("RemoveFunctionToRole : Retirer Function failed.");

                            mClass.UtilisateurModification = (string)Session["userName"];

                            bool result = mClass.fnRemove();

                            if (result)
                            {

                                ModelProxy mProxy = mstore.GetById(mClass.ID);

                                mProxy.Drop();

                                //mProxy.Set(mClass);

                                //mProxy.Commit();

                            }
                        }
                        
                    }
                }

                               

                
                //if (rolefonction.IsNew)
                //{
                //    ModelProxy _proxy = mstore.GetById(rolefonction.ID);
                //    _proxy.Drop();

                //    return this.Direct();
                //}

                //fonctions.fnGet(fonctions.ID);

                

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Role : Retirer function",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();            
        }

        private Role MapFormToObject(Role mClass)
        {
            mClass.Nom = X.GetCmp<TextField>("TxtNomRole").Text;
            mClass.Description = X.GetCmp<TextField>("TxtDescriptionRole").Text;
            //mClass.IsDisable = false;

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult AddFunctionToRole(string ItemRole)
        {
            //X.GetCmp<Viewport>("TmsViewPort").Mask();
            Guid roleID = !string.IsNullOrEmpty(ItemRole) ? Guid.Parse(ItemRole) : Guid.Empty;

            Role role = new Role();
            role.ID = roleID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListeRoleFonctions", Model = role };            
        }

        public ActionResult SelectToUser(string ItemUser)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemUser) && (Guid.Parse(ItemUser) != Guid.Empty))
            {
                id = Guid.Parse(ItemUser);
                var listeRole = new Role().fnSelectToUser(id);
                return this.Store(listeRole);
            }
            else
            {
                var listAllRole = new Role().fnSelect(0);
                return this.Store(listAllRole);
            }
        }

        public ActionResult SubmitListeFonctions(string ItemSelected)
        {
            List<Fonction> fonctions = JSON.Deserialize<List<Fonction>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            Store store = X.GetCmp<Store>("storeListeRoleFonction");

            foreach (var item in fonctions)
            {
                item.IsNew = true;                
                item.IsNewInList = true;
                store.Insert(0, item);
                X.GetCmp<RowSelectionModel>("rowSelectionRoleFonction").Select(0);
            }

            X.GetCmp<Window>("FormListeRoleFonctions").Close();
            return this.Direct();
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
            X.GetCmp<RowSelectionModel>("rowSelectionRole").DeselectAll();
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
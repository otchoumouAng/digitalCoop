using Ext.Net;
using Ext.Net.MVC;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class UtilisateurController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Utilisateur
        public ActionResult Index()
        {

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{09F21CF5-1280-49BB-8504-6A77271A33CD}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{FFAC877A-A03C-44A3-99DE-EFEA8CF38215}", UserName) == false)
                X.GetCmp<Button>("btnNewUser").Disable();
            else
                X.GetCmp<Button>("btnNewUser").Enable();

            if (HasAccess.fnGetUserAccessStatus("{23C86482-74B7-4FE4-A631-A3DBD4A59CE8}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListUser").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListUser").Enable();

            if (HasAccess.fnGetUserAccessStatus("{1C8B7245-9270-4B04-A754-2C207F257643}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintUserList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintUserList").Enable();

            X.GetCmp<Hidden>("UshiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{FFAC877A-A03C-44A3-99DE-EFEA8CF38215}", UserName));
            X.GetCmp<Hidden>("UshiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{85192DBC-5C2D-4AD1-857D-1452587B2C90}", UserName));
            X.GetCmp<Hidden>("UshiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{1C655695-11B9-495E-B7BF-99428132216D}", UserName));
            X.GetCmp<Hidden>("UshiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{CFE50447-B7CA-4FD0-8943-94C742409FA4}", UserName));
            X.GetCmp<Hidden>("UshiddenPermPrintUsersList").SetValue(HasAccess.fnGetUserAccessStatus("{1C8B7245-9270-4B04-A754-2C207F257643}", UserName));
            X.GetCmp<Hidden>("UshiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{23C86482-74B7-4FE4-A631-A3DBD4A59CE8}", UserName));
            X.GetCmp<Hidden>("UshiddenPermResetPassword").SetValue(HasAccess.fnGetUserAccessStatus("{14444F41-F74E-431A-8F21-E02DA43BF2D4}", UserName));
            //X.GetCmp<Hidden>("UshiddenPermSetRole").SetValue(HasAccess.fnGetUserAccessStatus("{F96F03BB-467F-4918-BF98-4D25837E961B}", UserName));
            X.GetCmp<Hidden>("UshiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{09F21CF5-1280-49BB-8504-6A77271A33CD}", UserName));

            #endregion


            return View();
        }

        public ActionResult OnAdd()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HavPermissionSetRole = HasAccess.fnGetUserAccessStatus("{F96F03BB-467F-4918-BF98-4D25837E961B}", UserName);
            bool HavPermissionRemoveRole = HasAccess.fnGetUserAccessStatus("{4543321C-E708-4782-BDB2-A4A0FC781D73}", UserName);

            ViewData["HavPermissionSetRole"] = HavPermissionSetRole;
            ViewData["HavPermissionRemoveRole"] = HavPermissionRemoveRole;            

            UtilisateurViewModel userVm = new UtilisateurViewModel();
            Parametres mParam = new Parametres(0);            
            userVm._Utilisateur = new Utilisateur();
            userVm._Utilisateur.BasedInLocation = new Site();
            userVm._Utilisateur.BasedInLocation.ID = mParam.Site;
            userVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName =  "FormUser", Model = userVm, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HavPermissionSetRole = HasAccess.fnGetUserAccessStatus("{F96F03BB-467F-4918-BF98-4D25837E961B}", UserName);
            bool HavPermissionRemoveRole = HasAccess.fnGetUserAccessStatus("{4543321C-E708-4782-BDB2-A4A0FC781D73}", UserName);

            ViewData["HavPermissionSetRole"] = HavPermissionSetRole;
            ViewData["HavPermissionRemoveRole"] = HavPermissionRemoveRole;            

            UtilisateurViewModel userVm = new UtilisateurViewModel();
            userVm._Utilisateur = JSON.Deserialize<Utilisateur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });            
            userVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormUser", Model = userVm, ViewData = ViewData };
        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            UtilisateurViewModel userVm = new UtilisateurViewModel();
            
            userVm._Utilisateur = JSON.Deserialize<Utilisateur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            userVm._Utilisateur.BasedInLocation = new Site();            

            userVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormUser", Model = userVm };
        }

        public ActionResult OnChangePassWord()
        {
            //X.GetCmp<Viewport>("TmsViewPort").Mask();

            Utilisateur user = new Utilisateur();

            string ItemUser = (string)Session["userName"];
            
            if (!string.IsNullOrEmpty(ItemUser)) user.fnGetByUserName(ItemUser);
            else return this.Direct();
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormUserCredentials", Model = user };
            //return View("FormUserCredentials", user);
            
        }


        public ActionResult OnResetPassWord(string ItemUser)
        {
            try
            {
                Utilisateur user = JSON.Deserialize<Utilisateur>(ItemUser, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = user.fnGet(user.IDUtilisateur);

                if (!result)
                    throw new Exception("Reset PassWord : User loading failed.");

                user.UtilisateurModification = (string)Session["userName"];
               
                result = user.fnReinitialiserPassword();                

                if (!result)
                    throw new Exception("Reset PassWord : Reset Password failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeUser");

                    ModelProxy mProxy = mstore.GetById(user.IDUtilisateur);

                    mProxy.BeginEdit();

                    mProxy.Set(user);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Reset Password",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult AddModuleToUser(string ItemUser)
        {
            Guid userID = !string.IsNullOrEmpty(ItemUser) ? Guid.Parse(ItemUser) : Guid.Empty;

            Utilisateur user = new Utilisateur();
            user.IDUtilisateur = userID;           

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListeModuleUser", Model = user };
        }

        public ActionResult OnOpenFormModuleUsers(string ItemUser)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            UtilisateurViewModel userVm = new UtilisateurViewModel();

            userVm._Utilisateur = JSON.Deserialize<Utilisateur>(ItemUser, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            userVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormModuleUser", Model = userVm };
        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Utilisateur user = new Utilisateur();
                RoleUser roleuser = new RoleUser();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    user.IsNew = true;
                else
                {
                    user.IsNew = false;

                    user.fnGet(Guid.Parse(GetFormValue("TxtUserID")));

                    if (user == null || user.IDUtilisateur == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Role load failed.");
                }

                bool result = false;
                bool resultroleuser = true;

                user = MapFormToObject(user);

                _db = user.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = user.fnUpdate(mtran);

                if (result)
                {
                    List<Role> ItemRole = JSON.Deserialize<List<Role>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemRole.Count > 0)
                    {
                        for (int i = 0; i < ItemRole.Count(); i++)
                        {
                            roleuser = new RoleUser();
                            roleuser.Role = new Role();
                            roleuser.Utilisateur = new Utilisateur();

                            roleuser.SetDataSource(_db);

                            roleuser.Utilisateur.IDUtilisateur = user.IDUtilisateur;
                            roleuser.Role.ID = ItemRole[i].ID;
                            roleuser.IsNew = ItemRole[i].IsNew;

                            //(string)Session["userName"];
                            roleuser.UtilisateurCreation = (string)Session["userName"];
                            roleuser.UtilisateurModification = (string)Session["userName"];
                            if (roleuser.IsNew)
                            {
                                resultroleuser = roleuser.fnUpdate(mtran);
                            }

                            if (!resultroleuser)
                            {
                                break;
                            }
                        }
                    }
                    if (!resultroleuser)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);

                }


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeUser");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, user);
                        //X.GetCmp<RowSelectionModel>("rowSelectionUser").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(user.IDUtilisateur);

                        mProxy.BeginEdit();

                        mProxy.Set(user);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormUser").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitModuleUser(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {                
                ModuleUsers itemuser = new ModuleUsers();

                Guid userid = Guid.Parse(GetFormValue("Txtuserid"));
                if (userid == null || userid == Guid.Empty)
                    throw new Exception("SubmitFormMethod : User load failed.");
                
                bool resulItemuser = true;                

                _db = itemuser.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);                
                
                List<Module> Item = JSON.Deserialize<List<Module>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (Item.Count > 0)
                {
                    for (int i = 0; i < Item.Count(); i++)
                    {
                        itemuser = new ModuleUsers();
                        itemuser.Module = new Module();
                        itemuser.Utilisateur = new Utilisateur();

                        itemuser.SetDataSource(_db);

                        itemuser.Utilisateur.IDUtilisateur = userid;
                        itemuser.Module.ID = Item[i].ID;
                        itemuser.IsNew = Item[i].IsNew;

                        //(string)Session["userName"];
                        itemuser.UtilisateurCreation = (string)Session["userName"];
                        itemuser.UtilisateurModification = (string)Session["userName"];
                        if (itemuser.IsNew)
                        {
                            resulItemuser = itemuser.fnUpdate(mtran);
                        }

                        if (!resulItemuser)
                        {
                            break;
                        }
                    }
                }
                if (!resulItemuser)
                {
                    _db.RollBackTransaction(mtran);
                }
                _db.CommitTransaction(mtran);


                X.GetCmp<Window>("FormModuleUser").Close();
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Unmask();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult AddRoleToUser(string ItemUser)
        {            
            Guid userID = !string.IsNullOrEmpty(ItemUser) ? Guid.Parse(ItemUser) : Guid.Empty;

            Utilisateur user = new Utilisateur();
            user.IDUtilisateur = userID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListeUserRoles", Model = user };
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Utilisateur user = JSON.Deserialize<Utilisateur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = user.fnGet(user.IDUtilisateur);

                if (!result)
                    throw new Exception("OnActivateDeactivate : User loading failed.");

                user.UtilisateurModification = (string)Session["userName"];                

                if (user.IsDisabled)
                    result = user.fnActivate();
                else
                    result = user.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : User cancel failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeUser");

                    ModelProxy mProxy = mstore.GetById(user.IDUtilisateur);

                    mProxy.BeginEdit();

                    mProxy.Set(user);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("UserCriteriaPanel");
            mform.ToggleCollapse();
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
            var listeUser = new Utilisateur().fnSelect(-1,status);

            // Paging
            //int start = parameters.Start;

            //int limit = parameters.Limit;

            //if ((start + limit) > listeUser.Count)
            //{
            //    limit = listeUser.Count - start;
            //}

            //List<DataPersist> rangePlants = (start < 0 || limit < 0) ? listeUser : listeUser.GetRange(start, limit);
            //return this.Store(new Paging<DataPersist>(rangePlants, listeUser.Count));
            return this.Store(listeUser);

        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeUser");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("UserCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        public ActionResult SelectRole(string ItemUser, string ItemExecMode)
        {
            if (!string.IsNullOrEmpty(ItemExecMode) && (ItemExecMode != Tms.Components.Settings.EnumsDefinition.ADD_NEW))
            {
                Guid id = Guid.Empty;
                if (!string.IsNullOrEmpty(ItemUser))
                {
                    id = Guid.Parse(ItemUser);
                }
                var listeRole = new RoleUser().fnSelect(id);
                return this.Store(listeRole);
            }
            else
            {
                return this.Direct();
            }
            
        }

        public ActionResult SelectModule(string ItemUser)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemUser))
            {
                id = Guid.Parse(ItemUser);
            }
            var liste = new ModuleUsers().fnSelect(id);
            return this.Store(liste);
        }

        public ActionResult SubmitListeRole(string ItemSelected)
        {
            List<Role> roles = JSON.Deserialize<List<Role>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            Store store = X.GetCmp<Store>("storeListeRoleUser");

            foreach (var item in roles)
            {
                item.IsNew = true;
                item.IsDisable = true;
                store.Insert(0, item);
                X.GetCmp<RowSelectionModel>("rowSelectionRoleUser").Select(0);
            }

            X.GetCmp<Window>("FormListeUserRoles").Close();
            return this.Direct();
        }

        public ActionResult RemoveRoleToUser(string ItemSelected)
        {
            try
            {

                RoleUser roleuser = JSON.Deserialize<RoleUser>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeListeRoleUser");



                if (roleuser.IsNew)
                {
                    ModelProxy _proxy = mstore.GetById(roleuser.ID);
                    _proxy.Drop();

                    return this.Direct();
                }

                roleuser.fnGet(roleuser.ID);

                if (roleuser == null || roleuser.ID == Guid.Empty)
                    throw new Exception("RemoveRoleToUser : Retirer Role failed.");

                bool result = roleuser.fnRemove();

                if (result)
                {

                    ModelProxy mProxy = mstore.GetById(roleuser.ID);

                    mProxy.Drop();

                    mProxy.Set(roleuser);

                    mProxy.Commit();

                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Retirer Role",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitListeModule(string ItemSelected)
        {
            List<Module> Modules = JSON.Deserialize<List<Module>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            Store store = X.GetCmp<Store>("storeListeModuleUser");

            foreach (var item in Modules)
            {
                item.IsNew = true;
                item.IsDisable = true;
                store.Insert(0, item);
                X.GetCmp<RowSelectionModel>("rowSelectionModuleUser").Select(0);
            }

            X.GetCmp<Window>("FormListeModuleUser").Close();
            return this.Direct();
        }

        public ActionResult RemoveModuleToUser(string ItemSelected)
        {
            try
            {

                ModuleUsers moduleuser = JSON.Deserialize<ModuleUsers>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeListeModuleUser");



                if (moduleuser.IsNew)
                {
                    ModelProxy _proxy = mstore.GetById(moduleuser.ID);
                    _proxy.Drop();

                    return this.Direct();
                }

                moduleuser.fnGet(moduleuser.ID);

                if (moduleuser == null || moduleuser.ID == Guid.Empty)
                    throw new Exception("Module Item : Retirer Item failed.");

                bool result = moduleuser.fnRemove();

                if (result)
                {

                    ModelProxy mProxy = mstore.GetById(moduleuser.ID);

                    mProxy.Drop();

                    mProxy.Set(moduleuser);

                    mProxy.Commit();

                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Retirer Module Item",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult SubmitNewCredential()
        {

            try
            {               

                string password = HttpUtility.HtmlEncode(Request.Form["password"].ToString());

                string iduser = HttpUtility.HtmlEncode(Request.Form["userid"].ToString());

                Utilisateur user = new Utilisateur();

                user.fnGet(Guid.Parse(iduser));

                if (user == null || user.IDUtilisateur == Guid.Empty)
                    throw new Exception("Login : User not found, please Retry.");

                //user.IDUtilisateur = Guid.Parse(iduser);                

                user.Password = password;                

                user.IsNew = false;                

                if (user == null || user.IDUtilisateur == Guid.Empty)
                    throw new Exception("SubmitCredential : User load failed.");                             

                bool result = user.fnNewPassword();

                if (result)
                {
                    var loginClaim = new Claim(ClaimTypes.NameIdentifier, user.UserName);
                    var claimsIdentity = new ClaimsIdentity(new[] { loginClaim }, DefaultAuthenticationTypes.ApplicationCookie);
                    var ctx = Request.GetOwinContext();
                    var autheticationManager = ctx.Authentication;
                    autheticationManager.SignIn(claimsIdentity);

                    // Add by AK
                    // Set the culture used
                    string culture = "en-US";
                    Session["culture"] = culture;
                    Session["userID"] = user.IDUtilisateur;
                    //set the username when authenticated
                    Session["userName"] = user.UserName;
                    
                    return RedirectToAction("Index", "Workspace");
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        [HttpPost]
        public ActionResult SubmitCredential(string userid)
        {

            try
            {
                if (string.IsNullOrEmpty(userid))
                    throw new Exception("SubmitCredential : User not found, please Close Form and retry.");                

                Utilisateur user = new Utilisateur();

                user.IsNew = false;

                user.fnGet(Guid.Parse(userid));

                if (user == null || user.IDUtilisateur == Guid.Empty)
                    throw new Exception("SubmitCredential : User not found, please Close Form and retry.");

                user.OldPassword = X.GetCmp<TextField>("TxtOldpwd").Text;
                string newpwd = X.GetCmp<TextField>("TxtNewpwd").Text;
                //string confirmpwd = X.GetCmp<TextField>("TxtConfirmpwd").Text;

                user.Password = X.GetCmp<TextField>("TxtNewpwd").Text;

                bool result = user.fnUpdatePassword();

                if (result)
                {
                    X.GetCmp<FormPanel>("FormUserCredentials").Close();
                    return View("Index", "");
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        private Utilisateur MapFormToObject(Utilisateur mClass)
        {
            mClass.Name = X.GetCmp<TextField>("TxtUser").Text;
            mClass.UserName = X.GetCmp<TextField>("TxtUserName").Text;
            mClass.Email = X.GetCmp<TextField>("TxtUserMail").Text;
            mClass.EmployeeNumber = X.GetCmp<TextField>("TxtUserNumber").Text;
            mClass.Password = "0000";
            mClass.FunctionName = X.GetCmp<TextField>("TxtUserFunction").Text;
            mClass.Magasin = new Tms.Classes.Shared.stock.Magasin();
            mClass.Magasin.ID = int.Parse(GetFormValue("cmbMagasin")); 
            mClass.Magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text;

            mClass.BasedInLocation = new Tms.Classes.Shared.Site();
            mClass.BasedInLocation.ID = int.Parse(GetFormValue("cmbSite"));
            mClass.BasedInLocation.Nom = X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
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

        public ActionResult OnPrintUserList()
        {
            
            try
            {
                string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Utilisateur/ViewReportListResult', this, 'List Of Users','')", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "User : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult ViewReportListResult()
        {
            //XtraReport report = null;

            rptListOfUsers report = new rptListOfUsers();
            report.DataSource = DevExpressReportDs.SetDataSource(report);            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


    }
}
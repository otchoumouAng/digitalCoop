using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms2017.MVC.Models;

namespace Tms2017.MVC.Controllers
{

    public class WorkspaceController : Controller
    {
        // GET: Workspace
        [Authorize]
        [NoCache]
        public ActionResult Index()
        {
            //if (this.Session["auth_token"] == null)
            //    return RedirectToAction("Index", "Login");

            //string token = this.Session["auth_token"].ToString();

            //if (!token.Equals(true.ToString()))
            //    return RedirectToAction("Index", "Login");

            //List<Models.Module> mItems = Models.Module.GetAll();

            //return View(mItems);
            try
            {
                ViewBag.UserID = (Guid)Session["userID"] == null ? Guid.Empty : (Guid)Session["userID"];

                string UserName = (string)Session["userName"];
                Fonction HasAccess = new Fonction();

                //if (HasAccess.fnGetUserAccessStatus("{6660B2D2-F2C6-450D-A805-DD7735FE4291}", UserName) == false)
                //{
                //    X.GetCmp<Button>("btnDashboard").Disable();
                //    X.GetCmp<Button>("btnDashboard").Hidden = true;
                //}
                //else
                //{
                //    X.GetCmp<Button>("btnDashboard").Enable();
                //    X.GetCmp<Button>("btnDashboard").Hidden = false;
                //}

                //if (HasAccess.fnGetUserAccessStatus("{C59D8DD3-74F5-4227-A12B-3206067C2F7A}", UserName) == false)
                //{
                //    X.GetCmp<Button>("btnDeliveryMonitor").Disable();
                //    X.GetCmp<Button>("btnDeliveryMonitor").Hidden = true;
                //}
                //else
                //{
                //    X.GetCmp<Button>("btnDeliveryMonitor").Enable();
                //    X.GetCmp<Button>("btnDeliveryMonitor").Hidden = false;
                //}

                //if (HasAccess.fnGetUserAccessStatus("{75C5364C-8E31-46BE-8E0E-9DAAEBC8300B}", UserName) == false)
                //{
                //    X.GetCmp<Button>("btnReporting").Disable();
                //    X.GetCmp<Button>("btnReporting").Hidden = true;
                //}
                //else
                //{
                //    X.GetCmp<Button>("btnReporting").Enable();
                //    X.GetCmp<Button>("btnReporting").Hidden = false;
                //}

                if (HasAccess.fnGetUserAccessStatus("{7DC144CB-2F78-42B0-A4E9-5403734AE24F}", UserName) == false)
                {
                    X.GetCmp<MenuItem>("AppParameters").Disable();
                    X.GetCmp<MenuItem>("AppParameters").Hidden = true;
                }
                else
                {
                    X.GetCmp<MenuItem>("AppParameters").Enable();
                    X.GetCmp<MenuItem>("AppParameters").Hidden = false;
                }

                //if (HasAccess.fnGetUserAccessStatus("{1bedee12-0b70-4e19-942f-978e16bddbb4}", UserName) == false)
                //{
                //    X.GetCmp<Button>("btnDashboardStock").Disable();
                //    X.GetCmp<Button>("btnDashboardStock").Hidden = true;
                //}
                //else
                //{
                //    X.GetCmp<Button>("btnDashboardStock").Enable();
                //    X.GetCmp<Button>("btnDashboardStock").Hidden = false;
                //}
                return View();
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Index", "Login");
            }


        }

        public ActionResult RenderPartialNorth(string containerId)
        {
            return PartialView("PartialViewNorth");
        }

        public ActionResult RenderPartialMenuItems(string containerId)
        {
            //List<Models.Module> mItems = Models.Module.GetAll();

            return PartialView("PartialViewMenuItems", null);
        }


        public Ext.Net.MVC.PartialViewResult PartialLateralMenuView(string containerId)
        {
            //List<Models.Module> mItems = Models.Module.GetAll();

            return new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false,
                ViewName = "PartialLateralMenuView"
            };
        }


        public PartialViewResult ShowFunctions(double? ModuleID)
        {

            string hiddenField = Request.QueryString["module_id"];

            double iModuleID = !string.IsNullOrEmpty(hiddenField) ? double.Parse(hiddenField) : Models.Module.GetAllModules()[0].ID;
            //int iModuleID = ModuleID.HasValue ? ModuleID.Value : 2;//Module.GetAllModules()[0].ID;

            //List<Models.Module> mItems = Models.Module.GetByModuleID(iModuleID);

            //return PartialView("PartialFunctionView", mItems);
            return PartialView("PartialFunctionView");
        }

        //[Authorize]
        public PartialViewResult ShowUserModule(Guid? AppID)
        {
            List<Tms.Classes.Security.Module> listModules = new List<Tms.Classes.Security.Module>();
            try
            {
                Guid hiddenDeFaultApp = (Guid)Session["DefaultApp"];  
                string user = (string)Session["userName"];
                Utilisateur mUser = new Utilisateur();

                //mUser.fnGetByUserName(user);
                mUser.IDUtilisateur = (Guid)Session["userID"];
                Guid idApp;

                if (AppID == null) AppID = hiddenDeFaultApp;
                //else idApp = AppID;

                var mList = new Tms.Classes.Security.Module().fnSelectAllByUserAndApp(mUser.IDUtilisateur, (Guid)AppID, 1, -1);
                if (mList.Count > 0)
                {
                    for (int i = 0; i < mList.Count; i++)
                    {
                        listModules.Add(mList[i] as Tms.Classes.Security.Module);
                    }
                }
                return PartialView("PartialMetroFunctionView", listModules);
            }
            catch (Exception ex)
            {
                return PartialView("PartialMetroFunctionView", listModules);
            }

        }


        public PartialViewResult ShowModules()
        {

            List<Models.Module> mItems = Models.Module.GetAllModules();
            return PartialView("PartialModuleView", mItems);
        }

        //[Authorize]
        public PartialViewResult ShowApplications()
        {
            List<Application> mList = new List<Application>();
            try
            {
                if ((Guid)Session["userID"] != Guid.Empty)
                {
                    var mApp = (new Application()).fnSelectByUser((Guid)Session["userID"], -1);

                    if (mApp.Count > 0)
                    {
                        for (int i = 0; i < mApp.Count; i++)
                        {
                            mList.Add(mApp[i] as Application);
                        }
                        Session["DefaultApp"] = mList.ElementAt(0).ID;
                    }
                    return PartialView("PartialModuleView", mList);
                }
                else
                    return PartialView("PartialModuleView", mList);

            }
            catch (Exception ex)
            {
                return PartialView("PartialModuleView", mList);

            }

        }


        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
            return Content((new Parametres(0)).Base_url);
        }

        public ActionResult CheckSessionExpiration()
        {
            bool sessionExpired = (Session["userName"] == null);
            return Content(sessionExpired.ToString());
        }

        public string CheckSessionValue()
        {
            bool sessionExpired = (Session["userName"] == null);

            return sessionExpired ? string.Empty : (string)Session["userName"];
        }

    }
}
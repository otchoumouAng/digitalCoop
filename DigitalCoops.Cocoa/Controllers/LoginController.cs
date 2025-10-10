using Ext.Net;
using Ext.Net.MVC;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tms.Classes.Security;

namespace Tms2017.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        [NoCache]
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty((string)Session["userName"]) && (Guid)Session["userID"] != Guid.Empty)
            {
                return RedirectToAction("Index", "Workspace");
            }
            //var ar = new System.Globalization.CultureInfo("en");
            //System.Threading.Thread.CurrentThread.CurrentCulture = ar;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = ar;
            if (TempData["Mode"] != null) ViewBag.Mode = Tms.Components.Settings.EnumLoginDefinition.CONNECT;
                else ViewBag.Mode = Tms.Components.Settings.EnumLoginDefinition.DEFAULT;

            if (TempData["UserName"] != null) ViewBag.UserName = TempData["UserName"];

            return View("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [NoCache]
        public ActionResult Authenticate(string returnUrl)
        {
            Utilisateur mUser = new Utilisateur();
            try
            {
                string userName = HttpUtility.HtmlEncode(Request.Form["username"].ToString());

                string password = HttpUtility.HtmlEncode(Request.Form["password"].ToString());                

                mUser.UserName = userName;
                bool resultAuth = true;
                if (!string.IsNullOrEmpty(password))
                {
                    mUser.Password = password;
                    resultAuth = mUser.fnAuthenticate();
                }                
                
                if (resultAuth && mUser.IsAuthenticate) 
                {
                    //if (mUser.MustChangePwd)
                    //{
                    //    ViewBag.ChgPwd = 1;
                    //    return View("Index", mUser);
                    //    //return RedirectToAction("Index", "Login", mUser,);
                    //}
                    var loginClaim = new Claim(ClaimTypes.NameIdentifier,userName);
                    var claimsIdentity = new ClaimsIdentity(new[] { loginClaim }, DefaultAuthenticationTypes.ApplicationCookie);
                    var ctx = Request.GetOwinContext();
                    var autheticationManager = ctx.Authentication;
                    autheticationManager.SignIn(claimsIdentity);

                    // Add by AK
                    // Set the culture used
                    string culture = "en-US";
                    Session["culture"] = culture;

                    //set the username when authenticated
                    Session["userName"] = userName;
                    Session["userID"] = mUser.IDUtilisateur;

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Workspace");
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mode"] = Tms.Components.Settings.EnumLoginDefinition.CONNECT;
                TempData["UserName"] = mUser.UserName;
                TempData["ErrorMsg"] = ex.Message;    
                            
                return RedirectToAction("Index");

            }
            return this.Direct();

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]        
        public ActionResult CheckUser()
        {

            try
            {                

                string userName = HttpUtility.HtmlEncode(Request.Form["username"].ToString());               

                Utilisateur mUser = new Utilisateur();

                mUser.UserName = userName;
                bool resultAuth = true;
                
                resultAuth = mUser.fnCheckUser();                


                if (resultAuth && mUser.IsAuthenticate)
                {
                    if (mUser.MustChangePwd)
                    {
                        ViewBag.Mode = Tms.Components.Settings.EnumLoginDefinition.CHANGEPWD;
                        ViewBag.UserName = mUser.UserName;
                        ViewBag.userID = mUser.IDUtilisateur;
                        return View("Index");
                        //return RedirectToAction("Index", "Login", mUser,);
                    }
                    else
                    {
                        ViewBag.Mode = Tms.Components.Settings.EnumLoginDefinition.CONNECT;
                        ViewBag.UserName = mUser.UserName;
                        return View("Index");
                    }                    
                    
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return RedirectToAction("Index");

            }
            return this.Direct();

        }



        public ActionResult Logout()
        {
            //var ctx = Request.GetOwinContext();
            //var authenticationManager = ctx.Authentication;
            //authenticationManager.SignOut();
            
            return RedirectToAction("Index");
        }
    }
}
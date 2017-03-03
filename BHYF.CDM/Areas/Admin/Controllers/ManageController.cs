using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using BHYF.CDM.Areas.Admin.Models;
using BHYF.CDM.Models;
using System.Collections.Specialized;

namespace BHYF.CDM.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        #region 登录及注销
        public ActionResult Login()
        {
            HttpCookie cookie = Request.Cookies.Get("COOKIE_NAME_FOR_USER");

            if (cookie == null || cookie["COOKIE_USER_NAME"] == null)
                ViewData["username"] = "";
            else
                ViewData["username"] = HttpUtility.UrlDecode(cookie["COOKIE_USER_NAME"].ToString().Trim());

            if (cookie == null || cookie["COOKIE_USER_PASS"] == null)
                ViewData["userpass"] = "";
            else
                ViewData["userpass"] = HttpUtility.UrlDecode(cookie["COOKIE_USER_PASS"].ToString().Trim());

            if (cookie != null)
                ViewData["CHECKBOX"] = "checked=checked";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            // Membership.ValidateUser 判断用户名和密码是否正确
            if (Membership.ValidateUser(model.UserName.Trim(), model.Password.Trim()))
            {
                var strRemember = Request.Form["RememberMe"];
                if (strRemember == "on")
                {
                    HttpCookie cookie = new HttpCookie("COOKIE_NAME_FOR_USER");
                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Values.Add("COOKIE_USER_NAME", HttpUtility.UrlEncode(model.UserName.Trim()));
                    cookie.Values.Add("COOKIE_USER_PASS", HttpUtility.UrlEncode(model.Password.Trim()));
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("COOKIE_NAME_FOR_USER");
                    cookie.Expires = DateTime.Now.AddYears(-1);
                    Request.Cookies.Add(cookie);
                    cookie["COOKIE_USER_NAME"] = null;
                    cookie["COOKIE_USER_PASS"] = null;
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }

                if (Roles.IsUserInRole(model.UserName.Trim(), "BHYFManager"))
                {
                    var pmodel = GetPersonInfo(model.UserName.Trim());
                    if (pmodel != null || pmodel.status == 0)
                    {
                        // 调用Forms 的登录 User.Identity.IsAuthenticated 将设置为True
                        // User.Identity.Name 会设置成我们下面的UserName
                        FormsAuthentication.SetAuthCookie(model.UserName.Trim(), false);
                        Session["personinfo"] = pmodel;

                        var en = GetApplyDesignManager();
                        if (en.designmanager == pmodel.memberid)
                        {
                            return RedirectToAction("DesignApply", "Home");
                        }
                        if (en.reviewmanager == pmodel.memberid)
                        {
                            return RedirectToAction("ApplyAuditing", "Home");
                        }
                        if (en.mgroupid == pmodel.mgroupid)
                        {
                            return RedirectToAction("Demands", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "没有登陆权限！.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户信息已停用或者失效！.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "您所在用户组无登录权限！.");
                }
            }
            else
            {
                ModelState.AddModelError("", "用户名或者密码错误!.");
            }
            return View(model);
        }

        public ActionResult DeliveryLogin()
        {
            HttpCookie cookie = Request.Cookies.Get("COOKIE_NAME_FOR_USER");

            if (cookie == null || cookie["COOKIE_USER_NAME"] == null)
                ViewData["username"] = "";
            else
                ViewData["username"] = HttpUtility.UrlDecode(cookie["COOKIE_USER_NAME"].ToString().Trim());

            if (cookie == null || cookie["COOKIE_USER_PASS"] == null)
                ViewData["userpass"] = "";
            else
                ViewData["userpass"] = HttpUtility.UrlDecode(cookie["COOKIE_USER_PASS"].ToString().Trim());

            if (cookie != null)
                ViewData["CHECKBOX"] = "checked=checked";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeliveryLogin(LoginModel model)
        {
            // Membership.ValidateUser 判断用户名和密码是否正确
            if (Membership.ValidateUser(model.UserName.Trim(), model.Password.Trim()))
            {
                var strRemember = Request.Form["RememberMe"];
                if (strRemember == "on")
                {
                    HttpCookie cookie = new HttpCookie("COOKIE_NAME_FOR_USER");
                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Values.Add("COOKIE_USER_NAME", HttpUtility.UrlEncode(model.UserName.Trim()));
                    cookie.Values.Add("COOKIE_USER_PASS", HttpUtility.UrlEncode(model.Password.Trim()));
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("COOKIE_NAME_FOR_USER");
                    cookie.Expires = DateTime.Now.AddYears(-1);
                    Request.Cookies.Add(cookie);
                    cookie["COOKIE_USER_NAME"] = null;
                    cookie["COOKIE_USER_PASS"] = null;
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }

                if (Roles.IsUserInRole(model.UserName.Trim(), "BHYFManager"))
                {
                    var pmodel = GetPersonInfo(model.UserName.Trim());
                    if (pmodel != null || pmodel.status == 0)
                    {
                        // 调用Forms 的登录 User.Identity.IsAuthenticated 将设置为True
                        // User.Identity.Name 会设置成我们下面的UserName
                        FormsAuthentication.SetAuthCookie(model.UserName.Trim(), false);

                        string menustr = GetMenuList(model.UserName.Trim());
                        if (menustr.Contains("ApplyListManage"))
                        {
                            return RedirectToAction("ApplyListManage", "Delivery");
                        }
                        return RedirectToAction("ApplyList", "Delivery");
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户信息已停用或者失效！.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "您所在用户组无登录权限！.");
                }
            }
            else
            {
                ModelState.AddModelError("", "用户名或者密码错误!.");
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["personinfo"] = null;
            return RedirectToAction("Login", "Manage");
        }

        public ActionResult DeliveryLogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("DeliveryLogin", "Manage");
        }

        public PersonInfo GetPersonInfo(string name)
        {
            using (YwtDbContext ctx = new YwtDbContext())
            {
                var entity = ctx.PersonInfo.FirstOrDefault(m => m.name == name);

                return entity;
            }
        }

        public ApplyDesignManager GetApplyDesignManager()
        {
            using (YwtDbContext ctx = new YwtDbContext())
            {
                var entity = ctx.ApplyDesignManager.FirstOrDefault();

                return entity;
            }
        }
        #endregion

        public string GetMenuList(string name)
        {
            using (YwtDbContext ctx = new YwtDbContext())
            {
                var entity = ctx.PersonInfo.FirstOrDefault(m => m.name == name);

                var list = ctx.DeliveryMenu.Where(m => m.Mgroupid == entity.mgroupid).ToList();
                string strmenu = "";
                foreach (var item in list)
                {
                    strmenu += item.MenuUrl + ",";
                }
                return strmenu.TrimEnd(',');
            }
        }

    }


}
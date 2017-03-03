using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BHYF.CDM.Areas.Activity.Controllers
{
    public class XuebaController : Controller
    {
        //
        // GET: /Activity/Xueba/
        public ActionResult Index(string source, string uid)
        {
            ViewBag.Title = "我是小学霸";
            ViewBag.uid = uid;
            ViewBag.source = source;

            return View();
        }

        public ActionResult ActivityRules(string source, string uid)
        {
            ViewBag.Title = "我是小学霸-活动规则";
            ViewBag.source = source;
            ViewBag.uid = uid;
            return View();
        }

        /// <summary>
        /// 我要参加- 上传资料
        /// </summary>
        public ActionResult Join(string source, string uid)
        {
            ViewBag.Title = "我是小学霸-上传资料";
            ViewBag.uid = uid;
            ViewBag.source = source;
            return View();
        }

        /// <summary>
        /// 我的星星墙- 个人主页
        /// </summary>
        public ActionResult Detail(int id, string source, string uid)
        {
            ViewBag.Title = "我是小学霸-个人主页";
            ViewBag.id = id;
            ViewBag.source = source;
            ViewBag.uid = uid;
            return View();
        }

        public ActionResult List(string source, string uid)
        {
            ViewBag.Title = "我是小学霸-排行榜";
            ViewBag.source = source;
            ViewBag.uid = uid;
            return View();
        }

        public ActionResult Address()
        {
            ViewBag.Title = "我是小学霸-编辑收货地址";
            return View();
        }

        public ActionResult Editadd(int id,string source,string uid)
        {
            ViewBag.Title = "我是小学霸-编辑收货地址";
            ViewBag.id = id;
            ViewBag.source = source;
            ViewBag.uid = uid;
            return View();
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        private string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }


    }
}
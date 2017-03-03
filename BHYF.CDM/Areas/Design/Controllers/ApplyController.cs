using BHYF.CDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHYF.CDM.Areas.Design.Controllers
{
    public class ApplyController : Controller
    {
        /// <summary>
        /// 科学环创首页
        /// </summary>
        /// <param name="source">来源 ios/android</param>
        /// <param name="uid">用户id</param>
        /// <param name="utype">用户类型</param>
        /// <returns></returns>
        public ActionResult Index(string source, string uid, string utype)
        {
            ViewBag.source = source;
            ViewBag.userid = uid;

            if (!string.IsNullOrEmpty(uid))
            {
                using (YwtDbContext ctx = new YwtDbContext())
                {
                    var entity = ctx.DesignApply.FirstOrDefault(m => m.apply_userId == uid);
                    if (entity != null && entity.apply_id > 0)
                        return RedirectToAction("Successful", new { applyid = entity.apply_id });
                }
            }

            return View();
        }

        /// <summary>
        /// 立即申请表单页
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult ApplyInfo(string source, string userid)
        {
            if (string.IsNullOrEmpty(source))
                source = "微信";

            ViewBag.source = source;
            ViewBag.userid = userid;
            ViewBag.curtime = DateTime.Now.Ticks;


            if (!string.IsNullOrEmpty(userid))
            {
                using (YwtDbContext ctx = new YwtDbContext())
                {
                    var entity = ctx.DesignApply.FirstOrDefault(m => m.apply_userId == userid);
                    if (entity != null && entity.apply_id > 0)
                        return RedirectToAction("Successful", new { applyid = entity.apply_id });
                }
            }


            return View();
        }

        /// <summary>
        /// 完善信息
        /// </summary>
        /// <param name="applyid">设计申请id</param>
        /// <returns></returns>
        public ActionResult MoreInfo(int applyid, string source)
        {
            ViewBag.applyid = applyid;
            ViewBag.source = source;
            return View();
        }

        /// <summary>
        /// 我的申请
        /// </summary>
        /// <param name="applyid">设计申请id</param>
        /// <returns></returns>
        public ActionResult MyDemand(int applyid)
        {
            ViewBag.applyid = applyid;
            return View();
        }

        /// <summary>
        /// 申请成功页面
        /// </summary>
        /// <param name="applyid"></param>
        /// <returns></returns>
        public ActionResult Successful(int applyid)
        {
            ViewBag.applyid = applyid;
            return View();
        }

    }
}
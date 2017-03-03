using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using BHYF.CDM.Areas.Admin.Models;
using BHYF.CDM.Models;
using System.Data;

namespace BHYF.CDM.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        #region 设计申请与管理
        /// <summary>
        /// 我提的需求
        /// </summary>
        /// <returns></returns>
        [CheckLogin()]
        public ActionResult Demands()
        {
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            return View();
        }

        /// <summary>
        /// 编辑需求
        /// </summary>
        /// <returns></returns>
        //[CheckLogin()]
        public ActionResult EditDemand(int? id)
        {
            ViewBag.ID = id;
            if (id == null)
            {
                ViewBag.ID = 0;
            }
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            return View();
        }
        #endregion

        #region 需求审批
        [CheckLogin()]
        public ActionResult ApplyAuditing()
        {
            ViewBag.Title = "科学环创-需求审批";
            ViewBag.ApplyStatus = "WaitAuditing,WaitConfirm,Refuse,";
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            ViewData["applyenumlist"] = new SelectList(this.EnumList(typeof(ApplyStaus)), "value", "text");
            ViewData["applysource"] = new SelectList(this.EnumList(typeof(ApplySource)), "value", "text");
            return View();
        }

        /// <summary>
        /// 需求查看及审批
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CheckLogin()]
        public ActionResult ApplyRead(int? id)
        {
            ViewBag.ID = id;
            if (id == null)
            {
                ViewBag.ID = 0;
            }
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            ViewData["applyenumlevel"] = new SelectList(this.EnumList(typeof(ApplyLevel)), "value", "text");
            ViewData["applyenumlist"] = new SelectList(this.EnumList(typeof(ApplyReviewStatus)), "value", "text");

            return View();
        }
        #endregion
      
        #region 需求设计
        [CheckLogin()]
        public ActionResult DesignApply()
        {
            ViewBag.Title = "科学环创-需求设计";
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            ViewData["applyenumlist"] = new SelectList(this.EnumList(typeof(DesignStatus)), "value", "text");
            return View();
        }

        /// <summary>
        /// 设计方案
        /// </summary>
        /// <returns></returns>
        public ActionResult DesignPlan(int? applyid)
        {
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            ViewBag.Title = "设计方案";
            ViewBag.applyid = applyid;
            
            return View();
        }

        /// <summary>
        /// 添加设计方案
        /// </summary>
        /// <param name="applyid"></param>
        /// <returns></returns>
        public ActionResult AddDesignPlan(int? applyid)
        {
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            ViewBag.Title = "添加设计方案";
            ViewBag.applyid = applyid;
            return View();
        }
        #endregion

        #region 指派给我
        [CheckLogin()]
        public ActionResult AssignMe()
        {
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            return View();
        }
        #endregion

        #region 方案审核
        /// <summary>
        /// 方案审核
        /// </summary>
        /// <returns></returns>
        public ActionResult DesignReview()
        {
            ViewBag.Title = "科学环创-方案审核";
            ViewBag.ApplyStatus = "Auditing,Designing";
            ViewBag.Memberid = (Session["personinfo"] as PersonInfo).memberid;
            ViewData["applyenumlist"] = new SelectList(this.EnumList(typeof(DesignStatus)), "value", "text");
            ViewData["applysource"] = new SelectList(this.EnumList(typeof(ApplySource)), "value", "text");
            return View();
        }
        #endregion 

    }
}
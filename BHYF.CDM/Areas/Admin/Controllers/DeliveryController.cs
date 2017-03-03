using BHYF.CDM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BHYF.CDM.Areas.Admin.Controllers
{
    public class DeliveryController : DeliveryBaseController
    {
        //
        // GET: /Admin/Delivery/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CreateApply(int? id)
        {
            ViewBag.LoginUser = System.Web.HttpContext.Current.User.Identity.Name;
            ViewBag.Title = "新增申请";
            ViewBag.Id = id ?? 0;
            return View();
        }

        public ActionResult ApplyList()
        {
            ViewBag.Title = "申请列表";
            ViewData["deliverystatuslist"] = new SelectList(this.EnumList(typeof(DeliveryApplyStatus)), "value", "text");
            return View();
        }

        public ActionResult ApplyDetail(int id)
        {
            ViewBag.Title = "申请详情";
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Pending(int id)
        {
            ViewBag.Title = "申请审批";
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Invoice(int id)
        {
            ViewBag.Title = "发货单";
            ViewBag.InvoiceNumber = "P" + DateTime.Now.ToString("yyyyMMddHHmmss");
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Receipt(int id)
        {
            ViewBag.Id = id;
            ViewBag.Title = "收货回执单";
            return View();
        }

        public ActionResult ApplyListManage()
        {
            ViewBag.Title = "申请列表";
            ViewData["deliverystatuslist"] = new SelectList(this.EnumList(typeof(DeliveryApplyStatus)), "value", "text");
            return View();
        }

        public ActionResult MenuManage()
        {
            ViewBag.Title = "菜单管理";
            ViewData["deliverymenutype"] = new SelectList(this.EnumList(typeof(DeliveryMenuType)), "value", "text");
            return View();
        }

        public ActionResult PowerManage()
        {
            ViewBag.Title = "权限管理";
            ViewData["deliverymenutype"] = new SelectList(this.EnumList(typeof(DeliveryPowerType)), "value", "text");
            return View();
        }

        public ActionResult ProductManage()
        {
            ViewBag.Title = "产品管理";
            return View();
        }

        public ActionResult WaitAudit()
        {
            ViewBag.Title = "待公司审批";
            return View();
        }


    }

    public class DeliveryBaseController : Controller
    {
        public DeliveryBaseController()
        {
            if (FormsAuthentication.IsEnabled)
            {
                var managerName = System.Web.HttpContext.Current.User.Identity.Name;
                if (!string.IsNullOrEmpty(managerName))
                {
                    var pmodel = GetPersonInfo(managerName);
                    ViewBag.manager = managerName;
                    ViewBag.mgroupid = pmodel.mgroupid;
                    ViewBag.memberid = pmodel.memberid;
                    ViewBag.powerstr = GetDeliveryPower(pmodel.mgroupid);
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (FormsAuthentication.IsEnabled)
            {
                var managerName = System.Web.HttpContext.Current.User.Identity.Name;
                if (string.IsNullOrEmpty(managerName))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Manage", Action = "DeliveryLogin" }));
                }
            }
        }

        public PersonInfo GetPersonInfo(string name)
        {
            using (YwtDbContext ctx = new YwtDbContext())
            {
                var entity = ctx.PersonInfo.FirstOrDefault(m => m.name == name);

                return entity;
            }
        }

        /// <summary>
        /// 获取当前用户组的权限代码集合
        /// </summary>
        /// <param name="mgid"></param>
        /// <returns></returns>
        public string GetDeliveryPower(int mgid)
        {
            using (YwtDbContext ctx = new YwtDbContext())
            {
                var list = ctx.DeliveryPower.Where(m => m.Mgroupid == mgid);
                var powerstr = "";
                if (list != null || list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        powerstr += item.PowerCode + ",";
                    }
                }
                return powerstr.TrimEnd(',');
            }
        }

        /// <summary>
        /// 获得某个Enum类型的绑定列表
        /// </summary>
        /// <param name="enumType">枚举的类型，例如：typeof(Sex)</param>
        public List<ListItem> EnumList(Type enumType)
        {
            if (enumType.IsEnum != true)
            {    //不是枚举的要报错
                throw new InvalidOperationException();
            }
            List<ListItem> li = new List<ListItem>();
            //获得特性Description的类型信息
            Type typeDescription = typeof(DescriptionAttribute);
            //获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            System.Reflection.FieldInfo[] fields = enumType.GetFields();

            //检索所有字段
            foreach (FieldInfo field in fields)
            {
                //过滤掉一个不是枚举值的，记录的是枚举的源类型
                if (field.FieldType.IsEnum == true)
                {
                    // 通过字段的名字得到枚举的值
                    // 枚举的值如果是long的话，ToChar会有问题，但这个不在本文的讨论范围之内
                    //object value = (char)(int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                    string text = "";
                    //获得这个字段的所有自定义特性，这里只查找Description特性
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        //因为Description这个自定义特性是不允许重复的，所以我们只取第一个就可以了！
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        //获得特性的描述值，也就是‘男’‘女’等中文描述
                        text = aa.Description;
                    }
                    else
                    {
                        //如果没有特性描述（-_-!忘记写了吧~）那么就显示英文的字段名
                        text = field.Name;
                    }
                    li.Add(new ListItem
                    {
                        Value = field.Name,
                        Text = text
                    });
                }
            }

            return li;
        }

    }
}
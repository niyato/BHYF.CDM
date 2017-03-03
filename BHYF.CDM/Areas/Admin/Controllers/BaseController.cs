using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel;
using System.Reflection;

using BHYF.CDM.Models;
using System.Web.UI.WebControls;


namespace BHYF.CDM.Areas.Admin.Controllers
{
    [CheckLogin]
    public class BaseController : Controller
    {
        //
        // GET: /Admin/Base/
        public ActionResult Index()
        {
            return View();
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
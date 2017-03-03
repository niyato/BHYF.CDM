using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHYF.CDM.Models
{
    [Table("bhyf_design_apply")]
    public class DesignApply
    {
        [Key]
        public int apply_id { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string apply_identity { get; set; }
        /// <summary>
        /// 申请人名称
        /// </summary>
        public string apply_name { get; set; }
        public string apply_phone { get; set; }
        public string apply_address { get; set; }
        public string apply_gardenName { get; set; }
        /// <summary>
        /// 园性质
        /// </summary>
        public string apply_nature { get; set; }
        /// <summary>
        /// 预算
        /// </summary>
        public string apply_budget { get; set; }
        public DateTime? apply_createTime { get; set; }
        /// <summary>
        /// 管理员id
        /// </summary>
        public string apply_userId { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string apply_source { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public string apply_time { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string apply_status { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string apply_content { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string apply_imgs { get; set; }
        /// <summary>
        /// 环创区域
        /// </summary>
        public string apply_area { get; set; }
        /// <summary>
        /// 主题风格
        /// </summary>
        public string apply_style { get; set; }
        /// <summary>
        /// 方案预计完成时间
        /// </summary>
        public string apply_plantime { get; set; }

        #region 审批
        /// <summary>
        /// 优先级
        /// </summary>
        public string review_level { get; set; }
        /// <summary>
        /// 指派给外服
        /// </summary>
        public Guid? review_manager { get; set; }
        /// <summary>
        /// 审批说明
        /// </summary>
        public string review_content { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? review_time { get; set; }
        #endregion
        /// <summary>
        /// 方案提交状态
        /// </summary>
        public bool? plan_status { get; set; }
        /// <summary>
        /// 方案确认状态
        /// </summary>
        public string plan_conform { get; set; }

    }

    [Table("bhyf_yedr_datum")]
    public class YearDatum
    {
        [Key]
        public int id { set; get; }

        public string userName { set; get; }

        public string phone { set; get; }

        public string QQ { set; get; }

        public string job { set; get; }

        public string sex { set; get; }

        public string label { set; get; }

        public string picUrl { set; get; }

        public DateTime? createTime { set; get; }

        public int share { set; get; }

        public int status { set; get; }

        public string parenting { set; get; }

        public string userId { set; get; }

        public string source { set; get; }

    }
    [Table("bhyf_yedr_thumb")]
    public class YedrThumb
    {
        public int id { set; get; }
        public int datumId { set; get; }

        public int accountId { set; get; }
        public DateTime time { set; get; }
    }

    [Table("bhyf_yedr_event")]
    public class YedrEvent
    {
        public int id { set; get; }

        public string eventName { set; get; }

        public DateTime? startTime { set; get; }

        public DateTime? stopTime { set; get; }

        public int pvCount { set; get; }

        public int uvCount { set; get; }

        public int Status { set; get; }
    }

    [Table("bhyf_design_plan")]
    public class DesignPlan
    {
        [Key]
        public int DesignPlanId { get; set; }

        public int ApplyId { get; set; }
        public string DesignDesc { get; set; }
        public string DesignFileName { get; set; }
        public string DesignFilePath { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreaterName { get; set; }
        public string ChangeDesc { get; set; }

    }
}
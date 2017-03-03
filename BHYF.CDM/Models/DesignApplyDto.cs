using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHYF.CDM.Models
{
    public class DesignApplyDto
    {
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
    }

    public class MoreDesignApplyDto
    {
        public int apply_id { get; set; }
        /// <summary>
        /// 园性质
        /// </summary>
        public string apply_nature { get; set; }
        /// <summary>
        /// 预算
        /// </summary>
        public string apply_budget { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public string apply_time { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string apply_content { get; set; }
        /// <summary>
        /// 环创区域
        /// </summary>
        public string apply_area { get; set; }
        /// <summary>
        /// 主题风格
        /// </summary>
        public string apply_style { get; set; }
    }

    public class ReviewDesignApplyDto 
    {
        public int apply_id { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string apply_status { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public string review_level { get; set; }
        /// <summary>
        /// 指派给外服
        /// </summary>
        public string review_manager { get; set; }
        /// <summary>
        /// 审批说明
        /// </summary>
        public string review_content { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? review_time { get; set; }
    }

}
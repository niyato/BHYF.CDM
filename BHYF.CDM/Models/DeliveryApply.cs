using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BHYF.CDM.Models
{

    [Table("bhyf_delivery_apply")]
    public class DeliveryApply
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 申请单编号
        /// </summary>
        public string Num { get; set; }
        public string Status { get; set; }
        public string Memberid { get; set; }
        /// <summary>
        /// agency:办事处 agent:代理商
        /// </summary>
        public string ApplySource { get; set; }
        /// <summary>
        /// 办事处名称
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplyDate { get; set; }
        /// <summary>
        /// 办事处联系人
        /// </summary>
        public string Relation { get; set; }
        public string Phone { get; set; }

        public string Mail { get; set; }
        public string AgentSource { get; set; }
        public string GardenName { get; set; }
        public string AgentRelation { get; set; }
        public string AgentPhone { get; set; }
        public string AgentAddress { get; set; }
        public string UseDate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string CreateUid { get; set; }

        public string DepartmentLeaderSign { get; set; }
        public string CompanyLeaderSign { get; set; }
        public string BossSign { get; set; }
        public string Img { get; set; }
        public ICollection<DeliveryApplyProduct> DeliveryApplyProducts { get; set; }
    }

    [Table("bhyf_delivery_applyproduct")]
    public class DeliveryApplyProduct
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("DeliveryApply")]
        public int ApplyId { get; set; }
        public int Pid { get; set; }
        /// <summary>
        /// 新增时的产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Sum { get; set; }
        /// <summary>
        /// 销售类型
        /// </summary>
        public string SaleType { get; set; }
        /// <summary>
        /// 回款日期
        /// </summary>
        public string ReturnDate { get; set; }
        /// <summary>
        /// 回款金额
        /// </summary>
        public decimal ReturnPrice { get; set; }

        //public virtual DeliveryProduct DeliveryProduct { get; set; }
        public virtual DeliveryApply DeliveryApply { get; set; }
    }

    
}
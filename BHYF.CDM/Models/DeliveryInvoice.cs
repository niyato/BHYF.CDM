using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BHYF.CDM.Models
{
    [Table("bhyf_delivery_invoice")]
    public class DeliveryInvoice
    {
        [Key]
        public int Id{get;set;}
        /// <summary>
        /// 发货申请id
        /// </summary>
        public int ApplyId{get;set;}
        /// <summary>
        /// 发货日期 
        /// </summary>
        public string DeliveryDate{get;set;}
        /// <summary>
        /// 发货编号
        /// </summary>
        public string DeliveryNumber{get;set;}
        /// <summary>
        /// 办事处名称
        /// </summary>
        public string AgencyName{get;set;}
        /// <summary>
        /// 代理商名称
        /// </summary>
        public string AgentName{get;set;}
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string ReceiveName{get;set;}
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiveAddress{get;set;}
        /// <summary>
        /// 物流公司
        /// </summary>
        public string SendCompany{get;set;}
        /// <summary>
        /// 物流单号
        /// </summary>
        public string SendNumber{get;set;}
        /// <summary>
        /// 质检
        /// </summary>
        public string MassCheck{get;set;}
        /// <summary>
        /// 包装
        /// </summary>
        public string Packing{get;set;}
        /// <summary>
        /// 复印件凭证
        /// </summary>
        public string Img{get;set;}
        /// <summary>
        /// 保管员
        /// </summary>
        public string KeepName{get;set;}
        /// <summary>
        /// 制表
        /// </summary>
        public string TabName{get;set;}
        /// <summary>
        /// 备注
        /// </summary>
        public string Description{get;set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime{get;set;}

    }
}
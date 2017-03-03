using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHYF.CDM.Models
{

    [Table("bhyf_delivery_receipt")]
    public class DeliveryReceipt
    {
        [Key]
        public int Id { get; set; }
        public int ApplyId { get; set; }
        /// <summary>
        /// 收货方名称
        /// </summary>
        public string ReceiveCompany { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiveAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ReceiveName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string ReceivePhone { get; set; }
        /// <summary>
        /// 收货确认姓名
        /// </summary>
        public string TakeName { get; set; }
        /// <summary>
        /// 收货确认联系方式
        /// </summary>
        public string TakePhone { get; set; }
        /// <summary>
        /// 收货回执单复印件凭证
        /// </summary>
        public string Img { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
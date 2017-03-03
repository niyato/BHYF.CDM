using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHYF.CDM.Models
{
    [Table("bhyf_delivery_product")]
    public class DeliveryProduct
    {
        [Key]
        public int Pid { get; set; }

        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BHYF.CDM.Models
{
    [Table("bhyf_delivery_menu")]
    public class DeliveryMenu
    {
        [Key]
        public int Id { get; set; }

        public int Mgroupid { get; set; }
        public string MenuName { get; set; }
        public string MenuCode { get; set; }
        public string MenuUrl { get; set; }
        public string MenuDesc { get; set; }
        public string GroupName { get; set; }
    }

    [Table("bhyf_delivery_power")]
    public class DeliveryPower
    {
        [Key]
        public int Id { get; set; }
        public int Mgroupid { get; set; }
        public string PowerCode { get; set; }
        public string PowerName { get; set; }
        public string PowerDesc { get; set; }
        public string GroupName { get; set; }
    }

    [Table("bhyf_managegroup")]
    public class ManageGroup
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public Int16 status { get; set; }
    }

}
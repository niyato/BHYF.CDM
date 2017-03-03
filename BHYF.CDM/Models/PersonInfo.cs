using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHYF.CDM.Models
{
    [Table("bhyf_person")]
    public class PersonInfo
    {
        [Key]
        public Int64 ID { get; set; }
        public string name { get; set; }

        public Int16 status { get; set; }

        public Guid memberid { get; set; }
        /// <summary>
        /// 管理员组ID
        /// </summary>
        public int mgroupid { get; set; }
    }
}
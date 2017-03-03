using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHYF.CDM.Models
{
     [Table("bhyf_manager")]
    public class Manager
    {
         [Key]
         public int personid { get; set; }
         public Guid memberid { get; set; }
    }

    [Table("bhyf_design_manager")]
     public class ApplyDesignManager
     {
        [Key]
         public Guid designmanager { get; set; }
         public Guid reviewmanager { get; set; }
         public int mgroupid { get; set; }
     }
}
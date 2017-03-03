using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHYF.CDM.Models
{
    [Table("bhyf_design_event")]
    public class DesignEvent
    {
        [Key]
        public int event_id { get; set; }
        public string event_name { get; set; }
        public DateTime? event_stopTime { get; set; }
        public int? event_pvCount { get; set; }
        public int? event_uvCount { get; set; }
        public int? event_share { get; set; }
        public DateTime? event_startTime { get; set; }

    }
}
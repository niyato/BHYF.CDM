using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHYF.CDM.Models
{
    [Serializable]
    public class ActivityModel
    {
        public YedrEvent Event { set; get; }

        public int ShareCount { set; get; }

        public int SignUpCount { set; get; }

        public long TotalVoteCount { set; get; }
    }
}
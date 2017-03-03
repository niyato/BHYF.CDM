using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BHYF.CDM.Models
{
    [Table("bhyf_activity_superscholar")]
    /// <summary>
    /// 我是小学霸活动表实体
    /// </summary>
    public class ActivitySuperScholar
    {
        [Key]
        public int Id { get; set; }
        public string Uid { get; set; }

        public string Name { get; set; }
        public int? Age { get; set; }

        public string Describe { get; set; }

        public int Likes { get; set; }

        public string ImgUrl { get; set; }
        public string Source { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string ReceiveName { get; set; }
        public string Phone { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }

        public int AccessNum { get; set; }

    }

    public class ActivitySuperScholarCreateDto
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Describe { get; set; }
        public string Source { get; set; }
        public string ImgUrl { get; set; }
    }

    public class ActivitySuperScholarShowDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }

        public string Name { get; set; }
        public int? Age { get; set; }

        public string Describe { get; set; }

        public int Likes { get; set; }

        public int Rank { get; set; }

        public string ReceiveName { get; set; }
        public string Phone { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }

        public string ImgUrl { get; set; }
    }

    public class ActivitySuperScholarEditDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string ReceiveName { get; set; }
        public string Phone { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }
    }

    /// <summary>
    /// 小学霸活动统计
    /// </summary>
    public class ActivitySuperScholarStatistic
    {
        /// <summary>
        /// 参与总人数
        /// </summary>
        public int JoinCount { get; set; }
        /// <summary>
        /// 点赞总次数
        /// </summary>
        public int LikesCount { get; set; }
        /// <summary>
        /// 浏览总次数
        /// </summary>
        public int PvCount { get; set; }
    }

}
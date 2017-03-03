using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CDM.Infrastructure.WebApi
{
    public class Envelope
    {
        /// <summary>
        /// 单页最大数
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int? Total { get; set; }
    }

    public class Envelope<T>
    {
        /// <summary>
        /// 数据实体
        /// </summary>
        public T data { get; set; }
        /// <summary>
        /// 单页最大数
        /// </summary>
        public int? limit { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int? page { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int? total { get; set; }
    }
}

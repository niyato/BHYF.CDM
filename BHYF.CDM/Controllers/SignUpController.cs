using BHYF.CDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using CDM.Infrastructure.WebApi;
using System.Collections;
using System.Diagnostics;
namespace BHYF.CDM.Controllers
{
    [RoutePrefix("SignUp")]
    public class SignUpController : StemApiController
    {
        private static Dictionary<int, int> table = new Dictionary<int, int>();
        YwtDbContext db = new YwtDbContext();
        [Route("GetAllSignUP")]
        public IHttpActionResult GetAllSignUP(string Name = null, string Num = null, int? limit = null,
           int? page = null)
        {
            List<ActivityNumModel> Model = db.Database.SqlQuery<ActivityNumModel>("select datumId as ID,count(datumId) as Count from bhyf_yedr_thumb group by datumId").ToList();
            for (int i = 0; i < Model.Count; i++)
            {
                if (table.ContainsKey(Model[i].ID))
                {
                    int value = 0;
                    if (table.TryGetValue(Model[i].ID, out value))
                    {
                        Model[i].Count = value;
                    }
                }
            }

            var list = db.YearDatum.Where(a => a.status == 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(a => a.userName == Name);
            }
            if (!string.IsNullOrEmpty(Num))
            {
                list = list.Where(a => a.userId == Num);
            }
            var YearDatums = list.ToList();
            var ResultList = YearDatums.Join(Model, a => a.id, b => b.ID, (a, b) => new
            {
                id = a.id,
                userName = a.userName,
                phone = a.phone,
                QQ = a.QQ,
                job = a.job,
                sex = a.sex,
                label = a.label,
                source = a.source,
                userId = a.userId,
                status = a.status,
                createTime = a.createTime,
                parenting = a.parenting,
                picUrl = a.picUrl,
                share = a.share,
                count = b.Count
            }).OrderByDescending(a=>a.count).AsQueryable();
            if (limit != null)
            {
                var envelope = new Envelope { Limit = limit, Page = page, Total = ResultList.Count() };
                if (page != null) ResultList = ResultList.Skip((page.Value - 1) * limit.Value);
                ResultList = ResultList.Take(limit.Value);
                var entites = ResultList.ToList();
                return Ok(entites, envelope);
            }
            else
            {
                var entites = ResultList.ToList();
                return Ok(entites);
            }
        }

        [Route("Update")]
        [HttpGet]
        public IHttpActionResult Update(int id,int count)
        {
            if (table.ContainsKey(id))
            {
                table.Remove(id);
            }
            table.Add(id, count);
            return Ok("OK");
        }
        [Route("GetAllActivity")]
        public HttpResponseMessage GetAllActivity()
        {
            long? TotalVoteCount = 0;
            YedrEvent Event = null;
            Task t1 = Task.Factory.StartNew(() =>
            {
                using (YwtDbContext contextdb = new YwtDbContext())
                {
                    Event = contextdb.YedrEvent.FirstOrDefault(a => a.Status == 1);
                    TotalVoteCount = contextdb.YedrThumb.AsParallel().Count(a => a.datumId == Event.id);
                }
            });
            int? SignUpCount = db.YearDatum.Count(a => a.status == 1);
            int? ShareCount = db.YearDatum.AsParallel().Where(a => a.status == 1).Sum(a => a.share);
            Task.WaitAll(t1);
            ActivityModel Model = new ActivityModel()
            {
                Event = Event,
                TotalVoteCount = TotalVoteCount.HasValue ? TotalVoteCount.Value : 0,
                SignUpCount = SignUpCount.HasValue ? SignUpCount.Value : 0,
                ShareCount = ShareCount.HasValue ? ShareCount.Value : 0
            };
            var JsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(Model);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonResult, System.Text.Encoding.UTF8, "text/json")
            };
        }
    }
}

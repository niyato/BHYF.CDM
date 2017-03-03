using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using BHYF.CDM.Models;
using CDM.Infrastructure.WebApi;
using AutoMapper;

namespace BHYF.CDM.Controllers
{
    [RoutePrefix("v1/superscholar")]
    public class SuperScholarController : StemApiController
    {
        YwtDbContext ctx;

        // GET api/superscholar
        public List<ActivitySuperScholarShowDto> GetList()
        {
            using (ctx = new YwtDbContext())
            {
                var query = from s in ctx.ActivitySuperScholar
                            orderby s.Likes descending
                            select s;
                return query.AsEnumerable().Select((player, index) => new ActivitySuperScholarShowDto()
                {
                    Age = player.Age,
                    Describe = player.Describe,
                    Id = player.Id,
                    Likes = player.Likes,
                    Name = player.Name,
                    Uid = player.Uid,
                    Address = player.Address,
                    Area = player.Area,
                    Phone = player.Phone,
                    ReceiveName = player.ReceiveName,
                    Rank = index + 1,
                    ImgUrl=player.ImgUrl
                }).ToList();
            }
        }

        [Route]
        public IHttpActionResult Get()
        {
            return Ok(GetList());
        }

        [Route("{id:int}")]
        // GET v1/superscholar/5
        public IHttpActionResult Get(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = GetList().FirstOrDefault(m => m.Id == id);
                if (entity == null)
                    return NotFound();

                AddPv(id);

                return Ok(entity);
            }
        }

        /// <summary>
        /// 增加pv
        /// </summary>
        /// <param name="id"></param>
        public void AddPv(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.ActivitySuperScholar.FirstOrDefault(m => m.Id == id);
                if (entity == null)
                    return;

                entity.AccessNum += 1;
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// 添加投票
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("AddLikes")]
        [HttpPost]
        public IHttpActionResult AddLikes([FromBody]int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.ActivitySuperScholar.FirstOrDefault(m => m.Id == id);
                if (entity == null)
                    return NotFound();

                entity.Likes += 1;
                ctx.SaveChanges();

                return Ok(entity);
            }
        }


        [Route("GetStatistics")]
        public IHttpActionResult GetStatistics()
        {
            using (ctx = new YwtDbContext())
            {
                var model = new ActivitySuperScholarStatistic()
                {
                    JoinCount = ctx.ActivitySuperScholar.Count(),
                    LikesCount = ctx.ActivitySuperScholar.Sum(m => m.Likes),
                    PvCount = ctx.ActivitySuperScholar.Sum(m => m.AccessNum)
                };
                return Ok(model);
            }
        }

        [Route]
        public IHttpActionResult Get(string uid)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = GetList().FirstOrDefault(m => m.Uid == uid);
                if (entity == null)
                    return Ok("");

                return Ok(entity);
            }
        }

        /// <summary>
        /// 小学霸列表页关键字搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Route("GetInfoByKeyWord")]
        public IHttpActionResult GetInfoByKeyWord(string keyword)
        {
            using (ctx = new YwtDbContext())
            {
                var list = GetList().Where(m => m.Name.Contains(keyword) || keyword == m.Id.ToString());
                return Ok(list);
            }
        }

        ///<summary>
        ///小学霸列表页id搜索
        /// </summary>
        /// <param name="id"></param>
        /// <return></return>

 
        /// <summary>
        /// 我是小学霸 参加活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route]
        [HttpPost]
        // POST api/superscholar
        public IHttpActionResult Post([FromBody]ActivitySuperScholarCreateDto input)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = new ActivitySuperScholar();
                Mapper.Map<ActivitySuperScholarCreateDto, ActivitySuperScholar>(input, entity);
                entity.CreateTime = DateTime.Now;
                entity.Likes = 0;
                entity.AccessNum = 0;
                ctx.ActivitySuperScholar.Add(entity);
                ctx.SaveChanges();
                return Ok(entity);
            }
            
        }

        // PUT api/superscholar/5
        public void Put(int id, [FromBody]string value)
        {
        }


        [Route]
        public IHttpActionResult Put([FromBody]ActivitySuperScholarEditDto input)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.ActivitySuperScholar.FirstOrDefault(m => m.Id == input.Id);
                if (entity == null)
                    return NotFound();

                Mapper.Map<ActivitySuperScholarEditDto, ActivitySuperScholar>(input, entity);
                ctx.SaveChanges();
                return Ok(entity);
            }
        }

        // DELETE api/superscholar/5
        public void Delete(int id)
        {
        }
    }
}

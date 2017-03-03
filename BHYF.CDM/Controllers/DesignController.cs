using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

using BHYF.CDM.Models;
using CDM.Infrastructure.WebApi;
using AutoMapper;


namespace BHYF.CDM.Controllers
{
    [RoutePrefix("v1/design")]
    public class DesignController : StemApiController
    {
        YwtDbContext ctx;

        /// <summary>
        /// 科学环创请求分页
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="userid">用户id查询</param>
        /// <param name="gname">园所名称模糊查询</param>
        /// <param name="status">状态</param>
        /// <param name="orderBy"></param>
        /// <param name="reviewmanager">指派给谁</param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        [Route]
        public IHttpActionResult Get(
           int? limit = null,
           int? page = null,
           string userid = null,
           string gname = null,
           string status = null,
           string orderBy = null,
           string source = null,
           Guid? reviewmanager = null,
           bool ascending = true)
        {
            using (ctx = new YwtDbContext())
            {
                //var chain = ctx.DesignApply.AsQueryable();
                //左联
                var chain = from u in ctx.DesignApply
                            join m in ctx.PersonInfo on u.review_manager equals m.memberid  into temp
                            from tt in temp.DefaultIfEmpty()
                            select new
                            {
                                u.apply_address,
                                u.apply_area,
                                u.apply_budget,
                                u.apply_content,
                                u.apply_createTime,
                                u.apply_gardenName,u.apply_id,
                                u.apply_identity,
                                u.apply_name,
                                u.apply_nature,
                                u.apply_phone,
                                u.apply_plantime,
                                u.apply_source,
                                u.apply_status,
                                u.apply_style,
                                u.apply_time,
                                u.apply_userId,
                                u.review_content,
                                u.review_level,
                                u.review_manager,
                                u.review_time,
                                u.plan_status,
                                u.plan_conform,
                                rname = tt == null ? "" : tt.name
                            };

                if (userid != null) chain = chain.Where(n => n.apply_userId == userid);
                if (status != null)
                {
                    string[] arr = status.Split(',');
                    chain = chain.Where(m => arr.Contains(m.apply_status));
                }
                if (source != null) chain = chain.Where(n => n.apply_source.Contains(source));
                if (gname != null) chain = chain.Where(n => n.apply_gardenName.Contains(gname));
                if (reviewmanager != null)
                    chain = chain.Where(n => n.review_manager == reviewmanager);

                switch (orderBy == null ? null : orderBy.ToLower())
                {
                    case "createtime":
                        chain = ascending ? chain.OrderBy(n => n.apply_createTime) : chain.OrderByDescending(n => n.apply_createTime);
                        break;
                    default:
                        chain = ascending ? chain.OrderBy(n => n.apply_id) : chain.OrderByDescending(n => n.apply_id);
                        break;
                }

                if (limit != null)
                {
                    var envelope = new Envelope { Limit = limit, Page = page, Total = chain.Count() };
                    if (page != null) chain = chain.Skip((page.Value - 1) * limit.Value);
                    chain = chain.Take(limit.Value);

                    var entites = chain.ToList();
                    return Ok(entites, envelope);
                }
                else
                {
                    var entites = chain.ToList();
                    return Ok(entites);
                }
            }
        }

        [Route("{id:int}")]
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DesignApply.FirstOrDefault(p => id == p.apply_id);
                if (null == entity)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DesignApply entity)
        {
            using (ctx = new YwtDbContext())
            {
                entity.apply_createTime = DateTime.Now;
                entity.apply_status = ApplyStaus.WaitAuditing.ToString();
                ctx.DesignApply.Add(entity);
                ctx.SaveChanges();
                return Ok(entity);
            }
        }

        /// <summary>
        /// 后台修改科学环创请求
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]DesignApplyDto input)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DesignApply.FirstOrDefault(m => m.apply_id == input.apply_id);
                if (null == entity)
                {
                    return NotFound();
                }
                input.apply_status = ApplyStaus.WaitAuditing.ToString();
                Mapper.Map<DesignApplyDto, DesignApply>(input, entity);
                ctx.SaveChanges();
                return Ok(entity);
            }
        }

        /// <summary>
        /// 前台页面完善科学环创请求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route]
        public IHttpActionResult Put([FromBody]MoreDesignApplyDto input)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DesignApply.FirstOrDefault(m => m.apply_id == input.apply_id);
                if (null == entity)
                {
                    return NotFound();
                }
                Mapper.Map<MoreDesignApplyDto, DesignApply>(input, entity);
                ctx.SaveChanges();
                return Ok(entity);
            }
        }

        [Route("getapplyevent")]
        public IHttpActionResult GetDesignEvent()
        {
            using (ctx = new YwtDbContext())
            {
                var entitys = ctx.DesignEvent.AsQueryable().ToList();
                return Ok(entitys);
            }
        }

        [Route("PostDesignApplyReview")]
        [HttpPost]
        public IHttpActionResult PostDesignApplyReview([FromBody]ReviewDesignApplyDto input)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DesignApply.FirstOrDefault(m => m.apply_id == input.apply_id);
                if (entity == null)
                {
                    return NotFound();
                }
                input.review_time = DateTime.Now;
                Mapper.Map<ReviewDesignApplyDto, DesignApply>(input, entity);
                ctx.SaveChanges();
                return Ok(entity);
            }
        }

        #region 设计方案
        [Route("PostDesignPlan")]
        [HttpPost]
        public IHttpActionResult PostDesignPlan([FromBody]DesignPlan entity)
        {
            using (ctx = new YwtDbContext())
            {
                var enty = ctx.DesignApply.FirstOrDefault(m => m.apply_id == entity.ApplyId);
                enty.plan_status = false;   //上传方案后   方案状态变为未提交

                entity.CreateTime = DateTime.Now;
                entity.DesignFilePath = entity.DesignFilePath.Trim('"');
                ctx.DesignPlan.Add(entity);
                ctx.SaveChanges();
                return Ok(entity);
            }
        }

        [Route("GetDesignPlan")]
        [HttpGet]
        public IHttpActionResult GetDesignPlan(int applyid)
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.DesignPlan.Where(p => applyid == p.ApplyId);
                return Ok(list.ToList());
            }
        }

        [Route("ConfirmDesignApply")]
        [HttpPut]
        /// <summary>
        /// 需求确认
        /// </summary>
        /// <param name="applyid"></param>
        /// <returns></returns>
        public IHttpActionResult ConfirmDesignApply([FromBody]DesignApply entity)
        {
            using (ctx = new YwtDbContext())
            {
                var model = ctx.DesignApply.FirstOrDefault(m => m.apply_id == entity.apply_id);
                if (model == null)
                {
                    return NotFound();
                }

                model.apply_status = ApplyStaus.Designing.ToString();
                model.apply_plantime = entity.apply_plantime;
                ctx.SaveChanges();
                return Ok(entity);
            }
        }

        [Route("PostDesignPlanReview")]
        [HttpPost]
        /// <summary>
        /// 提交设计方案
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult PostDesignPlanReview([FromBody]DesignApply entity)
        {
            using (ctx = new YwtDbContext())
            {
                var model = ctx.DesignApply.FirstOrDefault(m => m.apply_id == entity.apply_id);
                if (model == null)
                    return NotFound();

                model.plan_status = true;
                ctx.SaveChanges();
                return Ok();
            }
        }
        #endregion

    }
}

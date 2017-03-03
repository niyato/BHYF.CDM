using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;

using CDM.Infrastructure.WebApi;
using BHYF.CDM.Models;
using AutoMapper;
using System.Text;

namespace BHYF.CDM.Controllers
{
    [RoutePrefix("v1/delivery")]
    public class DeliveryController : StemApiController
    {

        YwtDbContext ctx;

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryApply.Include("DeliveryApplyProducts").FirstOrDefault(m => m.Id == id);
                if (entity == null)
                    return NotFound();

                return Ok(Mapper.Map<DeliveryApplyDto>(entity));
            }
        }

        [Route]
        public IHttpActionResult Get(
            int? limit = null,
            int? page = null,
            string status=null,
            string date1=null,
            string date2=null,
            string memberid=null,
            string orderBy = null,
            bool ascending = true)
        {
            using (ctx = new YwtDbContext())
            {
                var chain = ctx.DeliveryApply.AsQueryable();

                if (status != null) chain = chain.Where(n => n.Status == status);
                if (memberid != null) chain = chain.Where(n => n.Memberid == memberid);
                if (date1 != null && date2 != null)
                {
                    DateTime d1 = Convert.ToDateTime(date1);
                    DateTime d2 = Convert.ToDateTime(date2);
                    chain=chain.Where(m => m.CreateTime > d1 && m.CreateTime < d2);
                }

                switch (orderBy == null ? null : orderBy.ToLower())
                {
                    case "id":
                        chain = ascending ? chain.OrderBy(p => p.Id) : chain.OrderByDescending(p => p.Id);
                        break;
                    default:
                        chain = chain.OrderByDescending(p => p.Id);
                        break;
                }

                if (limit != null)
                {
                    var envelope = new Envelope { Limit = limit, Page = page, Total = chain.Count() };
                    if (page != null) chain = chain.Skip((page.Value - 1) * limit.Value);
                    chain = chain.Take(limit.Value);
                    var entites = chain.ToList();
                    return Ok(entites.ToList(), envelope);
                }
                else
                {
                    var entites = chain.ToList();
                    return Ok(entites.ToList());
                }
            }
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DeliveryApply input)
        {
            using (ctx = new YwtDbContext())
            {
                input.Num = DateTime.Now.ToString("yyyyMMddHHmmss");
                input.Status = DeliveryApplyStatus.WaitAudit.ToString();
                input.CreateTime = DateTime.Now;
                input.LastUpdateTime = DateTime.Now;
                ctx.DeliveryApply.Add(input);
                ctx.SaveChanges();
                return Ok("");
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, DeliveryApplyPutDto input)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryApply.Include("DeliveryApplyProducts").FirstOrDefault(m => m.Id == id);

                if (null == entity)
                {
                    return NotFound();
                }

                ctx.DeliveryApplyProduct.RemoveRange(entity.DeliveryApplyProducts);
                ctx.SaveChanges();

                input.LastUpdateTime = DateTime.Now;
                Mapper.Map<DeliveryApplyPutDto, DeliveryApply>(input, entity);

                ctx.SaveChanges();
                return Ok(input);
            }
        }

        [HttpPost]
        [Route("PendingApplyInfo")]
        /// <summary>
        /// 流程审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IHttpActionResult PendingApplyInfo([FromBody]DeliveryApplyPendingDto input)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryApply.FirstOrDefault(m => m.Id == input.Id);
                entity.Img = input.Img;
                if (input.Content == "拒绝")
                {
                    entity.Status = DeliveryApplyStatus.Refuse.ToString();
                }
                else
                {
                    switch (input.Type)
                    {
                        //部们审批
                        case "department":
                            entity.DepartmentLeaderSign = input.Content;
                            entity.Status = DeliveryApplyStatus.Audited.ToString();
                            break;
                        //公司审批
                        case "company":
                            entity.CompanyLeaderSign = input.Content;
                            entity.BossSign = input.Content;
                            entity.Status = DeliveryApplyStatus.WaitSend.ToString();
                            break;
                        //工厂发货
                        case "factory":
                            entity.Status = DeliveryApplyStatus.WaitReceived.ToString();
                            break;
                    }
                }
                entity.LastUpdateTime = DateTime.Now;
                ctx.SaveChanges();
                return Ok(input);
            }
        }

        [HttpDelete]
        [Route("DeleteDeliveryApply")]
        public IHttpActionResult DeleteDeliveryApply(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryApply.Include("DeliveryApplyProducts").FirstOrDefault(m => m.Id == id);
                if (entity.Status == DeliveryApplyStatus.Refuse.ToString())
                {
                    ctx.DeliveryApply.Remove(entity);
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
                return Ok();
            }
        }

        #region  发货商品
        [HttpGet]
        [Route("GetDeliveryProduct")]
        public IHttpActionResult GetDeliveryProduct()
        {
            using (ctx = new YwtDbContext())
            {
                return Ok(ctx.DeliveryProduct.ToList());
            }
        }

        [HttpPost]
        [Route("CreateDeliveryProduct")]
        public IHttpActionResult CreateDeliveryProduct([FromBody]DeliveryProduct input)
        {
            using (ctx = new YwtDbContext())
            {
                input.LastUpdateTime = DateTime.Now;
                input.CreateTime = DateTime.Now;
                input.Status = 1;
                ctx.DeliveryProduct.Add(input);
                ctx.SaveChanges();
                return Ok();
            }
        }


        [HttpDelete]
        [Route("DeleteDeliveryProduct")]
        public IHttpActionResult DeleteDeliveryProduct(int pid)
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.DeliveryApplyProduct.Where(m => m.Pid == pid);
                if (list == null || list.Count() == 0)
                {
                    var entity = ctx.DeliveryProduct.FirstOrDefault(m => m.Pid == pid);
                    ctx.DeliveryProduct.Remove(entity);
                    ctx.SaveChanges();
                    return Ok("删除成功");
                }
                return Ok("删除失败,该商品已经在发货申请单中使用");
            }
        }

        #endregion

        #region 申请单商品
        [HttpDelete]
        [Route("DeleteDeliveryApplyProduct")]
        public IHttpActionResult DeleteDeliveryApplyProduct(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.DeliveryApplyProduct.FirstOrDefault(m => m.Id == id);
                ctx.DeliveryApplyProduct.Remove(list);
                ctx.SaveChanges();
                return Ok("删除成功");
            }
        }
        #endregion

        #region 发货单
        [HttpPost]
        [Route("PostDeliveryInvoice")]
        public IHttpActionResult PostDeliveryInvoice([FromBody]DeliveryInvoice input)
        {
            using (ctx = new YwtDbContext())
            {
                input.CreateTime = DateTime.Now;
                ctx.DeliveryInvoice.Add(input);
                var entity = ctx.DeliveryApply.FirstOrDefault(m => m.Id == input.ApplyId);
                entity.Status = DeliveryApplyStatus.WaitReceived.ToString();
                entity.LastUpdateTime = DateTime.Now;

                ctx.SaveChanges();
                return Ok(input);
            }
        }

        [HttpGet]
        [Route("GetDeliveryInvoice")]
        public IHttpActionResult GetDeliveryInvoice(int applyid)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryInvoice.FirstOrDefault(m => m.ApplyId == applyid);
                return Ok(entity);
            }
        }

        [HttpGet]
        [Route("GetDeliveryInvoice")]

        public IHttpActionResult GetDeliveryInvoice()
        {
            StringBuilder str = new StringBuilder();
            List<DeliveryInvoice> list =new List<DeliveryInvoice>();
            using (ctx = new YwtDbContext())
            {
                list=ctx.DeliveryInvoice.ToList();
                //return Ok(ctx.DeliveryInvoice.ToList());
            }
            foreach (var item in list)
            {
                str.Append("<option value='" + item.Id + "'>" + item.AgencyName + "</option>");
            }
            return Ok(str);
        }
        #endregion

        #region 收货回执单
        [HttpPost]
        [Route("PostDeliveryReceipt")]
        public IHttpActionResult PostDeliveryReceipt([FromBody]DeliveryReceipt input)
        {
            using (ctx = new YwtDbContext())
            {
                input.CreateTime = DateTime.Now;
                ctx.DeliveryReceipt.Add(input);

                var entity = ctx.DeliveryApply.FirstOrDefault(m => m.Id == input.ApplyId);
                entity.Status = DeliveryApplyStatus.Received.ToString();
                entity.LastUpdateTime = DateTime.Now;

                ctx.SaveChanges();
                return Ok(input);
            }
        }

        [HttpGet]
        [Route("GetDeliveryReceipt")]
        public IHttpActionResult GetDeliveryReceipt(int applyid)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryReceipt.FirstOrDefault(m => m.ApplyId == applyid);
                return Ok(entity);
            }
        }
        #endregion


        #region 角色菜单
        [HttpGet]
        [Route("GetDeliveryMenu")]
        public IHttpActionResult GetDeliveryMenu(int mgid)
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.DeliveryMenu.Where(m => m.Mgroupid == mgid);
                return Ok(list.ToList());
            }
        }

        [HttpGet]
        [Route("GetDeliveryMenuList")]
        public IHttpActionResult GetDeliveryMenuList()
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.DeliveryMenu.ToList();
                return Ok(list);
            }
        }

        [HttpPost]
        [Route("CreateDeliveryMenu")]
        public IHttpActionResult CreateDeliveryMenu([FromBody]DeliveryMenu input)
        {
            using (ctx = new YwtDbContext())
            {
                ctx.DeliveryMenu.Add(input);
                ctx.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        [Route("DeleteDeliveryMenu")]
        public IHttpActionResult DeleteDeliveryMenu(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryMenu.FirstOrDefault(m => m.Id == id);
                ctx.DeliveryMenu.Remove(entity);
                ctx.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("GetManageGroupList")]
        public IHttpActionResult GetManageGroupList()
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.ManageGroup.Where(m => m.status == 1);
                return Ok(list.ToList());
            }
        }

        [HttpGet]
        [Route("GetDeliveryPower")]
        public IHttpActionResult GetDeliveryPower(int mgid)
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.DeliveryPower.Where(m => m.Mgroupid == mgid);
                return Ok(list.ToList());
            }
        }

        [HttpGet]
        [Route("GetDeliveryPowerList")]
        public IHttpActionResult GetDeliveryPowerList()
        {
            using (ctx = new YwtDbContext())
            {
                var list = ctx.DeliveryPower.ToList();
                return Ok(list);
            }
        }

        [HttpPost]
        [Route("CreateDeliveryPower")]
        public IHttpActionResult CreateDeliveryPower([FromBody]DeliveryPower input)
        {
            using (ctx = new YwtDbContext())
            {
                ctx.DeliveryPower.Add(input);
                ctx.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        [Route("DeleteDeliveryPower")]
        public IHttpActionResult DeleteDeliveryPower(int id)
        {
            using (ctx = new YwtDbContext())
            {
                var entity = ctx.DeliveryPower.FirstOrDefault(m => m.Id == id);
                if (entity == null)
                {
                    return NotFound();
                }
                ctx.DeliveryPower.Remove(entity);
                ctx.SaveChanges();
                return Ok();
            }
        }

       
        
        #endregion

    }
}
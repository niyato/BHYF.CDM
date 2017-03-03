using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BHYF.CDM.Models;

using CDM.Infrastructure.WebApi;

namespace BHYF.CDM.Controllers
{
    [RoutePrefix("v1/manager")]
    public class ManagerController : StemApiController
    {
        private YwtDbContext db = new YwtDbContext();

        // GET api/Manager
        public IQueryable<PersonInfo> GetPersonInfo()
        {
            return db.PersonInfo;
        }

        /// <summary>
        /// 获取指定管理员用户组的成员信息
        /// </summary>
        /// <param name="mgroupid"></param>
        /// <returns></returns>
        [Route("GetPersonInfos")]
        public IHttpActionResult GetPersonInfos(int mgroupid)
        {
            var query = from u in db.Manager
                        join m in db.PersonInfo on u.memberid equals m.memberid
                        where m.mgroupid == mgroupid && m.status == 1
                        select new
                        {
                            u.memberid,
                            m.name
                        };
            return Ok(query.ToList());
        }

        [Route("GetApplyDesignManager")]
        public IHttpActionResult GetApplyDesignManager()
        {
            using (YwtDbContext db = new YwtDbContext())
            {
                var entity = db.ApplyDesignManager.FirstOrDefault();
                return Ok(entity);
            }
        }



        [Route("{id:int}")]
        // GET api/Manager/5
        [ResponseType(typeof(PersonInfo))]
        public async Task<IHttpActionResult> GetPersonInfo(long id)
        {
            PersonInfo personinfo = await db.PersonInfo.FindAsync(id);
            if (personinfo == null)
            {
                return NotFound();
            }

            return Ok(personinfo);
        }

        // PUT api/Manager/5
        public async Task<IHttpActionResult> PutPersonInfo(long id, PersonInfo personinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personinfo.ID)
            {
                return BadRequest();
            }

            db.Entry(personinfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Manager
        [ResponseType(typeof(PersonInfo))]
        public async Task<IHttpActionResult> PostPersonInfo(PersonInfo personinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonInfo.Add(personinfo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = personinfo.ID }, personinfo);
        }

        // DELETE api/Manager/5
        [ResponseType(typeof(PersonInfo))]
        public async Task<IHttpActionResult> DeletePersonInfo(long id)
        {
            PersonInfo personinfo = await db.PersonInfo.FindAsync(id);
            if (personinfo == null)
            {
                return NotFound();
            }

            db.PersonInfo.Remove(personinfo);
            await db.SaveChangesAsync();

            return Ok(personinfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonInfoExists(long id)
        {
            return db.PersonInfo.Count(e => e.ID == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web;
using System.IO;

using CDM.Infrastructure.WebApi;


namespace BHYF.CDM.Controllers
{
    /// <summary>
    /// 文件服务
    /// </summary>
    [RoutePrefix("v1/file")]
    public class FileController : StemApiController
    {
        string savePath = System.Web.HttpContext.Current.Server.MapPath("~/upload/applydesign/");
        string designplanpath = System.Web.HttpContext.Current.Server.MapPath("~/upload/designplan/");
        string activitypath = System.Web.HttpContext.Current.Server.MapPath("~/upload/activity/");
        string deliverypath = System.Web.HttpContext.Current.Server.MapPath("~/upload/delivery/");

        [HttpPost]
        [Route("upload")]
        public IHttpActionResult Upload(string aid)
        {
            //上传文件保存路径      
            savePath = savePath + aid;
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            //提供对客户端上载文件的访问
            HttpFileCollection uploadFiles = System.Web.HttpContext.Current.Request.Files;
            string strimg = "";
            try
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    System.Web.HttpPostedFile postedFile = uploadFiles[i];
                    string fileName = postedFile.FileName;//完整的路径
                    fileName = System.IO.Path.GetFileName(postedFile.FileName); //获取到名称
                    string fileExtension = System.IO.Path.GetExtension(fileName);//文件的扩展名称
                    string type = fileName.Substring(fileName.LastIndexOf("."));    //类型  
                    if (uploadFiles[i].ContentLength > 0)
                    {
                        string curfilename = Guid.NewGuid().ToString() + type;
                        uploadFiles[i].SaveAs(savePath + "/" + curfilename);
                        strimg += curfilename + ",";
                    }
                }
            }
            catch (System.Exception Ex)
            {
                return null;
            }

            return Ok(strimg.TrimEnd(','));
        }

        [Route("GetFiles")]
        public IHttpActionResult GetFiles(int id)
        {
            string filepath = savePath + id.ToString();
            if (Directory.Exists(filepath))
            {
                var files = Directory.GetFiles(filepath);
                string strimgs = "";
                foreach (var file in files)
                    strimgs += System.IO.Path.GetFileName(file) + ",";
                return Ok(strimgs.TrimEnd(','));
            }
            else
                return Ok("");
        }


        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(string id, string fid)
        {
            try
            {
                string filepath = savePath + id + "/" + fid;
                if (File.Exists(filepath))
                    File.Delete(filepath);
                return Ok();
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(savePath + "test1.txt", ex.Message, System.Text.Encoding.UTF8);
                return Ok(ex.Message);
            }
        }


        [HttpPost]
        [Route("upload")]
        public string Upload(string aid, string filetype)
        {
            string cururl = "";
            switch (filetype)
            {
                case "img":
                    cururl = savePath;
                    break;
                case "file":
                    cururl = designplanpath;
                    break;
            }

            //上传文件保存路径      
            cururl = cururl + aid;
            if (!Directory.Exists(cururl))
                Directory.CreateDirectory(cururl);

            //提供对客户端上载文件的访问
            HttpFileCollection uploadFiles = System.Web.HttpContext.Current.Request.Files;
            string strimg = "";
            try
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    System.Web.HttpPostedFile postedFile = uploadFiles[i];
                    string fileName = postedFile.FileName;//完整的路径
                    fileName = System.IO.Path.GetFileName(postedFile.FileName); //获取到名称
                    string fileExtension = System.IO.Path.GetExtension(fileName);//文件的扩展名称
                    string type = fileName.Substring(fileName.LastIndexOf("."));    //类型  
                    if (uploadFiles[i].ContentLength > 0)
                    {
                        string curfilename = Guid.NewGuid().ToString() + type;
                        uploadFiles[i].SaveAs(cururl + "/" + curfilename);
                        strimg = curfilename;
                    }
                }
            }
            catch (System.Exception Ex)
            {
                return null;
            }

            return strimg;
        }

        /// <summary>
        /// 小学霸头像
        /// </summary>
        /// <param name="activityname">活动名称</param>
        /// <returns>图片地址</returns>
        [HttpPost]
        [Route("UploadActivity")]
        public IHttpActionResult UploadActivity(string activityname, bool ismultiple = false)
        {
            string cururl = "";
            switch (activityname)
            {
                case "xueba":
                    cururl = activitypath + "xueba/";
                    break;
                default:
                    break;
            }

            //上传文件保存路径      
            if (!Directory.Exists(cururl))
                Directory.CreateDirectory(cururl);

            //提供对客户端上载文件的访问
            HttpFileCollection uploadFiles = System.Web.HttpContext.Current.Request.Files;
            string strimg = "";
            try
            {
                if (ismultiple)
                {
                    for (int i = 0; i < uploadFiles.Count; i++)
                    {
                        System.Web.HttpPostedFile postedFile = uploadFiles[i];
                        string fileName = postedFile.FileName;//完整的路径
                        fileName = System.IO.Path.GetFileName(postedFile.FileName); //获取到名称
                        string fileExtension = System.IO.Path.GetExtension(fileName);//文件的扩展名称
                        string type = fileName.Substring(fileName.LastIndexOf("."));    //类型  
                        if (uploadFiles[i].ContentLength > 0)
                        {
                            string curfilename = Guid.NewGuid().ToString() + type;
                            uploadFiles[i].SaveAs(cururl + "/" + curfilename);
                            strimg += curfilename + ",";
                        }
                    }
                }
                else
                {

                    System.Web.HttpPostedFile postedFile = uploadFiles[0];
                    string fileName = postedFile.FileName;//完整的路径
                    fileName = System.IO.Path.GetFileName(postedFile.FileName); //获取到名称
                    string fileExtension = System.IO.Path.GetExtension(fileName);//文件的扩展名称
                    string type = fileName.Substring(fileName.LastIndexOf("."));    //类型  
                    if (uploadFiles[0].ContentLength > 0)
                    {
                        string curfilename = Guid.NewGuid().ToString() + type;
                        uploadFiles[0].SaveAs(cururl + "/" + curfilename);
                        strimg = curfilename;
                    }
                }
            }
            catch (System.Exception Ex)
            {
                return null;
            }
            return Ok(strimg.TrimEnd(','));


        }

        [HttpPost]
        [Route("UploadDeliveryImg")]
        public IHttpActionResult UploadDeliveryImg(int id)
        {
            string cururl = deliverypath + id.ToString() + "/";
            //上传文件保存路径      
            if (!Directory.Exists(cururl))
                Directory.CreateDirectory(cururl);

            //提供对客户端上载文件的访问
            HttpFileCollection uploadFiles = System.Web.HttpContext.Current.Request.Files;
            System.Web.HttpPostedFile postedFile = uploadFiles[0];
            string fileName = postedFile.FileName;//完整的路径
            fileName = System.IO.Path.GetFileName(postedFile.FileName); //获取到名称
            string fileExtension = System.IO.Path.GetExtension(fileName);//文件的扩展名称

            string curfilename = string.Empty;
            if (uploadFiles[0].ContentLength > 0)
            {
                curfilename = Guid.NewGuid().ToString() + fileExtension;
                uploadFiles[0].SaveAs(cururl + "/" + curfilename);
            }
            return Ok(curfilename);
        }

    }
}
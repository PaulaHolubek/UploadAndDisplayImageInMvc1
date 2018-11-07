using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadAndDisplayImageInMvc.Models;
using UploadAndDisplayImageInMvc.Repositories;
using UploadAndDisplayImageInMvc.ViewModel;

namespace UploadAndDisplayImageInMvc.Controllers
{
    [RoutePrefix("Content")]
    [ValidateInput(false)]
    public class ContentController : Controller
    {
        private DBContext db = new DBContext();
        /// <summary>
        /// Retrive content from database 
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        [HttpGet]
        public ActionResult Index()
        {
            var content = db.Contents.Select(s => new
            {
                s.ID,
                s.Title,
                s.Image,
                s.Image1,
                s.Contents,
                s.Description
            });

            List<ContentViewModel> contentModel = content.Select(item => new ContentViewModel()
            {
                ID = item.ID,
                Title = item.Title,
                Image = item.Image,
                Image1 = item.Image1,
                Description = item.Description,
                Contents = item.Contents
            }).ToList();
            return View(contentModel);
        }

        /// <summary>
        /// Retrive Image from database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public ActionResult RetrieveImage1(int id)
        {
            byte[] cover1 = GetImageFromDataBase1(id);
            if (cover1 != null)
            {
                return File(cover1, "image/jpg");
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Contents where temp.ID == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        public byte[] GetImageFromDataBase1(int Id)
        {
            var qq = from temp in db.Contents where temp.ID == Id select temp.Image1;
            byte[] cover1 = qq.First();
            return cover1;
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Save content and images
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(ContentViewModel model)
        {
            ContentRepository service = new ContentRepository();

            HttpPostedFileBase file = Request.Files["ImageData"];
            HttpPostedFileBase file1 = Request.Files["ImageData1"];
            
            int i = service.UploadImageInDataBase(file, file1, model);
            
            if (i == 1)
            {
                    return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
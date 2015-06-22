using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using perseptus.PresentationEntity;
using perseptus.Models;
using perseptus.Services;
using Ninject;

namespace perseptus.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _imagesRepository;

        public HomeController(IFileService repo) 
        {
            _imagesRepository = repo;
        }

        public ActionResult Index()
        {
            var model = new ImagesListPE
            {
                Images = _imagesRepository.GetFileNames()
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private Stream ConvertBase64ToByte(string source)
        {
            string base64 = source.Substring(source.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] chartData = Convert.FromBase64String(base64);

            return new MemoryStream(chartData);
        }

        [HttpPost]
        public ActionResult DoImportJSON(List<String> jsonData)
        {
            bool isUploaded = true;
            String message;
            try
            {
                String name = _imagesRepository.GetFileNames().Count.ToString() + ".jpg";

                if (!String.IsNullOrEmpty(jsonData[0]) &&
                    _imagesRepository.AddFile(ConvertBase64ToByte(jsonData[0]), name))
                {
                    message = "Изображение успешно загружено!";
                }
                else
                {
                    message = "Ошибка загрузки файла!";
                    isUploaded = false;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Произошла ошибка. Если данная ошибка будет повторяться, обратитесь к администратору.");
                isUploaded = false;
                message = "Произошла неизвестная ошибка, обратитесь к системному администратору!";
            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }
    }    
}
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using perseptus.PresentationEntity;
using perseptus.ViewInterfaces;
using perseptus.Models;
using Ninject;

namespace perseptus.Controllers
{
    public class FileSaver
    {
        static public void SaveImage(Stream data)
        {
            const string dir = @"C:\data";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var file = File.Create(dir + @"\" + @"res.jpg");
            data.CopyTo(file);
            file.Close();
        }
    }

    public class HomeController : Controller
    {
        private readonly IImageModel _imagesRepository;

        public HomeController(IImageModel repo)
        {
            _imagesRepository = repo;
        }

        public ActionResult Index()
        {
            var model = new ImagesListPE
            {
                Images = _imagesRepository.GetAllImages()
            };

            return View(model);
        }

        public ActionResult GetImage(int id)
        {
            var model = new ImagesListPE
            {
                Images = _imagesRepository.GetAllImages()
            };
            return Json(new { isUploaded = 0, message = 0 }, "text/html");
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
            string message = "";
            try
            {
                if (!String.IsNullOrEmpty(jsonData[0]))
                {
                    message = "Файл успешно загружен!";
                    FileSaver.SaveImage(ConvertBase64ToByte(jsonData[0]));
                }
                else
                {
                    message = "Файл не указан!";
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
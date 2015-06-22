using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using perseptus.Services;

namespace perseptus.Controllers
{
    public class ImageController : Controller
    {
        private readonly perseptus.Services.IFileService _imagesRepository;

        public ImageController(IFileService repo)
        {
            _imagesRepository = repo;
        }

        public FileResult Index(String id)
        {
            Stream imgStream = _imagesRepository.GetFile(id);
            imgStream.Position = 0;
            return File(imgStream, "image/jpeg");
        }
    }
}
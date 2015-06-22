using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using perseptus.Services;
using perseptus.PresentationEntity;

namespace perseptus.Controllers
{
    public class SearchController : Controller
    {
        private readonly perseptus.Services.IFileService _imagesRepository;

        public SearchController(IFileService repo)
        {
            _imagesRepository = repo;
        }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Find(String name)
        {
            var model = new ComparedImagesListPE
            {
                Images = _imagesRepository.GetSimilarImages(name)
            };

            return View(model);
        }
    }
}
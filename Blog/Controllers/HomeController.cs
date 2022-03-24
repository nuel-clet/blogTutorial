using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data.Repository;
using Microsoft.Extensions.FileProviders;
using Blog.Data.FileManager;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _filemanager;

        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _filemanager = fileManager;
        }
        public IActionResult Index(string category) =>
            View(string.IsNullOrEmpty(category) ?
                _repo.GetAllPost() :
                _repo.GetAllPost(category));
        

        //public IActionResult Index(string category)
        //{
        //    var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPost() : _repo.GetAllPost(category);
        //    //boolean ? true : false;        1=1  ? run : ignore;
        //    return View(posts);
        //}


        public IActionResult Post(int id) =>
            View(_repo.GetPost(id));
        
        //public IActionResult Post(int id)
        //{
        //    var post = _repo.GetPost(id);
        //    return View(post);
        //}


        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image) =>
            new FileStreamResult(_filemanager.ImageStream(image),
                $"image/{ image.Substring(image.LastIndexOf(".") + 1)}");
        


        //[HttpGet("/Image/{image}")]
        //public IActionResult Image(string image)
        //{
        //    var mime = image.Substring(image.LastIndexOf(".") + 1);
        //    return new FileStreamResult(_filemanager.ImageStream(image), $"image/{mime}");
        //}
    }
}

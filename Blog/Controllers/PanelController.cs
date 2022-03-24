﻿using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }        

        public IActionResult Index()
        {
            var posts = _repo.GetAllPost();
            return View(posts);
        }        

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                    {
                        ID = post.ID,
                        Title = post.Title,
                        Body = post.Body,
                        CurrentImage = post.Image,
                        Description = post.Description,
                        Tags = post.Tags,
                        Category = post.Category

    });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                ID = vm.ID,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Tags = vm.Tags,
                Category = vm.Category
            };
            if(vm.Image == null)
            {
                post.Image = vm.CurrentImage;
            }
            else
            {
                if (!string.IsNullOrEmpty(vm.CurrentImage))
                    _fileManager.RemoveImage(vm.CurrentImage);

                post.Image = await _fileManager.SaveImage(vm.Image);
            }

            if (post.ID > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangeAsync())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(post);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangeAsync();
            return RedirectToAction("Index");
        }

    }
}

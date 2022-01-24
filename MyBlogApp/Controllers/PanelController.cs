using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Data.FileManager;
using MyBlogApp.Data.Repository;
using MyBlogApp.Models;
using MyBlogApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var post = new PostViewModel();
            if (id != null)
            {
                var postInfo = _repo.GetPost((int)id);
                post.Id = postInfo.Id;
                post.Title = postInfo.Title;
                post.Body = postInfo.Body;
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Body = vm.Body,
                Title = vm.Title,
                Id = vm.Id,
                Image = await _fileManager.SaveImage(vm.Image)
            };
            if (post.Id == 0)
                _repo.AddPost(post);
            else
                _repo.UpdatePost(post);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Data.Repository;
using MyBlogApp.Models;
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
        public PanelController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var post = new Post();
            if (id != null)
            {
                post = _repo.GetPost((int)id);
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
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


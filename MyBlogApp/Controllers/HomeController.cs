using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Data.Repository;
using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        public HomeController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var post = new Post();
            if(id != null)
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

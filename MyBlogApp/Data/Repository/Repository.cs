using MyBlogApp.Data;
using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            var posts = _context.Posts.ToList();
            return posts;
        }

        public Post GetPost(int id)
        {
            var post = _context.Posts.FirstOrDefault(_post => _post.Id == id);
            return post;
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}

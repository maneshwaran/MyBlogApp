using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);

        List<Post> GetAllPosts();

        void AddPost(Post post);

        void UpdatePost(Post post);

        void RemovePost(int id);

        Task<bool> SaveChangesAsync();
    }
}

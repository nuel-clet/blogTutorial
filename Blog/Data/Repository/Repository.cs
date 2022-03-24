using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {

        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p => p.ID == id);
        }

        public List<Post> GetAllPost()
        {
            return _ctx.Posts.ToList();
        }
        public List<Post> GetAllPost(string category)
        {
            return _ctx.Posts
                .Where(post => post.Category.ToLower().Equals(category.ToLower())).ToList();
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
            
        }
       
        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

        public async Task<bool> SaveChangeAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        
    }
}

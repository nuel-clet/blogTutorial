using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
namespace Blog.Data.Repository
{
    public interface IRepository 
    {
        Post GetPost(int id);
        List<Post> GetAllPost();
        List<Post> GetAllPost(string category);
        void AddPost(Post post);
        void RemovePost(int id);
        void UpdatePost(Post post);

        Task<bool> SaveChangeAsync();


    }
}

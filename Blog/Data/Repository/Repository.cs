using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _ctx.Posts.FirstOrDefault(p=>p.Id==id);
        }
        public List<Post> GetAllPost()
        {
            return _ctx.Posts.ToList();
        }
       public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
           
        }
        public void RemovePosts(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }
        public void UpdatePosts(Post post)
        {
            _ctx.Posts.Update(post);
        }
        public async Task<bool> SaveChangesAsync()
        {
            if(await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}

using BlogAppWASM.Services;
using BlogAppWASM.Shared;
using BlogAppWASM.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAppWASM.Data
{
    public class BlogRepository : IBlogRepository
    {
        public event Action? OnStateChanged;
        private readonly IBlogDbContextFactory _dbContextFactory;
        private readonly BlogStateService _blogStateService;

        public BlogRepository(IBlogDbContextFactory dbContextFactory, BlogStateService stateService)
        {
            _dbContextFactory = dbContextFactory;
            _blogStateService = stateService;
        }

        public async Task AddPost(string title, string content)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var blogPost = new BlogPost { Title = title, Content = content };
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            _blogStateService.NotifyStateChanges();
        }

        public async Task<List<BlogPost>> GetPosts()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetPost(int id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.BlogPosts.FirstAsync(x => x.Id == id);
        }

        public async Task UpdatePost(BlogPost post)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var existingPost = await dbContext.BlogPosts.FirstAsync(x => x.Id == post.Id);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                dbContext.Entry(existingPost).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }

            _blogStateService.NotifyStateChanges();
        }

        public async Task DeletePost(int id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var post = await dbContext.BlogPosts.FirstAsync(x => x.Id == id);
            if (post != null)
            {
                dbContext.BlogPosts.Remove(post);
                await dbContext.SaveChangesAsync();
            }

            _blogStateService.NotifyStateChanges();
        }
    }
}

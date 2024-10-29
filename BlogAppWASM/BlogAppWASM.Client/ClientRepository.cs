using BlogAppWASM.Shared;
using BlogAppWASM.Shared.Models;
using System.Net.Http.Json;

namespace BlogAppWASM.Client
{
    public class ClientRepository : IBlogRepository
    {
        private readonly HttpClient _httpClient;
        public event Action? OnStateChanged;

        public ClientRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BlogPost> GetPost(int id) =>
            (await _httpClient.GetFromJsonAsync<BlogPost>($"/api/posts/{id}")
               ?? throw new InvalidOperationException($"Failed to load post with id {id}."));

        public async Task<List<BlogPost>> GetPosts()
        {
            return await _httpClient.GetFromJsonAsync<List<BlogPost>>("/api/posts")
               ?? throw new InvalidOperationException("Failed to load posts");
        }

        public Task UpdatePost(BlogPost post) =>
                _httpClient.PutAsJsonAsync($"/api/posts/{post.Id}", post);

        public Task AddPost(string title, string content)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(int id)
        {
            throw new NotImplementedException();
        }
    }
}

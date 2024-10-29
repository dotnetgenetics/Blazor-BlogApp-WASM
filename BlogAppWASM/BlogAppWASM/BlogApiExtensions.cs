using BlogAppWASM.Shared;
using BlogAppWASM.Shared.Models;

namespace BlogAppWASM
{
    /// <summary>
    /// Client-side Editing 
    /// </summary>
    public static class BlogApiExtensions
    {
        public static WebApplication MapBlogApi(this WebApplication app)
        {
            app.MapGet("/api/posts/", async (IBlogRepository repository) => await repository.GetPosts());

            app.MapGet("/api/posts/{id}", async (IBlogRepository repository, int id) => await repository.GetPost(id));

            app.MapPost("/api/posts/", async (IBlogRepository repository, BlogPost post) => await repository.AddPost(post.Title, post.Content));

            app.MapPut("/api/posts/{id}", async (IBlogRepository repository, int id, BlogPost post) =>
            {
                if (id != post.Id)
                    throw new InvalidOperationException("Id mismatch");

                await repository.UpdatePost(post);
                return post;
            });

            app.MapDelete("/api/posts/{id}", async (IBlogRepository repository, int id) =>
            {
                await repository.DeletePost(id);
                return Results.NoContent();
            });

            return app;
        }
    }
}

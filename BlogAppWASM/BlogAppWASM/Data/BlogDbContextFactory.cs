namespace BlogAppWASM.Data
{
    public interface IBlogDbContextFactory
    {
        BlogDbContext CreateDbContext();
    }

    public class BlogDbContextFactory : IBlogDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BlogDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;                
        }

        public BlogDbContext CreateDbContext()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<BlogDbContext>();
        }
    }
}

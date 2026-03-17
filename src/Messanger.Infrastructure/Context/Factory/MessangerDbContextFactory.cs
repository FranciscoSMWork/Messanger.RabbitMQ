using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Messanger.Infrastructure.Context.Factory;

public class MessangerDbContextFactory : IDesignTimeDbContextFactory<MessangerDbContext>
{
    public MessangerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<MessangerDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new MessangerDbContext(optionsBuilder.Options);
    }
}

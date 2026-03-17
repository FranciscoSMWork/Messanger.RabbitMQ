
using Messanger.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Messanger.Infrastructure.Context;

public class MessangerDbContext : IdentityDbContext<User>
{
    public MessangerDbContext(DbContextOptions<MessangerDbContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(MessangerDbContext).Assembly);
    }
}

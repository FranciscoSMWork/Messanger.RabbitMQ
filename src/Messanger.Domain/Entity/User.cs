
using Microsoft.AspNetCore.Identity;

namespace Messanger.Domain.Entity;

public class User : IdentityUser
{
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

using Microsoft.AspNetCore.Identity;

namespace api.Entities;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}
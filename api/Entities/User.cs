using Microsoft.AspNetCore.Identity;

namespace api.Entities;

public class User : IdentityUser<int>
{
    public string KnownAs { get; set; }
    public DateTime Joined { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public DateOnly DateOfBirth { get; set; }
    public string Bio { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<Photo> Photos { get; set; } = new();

    public List<UserLike> LikedByUsers { get; set; }
    public List<UserLike> LikedUsers { get; set; }

    public List<Message> MessagesSent { get; set; }
    public List<Message> MessagesReceived { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
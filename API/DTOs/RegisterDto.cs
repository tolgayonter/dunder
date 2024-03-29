using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class RegisterDto
{
    [Required] public string Username { get; set; }
    [Required] public string KnownAs { get; set; }
    [Required] public DateOnly? DateOfBirth { get; set; }
    [Required] public string City { get; set; }
    [Required] public string Country { get; set; }

    [Required]
    [StringLength(14, MinimumLength = 4)]
    public string Password { get; set; }
}
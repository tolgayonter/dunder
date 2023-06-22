namespace API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PhotoUrl { get; set; }
    public int Age { get; set; }
    public string KnownAs { get; set; }
    public DateTime Joined { get; set; }
    public DateTime LastActive { get; set; }
    public string Bio { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<PhotoDto> Photos { get; set; }
}
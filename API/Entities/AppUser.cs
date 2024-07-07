using API.Extensions;

namespace API.Entities;
public class AppUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }

    public Byte[]? PasswordHash { get; set; }

    public Byte[]? PasswordSalt { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string KnownAs { get; set; }

    public DateTime LastTimeActive { get; set; }=DateTime.Now;

    public DateTime Created {get; set;}=DateTime.Now;

    public string Introduction {get; set;}

    public string LookingFor {get; set;}

    public string Interests {get; set;}

    public string City { get; set; }

    public string Country { get; set; }

    public List<Photo> Photos {get; set;} = new List<Photo>();

    // public int Age()
    // {
    //     return DateTimeExtensions.CalculateAge(DateOfBirth);
    // }
}
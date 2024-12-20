using Microsoft.AspNetCore.Identity;

namespace group4.Models;

public class User : IdentityUser
{
    public int UserId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Phone { get; set; }
}

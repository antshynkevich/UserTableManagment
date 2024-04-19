using Microsoft.AspNetCore.Identity;

namespace UserTableDataLayer;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime LastLoginTime { get; set; }

}

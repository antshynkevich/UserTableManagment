using Microsoft.AspNetCore.Identity;

namespace UserTableDataLayer;

public class User : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; }
    [PersonalData]
    public string LastName { get; set; }
    [PersonalData]
    public DateTime LastLoginTime { get; set; }
    [PersonalData]
    public DateTime Registration { get; set; }
    [PersonalData]
    public bool IsActive { get; set; }
}

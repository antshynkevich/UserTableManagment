using UserTableDataLayer;

namespace UserTableManagment.Models;

public class UserViewModel
{
    public string Id { get; set; }
    public string UserFullName { get; set; }
    public string Email { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime Registration { get; set; }
    public string IsActive { get; set; }
    public bool IsSelected { get; set; }

    public static UserViewModel MapUserToView(User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            UserFullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email ?? "",
            LastLogin = user.LastLoginTime,
            Registration = user.Registration,
            IsActive = user.IsActive ? "Active" : "Blocked"
        };
    }
}

using UserTableDataLayer;

namespace UserTableManagment.Models;

public class UserViewModel
{
    public string Id { get; set; }
    public string UserFullName { get; set; }
    public string Email { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime Registration { get; set; }
    public bool IsActive { get; set; }

    public static UserViewModel MapUserToView(User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            UserFullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            LastLogin = user.LastLoginTime,
            Registration = user.Registration
        };
    }
}

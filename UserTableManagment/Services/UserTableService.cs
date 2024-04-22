using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserTableDataLayer;
using UserTableManagment.Models;

namespace UserTableManagment.Services;

public class UserTableService
{
    private readonly UserManager<User> _userManager;

    public UserTableService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task ChangeBlockingStatusAsync(IEnumerable<UserViewModel> users, bool changingActivityFlag)
    {
        var selectedUsers = users.Where(x => x.IsSelected);

        foreach (var userView in selectedUsers)
        {
            var user = await _userManager.FindByIdAsync(userView.Id);
            if (user != null)
            {
                user.IsActive = changingActivityFlag;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}

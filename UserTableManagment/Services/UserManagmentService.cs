using Microsoft.AspNetCore.Identity;
using UserTableDataLayer;
using UserTableManagment.Models;

namespace UserTableManagment.Services;

public class UserManagmentService
{
    public async Task ChangeBlockingStatus(IEnumerable<UserViewModel> users, UserManager<User> userManager, bool changingActivityValue)
    {
        var selectedUsers = users.Where(x => x.IsSelected);

        foreach (var userView in selectedUsers)
        {
            var user = await userManager.FindByIdAsync(userView.Id);
            if (user != null)
            {
                user.IsActive = changingActivityValue;
                await userManager.UpdateAsync(user);
            }
        }
    }
}

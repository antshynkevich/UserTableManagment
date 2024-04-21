using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserTableDataLayer;

namespace UserTableManagment.Authorization;

public class IsNotBlockedUserHandler : AuthorizationHandler<IsNotBlockedUser>
{
    readonly UserManager<User> _userManager;

    public IsNotBlockedUserHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        IsNotBlockedUser requirement)
    {
        var userId = _userManager.GetUserId(context.User);
        if (userId != null)
        {
            var userFromDatabase = await _userManager.FindByIdAsync(userId);
            if (userFromDatabase != null && userFromDatabase.IsActive)
            {
                context.Succeed(requirement);
            }
        }
    }
}

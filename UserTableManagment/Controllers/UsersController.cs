using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserTableDataLayer;
using UserTableManagment.Models;
using UserTableManagment.Services;

namespace UserTableManagment.Controllers;

public class UsersController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserTableService _userService;

    public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, UserTableService userTableService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userTableService;
    }

    [Authorize("ActiveUser")]
    public IActionResult Index()
    {
        var userViews = _userManager.Users.Select(UserViewModel.MapUserToView).ToList();
        return View(userViews);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSelectedAsync(IEnumerable<UserViewModel> users)
    {
        var selectedUsers = users.Where(x => x.IsSelected);

        foreach (var userView in selectedUsers)
        {
            var user = await _userManager.FindByIdAsync(userView.Id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        if (selectedUsers.Any(x => x.Id == _userManager.GetUserId(User)))
        {
            await _signInManager.SignOutAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> BlockSelectedAsync(IEnumerable<UserViewModel> users)
    {
        await _userService.ChangeBlockingStatusAsync(users, false);
        if (users.Where(x => x.IsSelected).Any(x => x.Id == _userManager.GetUserId(User)))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UnblockSelectedAsync(IEnumerable<UserViewModel> users)
    {
        await _userService.ChangeBlockingStatusAsync(users, true);
        return RedirectToAction(nameof(Index));
    }
}

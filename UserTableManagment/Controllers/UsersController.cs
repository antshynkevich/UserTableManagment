using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserTableDataLayer;
using UserTableManagment.Models;

namespace UserTableManagment.Controllers;

public class UsersController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        var userViews = _userManager.Users.Select(UserViewModel.MapUserToView).ToList();
        return View(userViews);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSelectedAsync(IEnumerable<UserViewModel> model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        var selectedUsers = model.Where(x => x.IsSelected);
        var userForLoggingOut = await _userManager.GetUserAsync(User);
        if (userForLoggingOut != null && selectedUsers.Any(x => x.Id == userForLoggingOut.Id))
        {
            await _signInManager.SignOutAsync();
        }

        foreach (var item in selectedUsers)
        {
            var user = await _userManager.FindByIdAsync(item.Id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult BlockSelected(IEnumerable<UserViewModel> model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        // TODO: add code
        throw new NotImplementedException();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult UnblockSelected(IEnumerable<UserViewModel> model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        // TODO: add code
        throw new NotImplementedException();
        return RedirectToAction(nameof(Index));
    }
}

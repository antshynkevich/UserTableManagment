using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserTableDataLayer;
using UserTableManagment.Models;

namespace UserTableManagment.Pages;

public class UsersModel : PageModel
{
    private readonly UserManager<User> _userManager;

    public UsersModel(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public List<UserViewModel> Users { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        Users = users.Select(UserViewModel.MapUserToView).ToList();
        return Page();
    }
}

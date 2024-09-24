using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ExpenseTracker.Areas.Identity.Pages.Account
{
    public class ProfileModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}

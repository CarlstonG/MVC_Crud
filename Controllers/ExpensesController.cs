using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class ExpensesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ExpensesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Expenses
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var expenses = await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();
        return View(expenses);
    }

    // GET: Expenses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Expenses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Description,Amount,Date")] Expense expense)
    {
        if (ModelState.IsValid)
        {
            expense.UserId = _userManager.GetUserId(User);  // Assign current user ID
            _context.Add(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(expense);
    }
}

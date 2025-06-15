using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Login"); // Перенаправляем на вход, если пользователь не авторизован
            }

            var userResults = await _context.UserTestResults
                .Include(r => r.Course)
                .Where(r => r.UserId == username)
                .ToListAsync();

            return View(userResults);
        }
    }
} 
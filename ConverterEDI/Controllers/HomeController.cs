using ConverterEDI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConverterEDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            List<IdentityUser> users = new List<IdentityUser>();
            var user = await _userManager.GetUserAsync(User);
            if (user != null && (user.UserName == "j.klebucki@ajksoftware.pl"
                || user.UserName == "jaroslaw.klebucki@citronex.pl"
                || user.UserName == "j.klebucki@citronex.pl"))
                users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            if (result == IdentityResult.Success)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUserEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result == IdentityResult.Success)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NichiOnlineTest.API.Data;
using NichiOnlineTest.API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace NichiOnlineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpPost]
        [AllowAnonymous]
        //  [ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.MobileNumber, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.MobileNumber);
                    var role = await _userManager.GetRolesAsync(user);
                    _logger.LogInformation("User logged in.");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("NichiInSoftwareSolutions");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.FirstName+" "+user.LastName),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
                        }),

                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    // user.Token = ;

                    TokenViewModel tokenModel = new TokenViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.UserName,
                        Role = role?.FirstOrDefault(),
                        Token = tokenHandler.WriteToken(token),
                        Expiry = DateTime.UtcNow.AddDays(7)
                    };

                    return Ok(tokenModel);
                    // return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid mobile number and password");

                }
            }

            var errors = ModelState.Values.SelectMany(m => m.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

            // If we got this far, something failed, redisplay form
            return BadRequest(errors);
        }

        [HttpGet]
        [Authorize]
        [Route("Home")]
        public ActionResult<IEnumerable<string>> Home()
        {
            return new string[] { "Welcome", "User" };
        }

        [HttpPost]
        [AllowAnonymous]
        //   [ValidateAntiForgeryToken]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.MobileNumber, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    try
                    {
                        var userRole = await _userManager.FindByIdAsync(user.Id);
                        await _userManager.AddToRoleAsync(userRole, "User");
                    }
                    catch (System.Exception ex)
                    {
                        throw;
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(result);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            var errors = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
            return BadRequest(Json(errors));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            //return RedirectToAction(nameof(HomeController.Index), "Home");
            return null;
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(AccountsController.Register), "Home");
            }
        }

        #endregion
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NichiOnlineTest.API.Data;
using NichiOnlineTest.API.Filters;
using NichiOnlineTest.API.Helpers;
using NichiOnlineTest.Common;
using NichiOnlineTest.View.Logic;
using NichiOnlineTest.View.Models;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace NichiOnlineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AddHeaderAttribute(true)]
    public class ResetUserController : BaseController
    {
        /// <summary>
        /// _userManager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// _signInManager
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// ResetUserController(Constructor)
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public ResetUserController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// GetResetUserDataByMobileNo
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetUserDataByMobileNo")]
        public IActionResult GetResetUserDataByMobileNo(string mobileNo)
        {
            try
            {
                var response = ResetUserViewLogic.GetResetUserDataByMobileNo(mobileNo);
                return Ok(new ApiResponse<ResetUserViewModel>()
                {
                    StatusCode = 200,
                    Result = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    StatusCode = 400,
                    ErrorMessage = Messages.OT_008
                });
            }
        }

        /// <summary>
        /// /ResetUserPassword
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetUserPassword")]
        public async Task<IActionResult> ResetUserPassword(ResetUserViewModel userViewModel)
        {
            var response = ResetUserViewLogic.GetResetUserDataByMobileNo(userViewModel.MobileNumber);
            var user = await _userManager.FindByIdAsync(response.Id);
            user.UserName = response.MobileNumber;
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code, userViewModel.Password);
            if (!result.Succeeded)
            {
                AddErrors(result);
                var errors = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return BadRequest(Ok(errors));
            }
            return Ok(result);
        }

        /// <summary>
        /// AddErrors
        /// </summary>
        /// <param name="result"></param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
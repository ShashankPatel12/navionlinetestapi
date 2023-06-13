using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NichiOnlineTest.API.Filters;
using NichiOnlineTest.API.Helpers;
using NichiOnlineTest.Common;
using NichiOnlineTest.View.Logic;
using NichiOnlineTest.View.Models;

namespace NichiOnlineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AddHeaderAttribute(true)]
    public class OnlineTestController : BaseController
    {
        private UserTermsViewLogic _viewLogic = new UserTermsViewLogic();

        [Route("get")]
        public IActionResult OnlineTest()
        {
            try
            {
                var logic = new OnlineTestViewLogic();
                var result = logic.GetOnlineTestQuestions(UserId);
                return Ok(new ApiResponse<OnlineTestViewModel>
                {
                    StatusCode = 200,
                    Result = result,
                    ErrorMessage = null
                });
            }
            catch(Exception ex)
            {
                return Ok(new ApiResponse<string>
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("submitanswers")]
        public IActionResult SubmitAnswers(OnlineTestViewModel model)
        {
            var logic = new OnlineTestViewLogic();
            model.UserId = UserId;
            logic.SubmitAnswers(model);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetUserTestActivity")]
        public async Task<IActionResult> GetUserTestActivity(UserTermsViewModel userTermsViewModel)
        {
            try
            {
                var response = _viewLogic.GetUserTestActivity(userTermsViewModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
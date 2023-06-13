using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NichiOnlineTest.API.Filters;
using NichiOnlineTest.API.Helpers;
using NichiOnlineTest.Common;
using NichiOnlineTest.View.Logic;
using System;

namespace NichiOnlineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AddHeaderAttribute(true)]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("GetSummaryReport")]
        public ActionResult GetSummaryReportByCategoryId(Guid id)
        {
            try
            {
                HomeViewLogic logic = new HomeViewLogic();
                return Ok(logic.GetSummaryReportByCategoryId(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetCategoryList")]
        public IActionResult GetCategoryList()
        {
            try
            {
                HomeViewLogic logic = new HomeViewLogic();
                var response = logic.GetAllCategory();
                return Ok(response);
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
    }
}
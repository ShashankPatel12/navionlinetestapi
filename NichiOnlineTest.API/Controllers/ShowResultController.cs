using Microsoft.AspNetCore.Mvc;
using NichiOnlineTest.API.Filters;
using NichiOnlineTest.View.Logic;
using System;

namespace NichiOnlineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AddHeaderAttribute(true)]
    public class ShowResultController : ControllerBase
    {
        [HttpGet]
        [Route("GetShowResult")]
        public ActionResult GetShowResult(Guid categoryId, string userName)
        {
            var logic = new ShowResultViewLogic();
            var result = logic.GetResultByUser(categoryId, userName);
            return Ok(result);
        }
    }
}
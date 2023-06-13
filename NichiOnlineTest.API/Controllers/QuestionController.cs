using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NichiOnlineTest.API.Filters;
using NichiOnlineTest.API.Helpers;
using NichiOnlineTest.Common;
using NichiOnlineTest.Core.Models;
using NichiOnlineTest.View.Logic;
using NichiOnlineTest.View.Models;

namespace NichiOnlineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AddHeaderAttribute(true)]
    public class QuestionController : BaseController
    {
        private QuestionViewLogic _viewLogic = new QuestionViewLogic();


        [HttpGet]
        [Route("GetQuestions")]
        public IActionResult GetQuestions(Guid categoryId, Guid subCategoryId)
        {
            if (ModelState.IsValid)
            {
                var result = _viewLogic.GetQuestionsBySubCategoryId(categoryId, subCategoryId);

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            if (ModelState.IsValid)
            {
                var result = _viewLogic.GetAllCategory();

                return Ok(result);
            }

            return BadRequest("Error");
        }

        [HttpGet]
        [Route("GetCategorySummary")]
        public IActionResult GetCategorySummary(Guid categoryId)
        {
            if (ModelState.IsValid)
            {
                var result = _viewLogic.GetCategorySummary(categoryId);

                return Ok(new ApiResponse<List<QuestionCategorySummary>>()
                {
                    StatusCode = 200,
                    Result = result
                });
            }

            return Ok(new ApiResponse<string>()
            {
                StatusCode = 400,
                ErrorMessage = Messages.OT_0018
            });
        }


        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update(QuestionViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _viewLogic.Update(viewModel);

                    return Ok(new ApiResponse<bool>()
                    {
                        StatusCode = 200,
                        Result = result
                    });
                }

                return Ok(new ApiResponse<string>()
                {
                    StatusCode = 400,
                    ErrorMessage = ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Publish")]
        public async Task<ActionResult> PublishCategory(dynamic categoryId)
        {
            try
            {
                if (Guid.Parse(categoryId) != Guid.Empty)
                {
                    var result = _viewLogic.Publish(Guid.Parse(categoryId));

                    if (result)
                        return Ok(new ApiResponse<bool>()
                        {
                            StatusCode = 200,
                            Result = result
                        });
                }

                return Ok(new ApiResponse<string>()
                {
                    StatusCode = 400,
                    ErrorMessage = Messages.OT_0018
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
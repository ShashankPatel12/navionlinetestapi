using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NichiOnlineTest.API.Filters;
using NichiOnlineTest.API.Helpers;
using NichiOnlineTest.Common;
using NichiOnlineTest.View.Logic;
using NichiOnlineTest.View.Models;
using System;

namespace NichiOnlineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AddHeaderAttribute(true)]
    public class CategoryController : BaseController
    {
        private CategoryViewLogic _viewLogic = new CategoryViewLogic();

        [HttpGet]
        [AllowAnonymous]
        [Route("GetCategoryList")]
        public IActionResult GetCategoryList()
        {
            try
            {
                var response = _viewLogic.GetCategoryList();
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


        [HttpGet]
        [Route("GetCloneList")]
        public IActionResult GetCloneList()
        {
            try
            {
                var result = _viewLogic.GetCloneList();
                if (result == null)
                {
                    return Ok(new ApiResponse<string>()
                    {
                        StatusCode = 400,
                        ErrorMessage = Messages.OT_008
                    });
                }
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CreateCategory")]
        public IActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var response = _viewLogic.CreateCategory(categoryViewModel);
                return Ok(new ApiResponse<bool>()
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
                    ErrorMessage = ex.Message
                });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateCategory")]
        public IActionResult UpdateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var response = _viewLogic.UpdateCategory(categoryViewModel);
                return Ok(new ApiResponse<bool>()
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
                    ErrorMessage = ex.Message
                });
            }
        }

    }
}
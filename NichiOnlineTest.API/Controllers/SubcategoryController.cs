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
    public class SubcategoryController : BaseController
    {
        private SubcategoryViewLogic _viewLogic = new SubcategoryViewLogic();

        [HttpGet]
        [Route("GetSubCategoryList")]
        public IActionResult GetSubCategoryList()
        {
            try
            {
                var result = _viewLogic.GetSubCategoryList();
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

        [HttpGet]
        [Route("GetCategory")]
        public IActionResult GetCategory()
        {
            try
            {
                var result = _viewLogic.GetCategory();
                return Ok(new ApiResponse<SubcategoryViewModel>()
                {
                    StatusCode = 200,
                    Result = result
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
        [Route("Create")]
        public IActionResult Create(SubcategoryViewModel viewModel)
        {
            try
            {
                var response = _viewLogic.Create(viewModel);
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
        [Route("Update")]
        public IActionResult Update(SubcategoryViewModel viewModel)
        {
            try
            {
                var response = _viewLogic.Update(viewModel);
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
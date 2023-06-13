using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.Core.Models;
using NichiOnlineTest.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NichiOnlineTest.View.Logic
{
    public class SubcategoryViewLogic
    {
        private SubcategoryCoreLogic _coreLogic = new SubcategoryCoreLogic();

        public List<SubcategoryViewModel> GetAllSubCategory()
        {
            SubcategoryCoreLogic logic = new SubcategoryCoreLogic();
            return logic.GetAllSubCategory().Select(x => new SubcategoryViewModel
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                SubCategoryId = x.SubCategoryId,
                ClonedId = x.ClonedId,
                SubCategoryTotalMarks = x.SubCategoryTotalMarks
            }).ToList();
        }

        public List<SubcategoryViewModel> GetSubCategoryByCategoryId(Guid id)
        {
            SubcategoryCoreLogic logic = new SubcategoryCoreLogic();
            return logic.GetSubCategoryByCategoryId(id).Select(x => new SubcategoryViewModel
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                SubCategoryId = x.SubCategoryId,
                ClonedId = x.ClonedId,
                SubCategoryTotalMarks = x.SubCategoryTotalMarks
            }).ToList();
        }

        public SubcategoryViewModel GetSubCategoryById(Guid id)
        {
            SubcategoryCoreLogic logic = new SubcategoryCoreLogic();
            var result = logic.GetSubCategoryById(id);
            return new SubcategoryViewModel
            {
                CategoryId = result.CategoryId,
                Name = result.Name,
                SubCategoryId = result.SubCategoryId,
                ClonedId = result.ClonedId,
                SubCategoryTotalMarks = result.SubCategoryTotalMarks
            };
        }


        public SubcategoryViewModel GetCategory()
        {
            try
            {
                var result = _coreLogic.GetCategory();
                return new SubcategoryViewModel
                {
                    CategoryId = result.CategoryId.HasValue ? result.CategoryId.Value : Guid.Empty,
                    CategoryName = result.Name
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SubcategoryViewModel> GetSubCategoryList()
        {
            return _coreLogic.GetSubCategoryList().Select(x => new SubcategoryViewModel
            {
                CategoryName = x.CategoryName,
                CategoryId = x.CategoryId,
                Name = x.Name,
                SubCategoryId = x.SubCategoryId,
                IsPublished = x.IsPublished
            }).ToList();
        }

        public bool Create(SubcategoryViewModel viewModel)
        {
            try
            {
                var dataModel = new SubcategoryDataModel()
                {
                    Name = viewModel.Name,
                    CategoryId = viewModel.CategoryId,
                };
                return _coreLogic.Create(dataModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(SubcategoryViewModel viewModel)
        {
            try
            {
                var dataModel = new SubcategoryDataModel()
                {
                    SubCategoryId = viewModel.SubCategoryId,
                    Name = viewModel.Name,
                };
                return _coreLogic.Update(dataModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

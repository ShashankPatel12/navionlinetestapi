using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.Core.Models;
using NichiOnlineTest.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NichiOnlineTest.View.Logic
{
    public class CategoryViewLogic
    {
        private CategoryCoreLogic _coreLogic = new CategoryCoreLogic();

        public List<CategoryViewModel> GetAllCategory()
        {
            return _coreLogic.GetAllCategory().Select(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                TotalCategoryMarks = x.TotalCategoryMarks,
                TotalTime = x.TotalTime,
                IsPublished = x.IsPublished,
                IsShuffle = x.IsShuffle
            }).ToList();
        }

        public CategoryViewModel GetCategoryById(Guid id)
        {
            var result = _coreLogic.GetCategoryById(id);
            return new CategoryViewModel
            {
                CategoryId = result.CategoryId,
                Name = result.Name,
                TotalCategoryMarks = result.TotalCategoryMarks,
                TotalTime = result.TotalTime,
                IsPublished = result.IsPublished,
                IsShuffle = result.IsShuffle
            };
        }

        public bool CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var subCategoryGroupDataModel = new List<SubCategoryGroupDataModel>();

                foreach (var item in categoryViewModel.SubCategoryGroup)
                {
                    if (item.IsSelected)
                    {
                        subCategoryGroupDataModel.Add(new SubCategoryGroupDataModel()
                        {
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            SubCategoryId = item.SubCategoryId,
                            SubCategoryName = item.SubCategoryName,
                            IsSelected = item.IsSelected,
                            Marks = item.Marks
                        });
                    }

                }

                var categoryDataModel = new CategoryDataModel()
                {
                    Name = categoryViewModel.Name,
                    TotalTime = categoryViewModel.TotalTime,
                    IsShuffle = categoryViewModel.IsShuffle,
                    TotalCategoryMarks = categoryViewModel.TotalCategoryMarks,
                    SubCategoryGroup = subCategoryGroupDataModel
                };

                return _coreLogic.CreateCategory(categoryDataModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryViewModel> GetCategoryList()
        {
            var categoryList = _coreLogic.GetCategoryList();
            var categoryViewModel = new List<CategoryViewModel>();

            foreach (var item in categoryList)
            {
                categoryViewModel.Add(new CategoryViewModel()
                {
                    CategoryId = item.CategoryId,
                    IsPublished = item.IsPublished,
                    Name = item.Name,
                    IsShuffle = item.IsShuffle,
                    TotalCategoryMarks = item.TotalCategoryMarks,
                    TotalTime = item.TotalTime,
                    IsPublishedString = !item.IsPublished ? "In Progress" : "Completed"
                });

            }

            return categoryViewModel;
        }


        public List<SubCategoryGroup> GetCloneList()
        {
            var cloneDataModel = _coreLogic.GetCloneList();
            var categoryViewModel = new List<SubCategoryGroup>();

            foreach (var category in cloneDataModel)
            {
                categoryViewModel.Add(new SubCategoryGroup()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    SubCategoryId = category.SubCategoryId,
                    SubCategoryName = category.SubCategoryName,
                    IsSelected = category.IsSelected,
                    Marks = category.Marks
                });
            }

            return categoryViewModel;
        }


        public bool UpdateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var categoryDataModel = new CategoryDataModel()
                {
                    CategoryId = categoryViewModel.CategoryId,
                    Name = categoryViewModel.Name,
                    TotalTime = categoryViewModel.TotalTime,
                    IsShuffle = categoryViewModel.IsShuffle,
                    TotalCategoryMarks = categoryViewModel.TotalCategoryMarks
                };

                return _coreLogic.UpdateCategory(categoryDataModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NichiOnlineTest.View.Logic
{
    public class HomeViewLogic
    {
        public string GetSummaryReportByCategoryId(Guid categoryId)
        {
            try
            {
                HomeCoreLogic logic = new HomeCoreLogic();
                return logic.GetSummaryReportByCategoryId(categoryId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CategoryViewModel> GetAllCategory()
        {
                CategoryCoreLogic logic = new CategoryCoreLogic();
                return logic.GetAllCategory().OrderByDescending(n=>n.CreatedOn).Select(x => new CategoryViewModel
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    TotalCategoryMarks = x.TotalCategoryMarks,
                    TotalTime = x.TotalTime,
                    IsPublished = x.IsPublished,
                    IsShuffle = x.IsShuffle
                }).Where(x => x.IsPublished == true).Take(5).ToList();
        }
    }
}

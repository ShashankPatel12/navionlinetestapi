using NichiOnlineTest.Common;
using NichiOnlineTest.Core.Data;
using NichiOnlineTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace NichiOnlineTest.Core.Logic
{
    public class SubcategoryCoreLogic
    {
        private readonly VinayTestDBContext _context;

        public SubcategoryCoreLogic()
        {
            _context = new VinayTestDBContext();
        }

        public List<SubcategoryDataModel> GetAllSubCategory()
        {
            return _context.NisSubcategory.Select(x => new SubcategoryDataModel
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                ClonedId = x.ClonedId,
                SubCategoryId = x.Id,
                // SubCategoryTotalMarks = _context.NisQuestions.Where(n => n.SubcategoryId == x.Id).Sum(o => o.Marks)
            }).ToList();
        }

        public List<SubcategoryDataModel> GetSubCategoryByCategoryId(Guid id)
        {
            return _context.NisSubcategory.Where(x => x.CategoryId == id).Select(n => new SubcategoryDataModel
            {
                CategoryId = n.CategoryId,
                Name = n.Name,
                ClonedId = n.ClonedId,
                SubCategoryId = n.Id,
                //SubCategoryTotalMarks = _context.NisQuestions.Where(x => x.SubcategoryId == id).Sum(x => x.Marks)
            }).ToList();
        }

        public SubcategoryDataModel GetSubCategoryById(Guid id)
        {
            var subCategoryById = _context.NisSubcategory.FirstOrDefault(n => n.Id == id);
            SubcategoryDataModel model = new SubcategoryDataModel
            {
                CategoryId = subCategoryById.CategoryId,
                Name = subCategoryById.Name,
                ClonedId = subCategoryById.ClonedId,
                SubCategoryId = subCategoryById.Id,
                // SubCategoryTotalMarks = _context.NisQuestions.Where(x => x.SubcategoryId == id).Sum(x => x.Marks)
            };
            return model;
        }

        public CategoryDataModel GetCategory()
        {
            try
            {
                var category = _context.NisCategory.FirstOrDefault(n => n.IsPublished == false);

                if (category != null)
                {
                    CategoryDataModel model = new CategoryDataModel
                    {

                        CategoryId = category.Id,
                        Name = category.Name
                    };
                    return model;
                }
                else
                {
                    throw new Exception(Messages.OT_008);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<SubcategoryDataModel> GetSubCategoryList()
        {
            var categoryList = (from catergory in _context.NisCategory
                                select new
                                {
                                    catergory.Name,
                                    catergory.Id,
                                    catergory.IsPublished,
                                    catergory.CreatedDate,
                                }).OrderByDescending(x => x.CreatedDate).Take(5).ToList();

            var subcategoryList = (from category in categoryList
                                   join subcategory in _context.NisSubcategory on category.Id equals subcategory.CategoryId
                                   select new SubcategoryDataModel
                                   {
                                       CategoryName = category.Name,
                                       CategoryId = category.Id,
                                       Name = subcategory.Name,
                                       SubCategoryId = subcategory.Id,
                                       IsPublished = category.IsPublished
                                   }).OrderBy(x => x.IsPublished).ToList();
            return subcategoryList;
        }

        public bool Create(SubcategoryDataModel subcategoryDataModel)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                    var subCategoryNameAlreadyExist = dbContext.NisSubcategory.Where(x => x.Name == subcategoryDataModel.Name.Trim() && x.CategoryId == subcategoryDataModel.CategoryId).FirstOrDefault();

                    if (subCategoryNameAlreadyExist == null)
                    {
                        var subcategoryData = new NisSubcategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = subcategoryDataModel.Name.Trim(),
                            CategoryId = subcategoryDataModel.CategoryId
                        };
                        dbContext.NisSubcategory.Add(subcategoryData);
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception(string.Format(Messages.OT_006, subCategoryNameAlreadyExist.Name, "Subject Name"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SubcategoryDataModel subcategoryDataModel)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                    var subcategoryData = dbContext.NisSubcategory.SingleOrDefault(x => x.Id == subcategoryDataModel.SubCategoryId);

                    if (subcategoryData.Name.Trim() != subcategoryDataModel.Name.Trim())
                    {
                        var subCategoryNameAlreadyExist = dbContext.NisSubcategory.Where(x => x.Name == subcategoryDataModel.Name && x.CategoryId == subcategoryData.CategoryId).FirstOrDefault();

                        if (subCategoryNameAlreadyExist == null)
                        {
                            if (subcategoryData != null)
                            {
                                subcategoryData.Name = subcategoryDataModel.Name.Trim();
                                dbContext.SaveChanges();

                                return true;
                            }
                        }
                        else
                        {
                            throw new Exception(string.Format(Messages.OT_006, subCategoryNameAlreadyExist.Name, "Subject Name"));
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

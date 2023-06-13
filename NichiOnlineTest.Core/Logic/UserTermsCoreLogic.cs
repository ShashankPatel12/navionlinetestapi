using NichiOnlineTest.Core.Data;
using NichiOnlineTest.Core.Models;
using System.Linq;

namespace NichiOnlineTest.Core.Logic
{
    public class UserTermsCoreLogic
    {
        private readonly VinayTestDBContext _context;

        public UserTermsCoreLogic()
        {
            _context = new VinayTestDBContext();
        }

        public bool GetUserTestActivity(UserTermsDataModel userTermsDataModel)
        {
            var categoryList = _context.NisCategory.OrderByDescending(x => x.CreatedDate)
                                                 .ThenBy(x => !x.IsPublished).FirstOrDefault();

            var userActivityList = _context.NisUserTestActivity
                                    .Where(x => x.CategoryId == categoryList.Id
                                    && x.UserId == userTermsDataModel.UserId.ToString()
                                    && x.EndDateTime != null)
                                    .FirstOrDefault();

            if (userActivityList != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

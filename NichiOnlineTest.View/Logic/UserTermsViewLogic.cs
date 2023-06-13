using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.Core.Models;
using NichiOnlineTest.View.Models;

namespace NichiOnlineTest.View.Logic
{
    public class UserTermsViewLogic
    {
        private UserTermsCoreLogic _coreLogic = new UserTermsCoreLogic();

        public bool GetUserTestActivity(UserTermsViewModel userTermsViewModel)
        {
            var userTermsDataModel = new UserTermsDataModel()
            {
                UserId = userTermsViewModel.UserId
            };

            return _coreLogic.GetUserTestActivity(userTermsDataModel);
        }

    }
}

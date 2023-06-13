using NichiOnlineTest.Core.Data;
using NichiOnlineTest.Core.Models;
using System;
using System.Linq;

namespace NichiOnlineTest.Core.Logic
{
    public class ResetUserCoreLogic
    {
        /// <summary>
        /// GetResetUserDataByMobileNo
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        public static ResetUserDataModel GetResetUserDataByMobileNo(string mobileNo)
        {
            try
            {
                using (var context = new VinayTestDBContext())
                {
                    var nisUsers = context.NisUsers.Where(x => x.UserName == mobileNo).SingleOrDefault();
                    var resetUserModel = new ResetUserDataModel
                    {
                        Id = nisUsers.Id,
                        Name = nisUsers.FirstName + " " + nisUsers.LastName,
                        Email = nisUsers.Email,
                        MobileNumber = nisUsers.UserName
                    };

                    return resetUserModel;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

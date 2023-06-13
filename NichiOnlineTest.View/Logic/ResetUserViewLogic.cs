using System;
using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.View.Models;

namespace NichiOnlineTest.View.Logic
{
    public class ResetUserViewLogic
    {
        /// <summary>
        /// GetResetUserDataByMobileNo
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        public static ResetUserViewModel GetResetUserDataByMobileNo(string mobileNo)
        {
            try
            {
                var resetUserData = ResetUserCoreLogic.GetResetUserDataByMobileNo(mobileNo);

                var resetUserViewModel = new ResetUserViewModel()
                {
                    Id = resetUserData.Id,
                    Email = resetUserData.Email,
                    MobileNumber = resetUserData.MobileNumber,
                    Name = resetUserData.Name,
                };

                return resetUserViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

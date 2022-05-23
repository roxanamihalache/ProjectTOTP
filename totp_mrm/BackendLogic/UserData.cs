using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TotpMRM.BackendLogic.AlgoLogic;

namespace TotpMRM.BackendLogic
{
    public class UserData : IUserData
    {
        private readonly ITotp _totp;
        public UserData(ITotp totp)
        {
            _totp = totp;
        }
        public string GeneratePassword(int userId, DateTime dateTime)
        {
            if (userId <= 0)
                throw new ArgumentException("ID invalid", "userId");

            byte[] userIdBytes = BitConverter.GetBytes(userId);

            return _totp.GenerateTotp(userIdBytes, dateTime);
        }
    }
}
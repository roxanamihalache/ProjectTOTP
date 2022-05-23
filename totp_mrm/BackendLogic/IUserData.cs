using System;

namespace TotpMRM.BackendLogic
{
    public interface IUserData
    {
        string GeneratePassword(int userId, DateTime dateTime);
    }
}
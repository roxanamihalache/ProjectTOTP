using System;

namespace TotpMRM.BackendLogic.AlgoLogic
{
    public interface ITotp
    {
        string GenerateTotp(byte[] key, DateTime time);
    }
}
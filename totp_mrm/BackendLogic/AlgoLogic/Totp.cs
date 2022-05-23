using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;

namespace TotpMRM.BackendLogic.AlgoLogic
{
    public class Totp :  ITotp
    {
        private const int customDigitsFormat = 8;
        private const int customTimeTotp = 30;

        private static readonly DateTime UnixEpochDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public Totp()
        {
            
        }
        private static byte[] GetCounterBytes(long time)
        {
            byte[] counterBytes = new byte[8];

            for (int i = counterBytes.Length - 1; i >= 0; i--)
            {
                counterBytes[i] = (byte)(time & 0xff);
                time >>= 8;
            }

            return counterBytes;
        }

        public byte[] Encode(byte[] key, byte[] buffer)
        {
            HMACSHA1 hmac = new HMACSHA1(key);

            return hmac.ComputeHash(buffer);
        }

        private static int TruncateHashWithBinaryReduction(byte[] hash)
        {
            int truncationOffset = hash[hash.Length - 1] & 0xF;

            int binaryCode = ((hash[truncationOffset] & 0x7F) << 24) |
                             ((hash[truncationOffset + 1] & 0xFF) << 16) |
                             ((hash[truncationOffset + 2] & 0xFF) << 8) |
                             (hash[truncationOffset + 3] & 0xFF);

            return (int)(binaryCode % (int)Math.Pow(10, customDigitsFormat));
        }

        public string GenerateTotp(byte[] key, DateTime time)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (time < UnixEpochDate)
                throw new ArgumentOutOfRangeException("time", "Time cannot be less than unix epoch");

            long stepsSinceUnixEpoch = (long)(((time.ToUniversalTime() - UnixEpochDate).TotalSeconds) / customTimeTotp);

            byte[] hash = Encode(key, GetCounterBytes(stepsSinceUnixEpoch));

            int totpCode = TruncateHashWithBinaryReduction(hash);

            return totpCode.ToString(CultureInfo.InvariantCulture);
        }


    }
}
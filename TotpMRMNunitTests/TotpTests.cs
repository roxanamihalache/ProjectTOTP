using NUnit.Framework;
using TotpMRM.BackendLogic.AlgoLogic;
using System;


namespace TotpMRMNunitTests
{
    public class Tests
    {
        private Totp _totpT;

        private  byte[] _keyBytes = { 1, 0, 1, 1 };

        [SetUp]
        public void Setup()
        {
            _totpT = new Totp();
        }

        [Test]
        public void Totp_WhenDateIsEarlierThanUnixEpochDate()
        {
            Assert.That(() => _totpT.GenerateTotp(_keyBytes, DateTime.MinValue),
              Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        [Test]
        public void Totp_WhenKeyIsNull()
        {
            Assert.That(() => _totpT.GenerateTotp(null, DateTime.Now),
              Throws.TypeOf<ArgumentNullException>());
        }
        [Test]
        public void Totp_GenerateWhenWithSameKeyAndDate()
        {
            var totp1 = _totpT.GenerateTotp(_keyBytes, DateTime.Now);
            var totp2 = _totpT.GenerateTotp(_keyBytes, DateTime.Now);

            Assert.AreEqual(totp1, totp2);
        }
        [Test]
        public void Totp_GenerateWhenWithDifKeyAndDate()
        {
            byte[] _keyBytesNew = { 0, 0, 1, 2 };
            
            var totp1 = _totpT.GenerateTotp(_keyBytes, DateTime.Now);
            var totp2 = _totpT.GenerateTotp(_keyBytesNew, DateTime.Now);

            Assert.AreNotEqual(totp1, totp2);
        }

        [Test]
        public void Totp_GenerateWhenWithSameKeyAndDifDate()
        {
            var totp1 = _totpT.GenerateTotp(_keyBytes, DateTime.Now);
            var totp2 = _totpT.GenerateTotp(_keyBytes, DateTime.MaxValue);

            Assert.AreNotEqual(totp1, totp2);
        }
        [Test]
        public void Totp_GenerateWhenWithDifKeyAndDifDate()
        {
            byte[] _keyBytesNew = { 0, 0, 1, 2 };

            var totp1 = _totpT.GenerateTotp(_keyBytes, DateTime.Now);
            var totp2 = _totpT.GenerateTotp(_keyBytesNew, DateTime.Now);

            Assert.AreNotEqual(totp1, totp2);
        }

    }
}
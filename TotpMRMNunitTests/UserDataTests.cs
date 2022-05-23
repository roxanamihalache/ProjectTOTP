using NUnit.Framework;
using TotpMRM.BackendLogic;
using System;
using Moq;


namespace TotpMRMNunitTests
{
    internal class UserDataTests
    {
        private UserData _userDataT;
        private Mock<TotpMRM.BackendLogic.AlgoLogic.ITotp> _totpMockT;

        [SetUp]
        public void Setup()
        {
            _totpMockT = new Mock<TotpMRM.BackendLogic.AlgoLogic.ITotp>();
            _userDataT = new UserData(_totpMockT.Object);
        }

        [Test]
        public void UserData_GenerateCustomPass()
        {
            int customUserId = 1;

            string passUser = _userDataT.GeneratePassword(customUserId, DateTime.Now);

            byte[] userIdBytes = BitConverter.GetBytes(customUserId);
            string totpUser = _totpMockT.Object.GenerateTotp(userIdBytes, DateTime.Now);

            Assert.AreEqual(passUser, totpUser);

        }

        [Test]
        public void GeneratePassword_WhenUsedIdIs0_ThrowsException()
        {
            Assert.That(() => _userDataT.GeneratePassword(0, DateTime.Now),
                 Throws.TypeOf<ArgumentException>());
        }
    }
}

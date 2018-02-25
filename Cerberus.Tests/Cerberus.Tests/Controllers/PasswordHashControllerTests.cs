using Cerberus.Contracts.Controllers;

namespace Cerberus.Tests.Cerberus.Tests.Controllers
{
    using global::Cerberus.Controllers;
    using System;
    using Xunit;

    public class PasswordHashControllerTests
    {
        private readonly IPasswordHashController _passwordHashController;
        private const string Password = "Password1234!";
        private const string NullExceptionMessage = "Value cannot be null or empty.\r\nParameter name: ";

        public PasswordHashControllerTests()
        {
            _passwordHashController = new PasswordHashController();
        }

        [Fact]
        public void Controller_IsOfCorrectType()
        {
            Assert.IsAssignableFrom<IPasswordHashController>(_passwordHashController);
        }

        [Fact]
        public void Controller_HashString_NullPasswordThrowsException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _passwordHashController.HashString(null));
            Assert.Equal($"{NullExceptionMessage}password", ex.Message);
        }

        [Fact]
        public void Controller_HashString_HashesValueAsExpected()
        {
            var result = _passwordHashController.HashString(Password);
            Assert.Equal(68, result.Length);
        }

        [Fact]
        public void Controller_ValidatePassword_NullPasswordThrowsException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _passwordHashController.ValidatePassword(null, "HashedPassword"));
            Assert.Equal($"{NullExceptionMessage}password", ex.Message);
        }

        [Fact]
        public void Controller_ValidatePassword_NullHashedPasswordThrowsException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _passwordHashController.ValidatePassword(Password, null));
            Assert.Equal($"{NullExceptionMessage}hashedPassword", ex.Message);
        }

        [Fact]
        public void Controller_ValidatePassword_AsExpected()
        {
            var hashedPassword = _passwordHashController.HashString(Password);
            var isPasswordValidated = _passwordHashController.ValidatePassword(Password, hashedPassword);

            Assert.True(isPasswordValidated);
        }
    }
}

namespace Cerberus.Tests.Cerberus.Tests.Controllers
{
    using global::Cerberus.Contracts.Controllers;
    using global::Cerberus.Controllers;
    using System;
    using Xunit;

    public class PasswordHashControllerTests
    {
        private readonly IPasswordHashController _passwordHashController;
        private const string Password = "Password1234!";

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
            Assert.Equal("Value cannot be null.\r\nParameter name: Password", ex.Message);
        }

        [Fact]
        public void Controller_HashString_HashesValueAsExpected()
        {
            var result = _passwordHashController.HashString(Password);

            Assert.Equal(68, result.Length);
        }
    }
}

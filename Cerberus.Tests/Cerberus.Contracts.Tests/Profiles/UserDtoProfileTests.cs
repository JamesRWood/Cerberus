using Cerberus.Contracts.Dtos;
using Cerberus.Contracts.Profiles;
using Cerberus.Entities;
using System;
using Cerberus.Tests.Common;
using Xunit;

namespace Cerberus.Tests.Cerberus.Contracts.Tests.Profiles
{
    public class UserDtoProfileTests : AutomapperTestBase
    {
        public UserDtoProfileTests() : base(new UserDtoProfile())
        {
        }

        [Fact]
        public void Mapper_NullSourceObject_Returns_Null()
        {
            var result = _mapper.Map<User, UserDto>(null);

            Assert.Null(result);
        }

        [Fact]
        public void Mapper_ReturnedValue_IsOfCorrectType()
        {
            var result = _mapper.Map<User, UserDto>(new User());

            Assert.IsType<UserDto>(result);
        }

        [Fact]
        public void Mapper_DefaultSourceObject_Returns_DefaultDto()
        {
            var source = new User
            {
                Id = default(Guid),
                UserName = default(string),
                PrimaryEmail = default(string),
                SecondaryEmail = default(string),
                AccountLocked = default(bool),
                LastUpdatedAt = default(DateTime),
                LastUpdatedBy = default(string)
            };
            
            var result = _mapper.Map<User, UserDto>(source);
            
            Assert.NotNull(result);
            AssertPropertiesAsExpected(
                result,
                source.Id,
                source.UserName,
                source.PrimaryEmail,
                source.SecondaryEmail,
                source.AccountLocked,
                source.LastUpdatedAt,
                source.LastUpdatedBy);
        }

        [Fact]
        public void Mapper_EntityWithValues_Returns_PopulatedDto()
        {
            var source = new User
            {
                Id = Guid.NewGuid(),
                UserName = Guid.NewGuid().ToString(),
                PrimaryEmail = Guid.NewGuid().ToString(),
                SecondaryEmail = Guid.NewGuid().ToString(),
                AccountLocked = true,
                LastUpdatedAt = new DateTime(2017, 08, 26),
                LastUpdatedBy = Guid.NewGuid().ToString()
            };
            
            var result = _mapper.Map<User, UserDto>(source);
            
            Assert.NotNull(result);
            AssertPropertiesAsExpected(
                result,
                source.Id,
                source.UserName,
                source.PrimaryEmail,
                source.SecondaryEmail,
                source.AccountLocked,
                source.LastUpdatedAt,
                source.LastUpdatedBy);
        }

        private void AssertPropertiesAsExpected(
            UserDto actual,
            Guid expectedId,
            string expectedUserName,
            string expectedPrimaryEmail,
            string expectedSecondaryEmail,
            bool expectedAccountLocked,
            DateTime expectedLastUpdatedAt,
            string expectedLastUpdatedBy)
        {
            Assert.Equal(actual.Id, expectedId);
            Assert.Equal(actual.UserName, expectedUserName);
            Assert.Equal(actual.PrimaryEmail, expectedPrimaryEmail);
            Assert.Equal(actual.SecondaryEmail, expectedSecondaryEmail);
            Assert.Equal(actual.AccountLocked, expectedAccountLocked);
            Assert.Equal(actual.LastUpdatedAt, expectedLastUpdatedAt);
            Assert.Equal(actual.LastUpdatedBy, expectedLastUpdatedBy);
        }
    }
}

using Cerberus.Contracts.Dtos;
using Cerberus.Entities;
using System;
using Cerberus.Contracts.Profiles;
using Cerberus.Tests.Common;
using Xunit;

namespace Cerberus.Tests.Cerberus.Contracts.Tests.Profiles
{
    public class UserPasswordDtoProfileTests : AutomapperTestBase
    {
        public UserPasswordDtoProfileTests() : base(new UserPasswordDtoProfile())
        {
        }

        [Fact]
        public void Mapper_NullSourceObject_Returns_Null()
        {
            var result = _mapper.Map<UserPassword, UserPasswordDto>(null);
            
            Assert.Null(result);
        }

        [Fact]
        public void Mapper_ReturnedValue_IsOfCorrectType()
        {
            var result = _mapper.Map<UserPassword, UserPasswordDto>(new UserPassword());
            
            Assert.IsType<UserPasswordDto>(result);
        }

        [Fact]
        public void Mapper_DefaultSourceObject_Returns_DefaultDto()
        {
            var source = new UserPassword
            {
                Id = default(Guid),
                UserId = default(Guid),
                Password = default(string),
                LastUpdatedAt = default(DateTime),
                LastUpdatedBy = default(string)
            };
            
            var result = _mapper.Map<UserPassword, UserPasswordDto>(source);
            
            Assert.NotNull(result);
            AssertPropertiesAsExpected(
                result,
                source.Id,
                source.UserId,
                source.Password,
                source.LastUpdatedAt,
                source.LastUpdatedBy);
        }

        [Fact]
        public void Mapper_EntityWithValues_Returns_PopulatedDto()
        {
            var source = new UserPassword
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Password = Guid.NewGuid().ToString(),
                LastUpdatedAt = new DateTime(2017, 08, 26),
                LastUpdatedBy = Guid.NewGuid().ToString()
            };
            
            var result = _mapper.Map<UserPassword, UserPasswordDto>(source);
            
            Assert.NotNull(result);
            AssertPropertiesAsExpected(
                result,
                source.Id,
                source.UserId,
                source.Password,
                source.LastUpdatedAt,
                source.LastUpdatedBy);
        }

        private void AssertPropertiesAsExpected(
            UserPasswordDto actual,
            Guid expectedId,
            Guid expectedUserId,
            string expectedPassword,
            DateTime expectedLastUpdatedAt,
            string expectedLastUpdatedBy)
        {
            Assert.Equal(actual.Id, expectedId);
            Assert.Equal(actual.UserId, expectedUserId);
            Assert.Equal(actual.Password, expectedPassword);
            Assert.Equal(actual.LastUpdatedAt, expectedLastUpdatedAt);
            Assert.Equal(actual.LastUpdatedBy, expectedLastUpdatedBy);
        }
    }
}

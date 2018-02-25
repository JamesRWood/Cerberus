using Cerberus.Contracts.Dtos;
using Cerberus.Contracts.Profiles;
using Cerberus.Entities;
using System;
using Cerberus.Tests.Common;
using Xunit;

namespace Cerberus.Tests.Cerberus.Contracts.Tests.Profiles
{
    public class UserLoginEventDtoProfileTests : AutomapperTestBase
    {
        public UserLoginEventDtoProfileTests() : base(new UserLoginEventDtoProfile())
        {
        }

        [Fact]
        public void Mapper_NullSourceObject_Returns_Null()
        {
            var result = _mapper.Map<UserLoginEvent, UserLoginEventDto>(null);
            
            Assert.Null(result);
        }

        [Fact]
        public void Mapper_ReturnedValue_IsOfCorrectType()
        {
            var result = _mapper.Map<UserLoginEvent, UserLoginEventDto>(new UserLoginEvent());
            
            Assert.IsType<UserLoginEventDto>(result);
        }

        [Fact]
        public void Mapper_DefaultSourceObject_Returns_DefaultDto()
        {
            var source = new UserLoginEvent
            {
                Id = default(Guid),
                UserId = default(Guid),
                LastSuccessfulLogin = default(DateTime),
                LastUpdatedAt = default(DateTime),
                LastUpdatedBy = default(string)
            };
            
            var result = _mapper.Map<UserLoginEvent, UserLoginEventDto>(source);
            
            Assert.NotNull(result);
            AssertPropertiesAsExpected(
                result,
                source.Id,
                source.UserId,
                source.LastSuccessfulLogin,
                source.LastUpdatedAt,
                source.LastUpdatedBy);
        }

        [Fact]
        public void Mapper_EntityWithValues_Returns_PopulatedDto()
        {
            var source = new UserLoginEvent
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                LastSuccessfulLogin = new DateTime(2017, 08, 26),
                LastUpdatedAt = new DateTime(2017, 08, 27),
                LastUpdatedBy = Guid.NewGuid().ToString()
            };

            var result = _mapper.Map<UserLoginEvent, UserLoginEventDto>(source);
            
            Assert.NotNull(result);
            AssertPropertiesAsExpected(
                result,
                source.Id,
                source.UserId,
                source.LastSuccessfulLogin,
                source.LastUpdatedAt,
                source.LastUpdatedBy);
        }

        private void AssertPropertiesAsExpected(
            UserLoginEventDto actual,
            Guid expectedId,
            Guid expectedUserId,
            DateTime expectedLastSuccessfulLogin,
            DateTime expectedLastUpdatedAt,
            string expectedLastUpdatedBy)
        {
            Assert.Equal(actual.Id, expectedId);
            Assert.Equal(actual.UserId, expectedUserId);
            Assert.Equal(actual.LastSuccessfulLogin, expectedLastSuccessfulLogin);
            Assert.Equal(actual.LastUpdatedAt, expectedLastUpdatedAt);
            Assert.Equal(actual.LastUpdatedBy, expectedLastUpdatedBy);
        }
    }
}

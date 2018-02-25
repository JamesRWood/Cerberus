using System;
using System.Collections.Generic;
using System.Linq;
using Cerberus.Contracts.Data.Queries;
using Cerberus.Contracts.Dtos;
using Cerberus.Contracts.Profiles;
using Cerberus.Data.Queries;
using Cerberus.DatabaseContext.Interfaces;
using Cerberus.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Cerberus.Tests.Cerberus.Data.Tests.Queries
{
    public class UserByIdQueryTests : QueryTestBase
    {
        private readonly Guid _userIdA = Guid.NewGuid();
        private readonly Guid _userIdB = Guid.NewGuid();

        private readonly UserByIdQuery _userByIdQuery;

        public UserByIdQueryTests() : base(new UserDtoProfile())
        {
            _userByIdQuery = new UserByIdQuery(SetupTestDbContext().Object, _mapper);
        }

        [Fact]
        public void Execute_NullRequestObject_ThrowsException()
        {
            var result = Assert.Throws<ArgumentNullException>(() => _userByIdQuery.Execute(null));

            Assert.Equal("Value cannot be null.\r\nParameter name: request", result.Message);
        }

        [Fact]
        public void Execute_DefaultUserId_ThrowsException()
        {
            var result = Assert.Throws<ArgumentException>(() => _userByIdQuery.Execute(new UserByIdRequest(default(Guid))));

            Assert.Equal("Guid must not be default value\r\nParameter name: UserId", result.Message);
        }

        [Fact]
        public void Execute_UnMatchedUserId_ReturnsNull()
        {
            var result = _userByIdQuery.Execute(new UserByIdRequest(Guid.NewGuid()));

            Assert.NotNull(result);
            Assert.IsType<UserByIdResponse>(result);
            Assert.Null(result.UserDto);
        }

        [Fact]
        public void Execute_QueryReturnsExpected_UserIdA()
        {
            var result = _userByIdQuery.Execute(new UserByIdRequest(_userIdA));

            VerifyResult(result, _userIdA, "UserA");
        }

        [Fact]
        public void Execute_QueryReturnsExpected_UserIdB()
        {
            var result = _userByIdQuery.Execute(new UserByIdRequest(_userIdB));

            VerifyResult(result, _userIdB, "UserB");
        }

        private Mock<ICerberusDbContext> SetupTestDbContext()
        {
            var users = new List<User>
            {
                new User { Id = _userIdA, UserName = "UserA" },
                new User { Id = _userIdB, UserName = "UserB" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(() => users.GetEnumerator());

            var mockcontext = new Mock<ICerberusDbContext>();
            mockcontext.Setup(x => x.User).Returns(mockDbSet.Object);

            return mockcontext;
        }

        private void VerifyResult(UserByIdResponse result, Guid userId, string userName)
        {
            Assert.NotNull(result);
            Assert.IsType<UserByIdResponse>(result);
            Assert.NotNull(result.UserDto);
            Assert.IsType<UserDto>(result.UserDto);
            Assert.Equal(userId, result.UserDto.Id);
            Assert.Equal(userName, result.UserDto.UserName);
        }
    }
}

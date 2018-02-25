namespace Cerberus.Data.Queries
{
    using AutoMapper;
    using Cerberus.Contracts.Data.Queries;
    using Cerberus.Contracts.Dtos;
    using Cerberus.DatabaseContext.Interfaces;
    using Cerberus.Entities;
    using Hades.DataAccess.Contracts;
    using System;
    using System.Linq;

    public class UserByIdQuery : IDataOperation<UserByIdRequest, UserByIdResponse>
    {
        private readonly ICerberusDbContext _context;
        private readonly IMapper _mapper;

        public UserByIdQuery(
            ICerberusDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserByIdResponse Execute(UserByIdRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.UserId == default(Guid))
            {
                throw new ArgumentException("Guid must not be default value", nameof(request.UserId));
            }

            var user = _context.User.FirstOrDefault(x => x.Id == request.UserId);
            var userDto = user != null ? _mapper.Map<User, UserDto>(user) : null;

            return new UserByIdResponse(userDto);
        }
    }
}
using AutoMapper;
using AutoMapper.Configuration;

namespace Cerberus.Tests.Common
{
    public abstract class AutomapperTestBase
    {
        protected readonly IMapper _mapper;

        protected AutomapperTestBase(Profile profile)
        {
            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile(profile);
            var config = new MapperConfiguration(configExpression);

            this._mapper = new Mapper(config);
        }
    }
}

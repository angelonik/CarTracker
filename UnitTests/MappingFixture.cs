using AutoMapper;
using AutoMapper.Configuration;
using Services;
using System.Reflection;
using Xunit;

namespace UnitTests
{
    public class MappingFixture
    {
        public MappingFixture()
        {
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfiles(typeof(ManufacturerService).GetTypeInfo().Assembly);
            Mapper.Initialize(mappings);
        }
    }

    [CollectionDefinition("Mapping collection")]
    public class DatabaseCollection : ICollectionFixture<MappingFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}

using AutoMapper;
using NetLifeFighting.KnowTests.Mapping.Configuration.Profiles;

namespace NetLifeFighting.KnowTests.Mapping.Configuration
{
	public static class AutoMapperConfigurer
	{
		public static void Configure(IMapperConfigurationExpression cfg)
		{
			cfg.AddProfile<TestsProfile>();
			cfg.AddProfile<PersonsProfile>();
		}
	}
}
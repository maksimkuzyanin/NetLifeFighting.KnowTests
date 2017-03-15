using AutoMapper;

namespace NetLifeFighting.KnowTests.Mapping.Configuration
{
	public class AutoMapperInstaller
	{
		/// <summary>
		/// 
		/// </summary>
		public static void Install()
		{
			Mapper.Initialize(AutoMapperConfigurer.Configure);
		}
	}
}
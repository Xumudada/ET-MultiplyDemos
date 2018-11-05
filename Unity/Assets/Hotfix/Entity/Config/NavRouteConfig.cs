using ETModel;

namespace ETHotfix
{
	[Config(AppType.ClientH |  AppType.ClientM | AppType.Gate | AppType.Map)]
	public partial class NavRouteConfigCategory : ACategory<NavRouteConfig>
	{
	}

	public class NavRouteConfig: IConfig
	{
		public long Id { get; set; }
		public string Name;
		public string[] Desc;
		public int[] Position;
		public long[] Height;
		public double[] Weight;
	}
}

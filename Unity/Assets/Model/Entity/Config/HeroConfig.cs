namespace ETModel
{
	[Config(AppType.ClientH |  AppType.ClientM | AppType.Gate | AppType.Map)]
	public partial class HeroConfigCategory : ACategory<HeroConfig>
	{
	}

	public class HeroConfig: IConfig
	{
		public long Id { get; set; }
		public string Name;
		public string Prefab;
	}
}

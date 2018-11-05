namespace ETModel
{
	[Config(AppType.ClientH |  AppType.ClientM | AppType.Gate | AppType.Map)]
	public partial class Moba5V5ConfigCategory : ACategory<Moba5V5Config>
	{
	}

	public class Moba5V5Config: IConfig
	{
		public long Id { get; set; }
		public string Des;
		public string Prefab;
		public double[] Position;
		public int Rotation;
		public int Group;
		public int Type;
		public int Route;
	}
}

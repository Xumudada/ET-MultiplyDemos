using ETModel;
namespace ETHotfix
{
//游戏中 客户端请求对目标施展普通攻击
	[Message(HotfixOpcode.A1013_Attack_C2M)]
	public partial class A1013_Attack_C2M : IActorMessage {}

//游戏中 刷新一个野怪点
	[Message(HotfixOpcode.A1012_NewMonster_M2C)]
	public partial class A1012_NewMonster_M2C : IActorMessage {}

//游戏中 刷新全部野怪点
	[Message(HotfixOpcode.A1011_SpawnMonster_M2C)]
	public partial class A1011_SpawnMonster_M2C : IActorMessage {}

//游戏中 刷新第7波以后小兵 1炮车 1战士 1法师
	[Message(HotfixOpcode.A1010_NewArmyWave_M2C)]
	public partial class A1010_NewArmyWave_M2C : IActorMessage {}

//游戏中 刷新第6波以前小兵 2战士 1法师
	[Message(HotfixOpcode.A1009_NewArmyWave_M2C)]
	public partial class A1009_NewArmyWave_M2C : IActorMessage {}

//游戏开始 玩家可以移动出泉水
	[Message(HotfixOpcode.A1008_GameStart_M2C)]
	public partial class A1008_GameStart_M2C : IActorMessage {}

//玩家通知Map服务器 场景已准备就绪
	[Message(HotfixOpcode.A1007_GamerReadyMoba5V5_C2M)]
	public partial class A1007_GamerReadyMoba5V5_C2M : IActorMessage {}

//玩家移动房间广播 当前位置和移动路径
	[Message(HotfixOpcode.A1006_PathfindingResult_M2C)]
	public partial class A1006_PathfindingResult_M2C : IActorMessage {}

//右键点击移动消息
	[Message(HotfixOpcode.A1005_ClickMap_C2M)]
	public partial class A1005_ClickMap_C2M : IActorLocationMessage {}

//房间满足人数条件 Map通知客户端加载场景
	[Message(HotfixOpcode.A1004_CreateMoba5V5Secene_M2C)]
	public partial class A1004_CreateMoba5V5Secene_M2C : IActorMessage {}

//玩家信息
	[Message(HotfixOpcode.GamerInfo)]
	public partial class GamerInfo {}

//排队玩家减少了一个
	[Message(HotfixOpcode.A1003_MatcherReduceOne_M2C)]
	public partial class A1003_MatcherReduceOne_M2C : IActorMessage {}

//排队玩家增加了一个
	[Message(HotfixOpcode.A1003_MatcherPlusOne_M2C)]
	public partial class A1003_MatcherPlusOne_M2C : IActorMessage {}

//玩家请求开始匹配5V5
	[Message(HotfixOpcode.A1002_StartRobotMatch5V5_C2G)]
	public partial class A1002_StartRobotMatch5V5_C2G : IMessage {}

//玩家请求自己的个人信息 不需要参数
	[Message(HotfixOpcode.A1001_GetUserInfo_C2G)]
	public partial class A1001_GetUserInfo_C2G : IRequest {}

//获得用户名和用户等级
	[Message(HotfixOpcode.A1001_GetUserInfo_G2C)]
	public partial class A1001_GetUserInfo_G2C : IResponse {}

//客户端注册请求
	[Message(HotfixOpcode.A0001_Register_C2R)]
	public partial class A0001_Register_C2R : IRequest {}

//客户端注册请求回复
	[Message(HotfixOpcode.A0001_Register_R2C)]
	public partial class A0001_Register_R2C : IResponse {}

//客户端登陆请求
	[Message(HotfixOpcode.A0002_Login_C2R)]
	public partial class A0002_Login_C2R : IRequest {}

	[Message(HotfixOpcode.A0002_Login_R2C)]
	public partial class A0002_Login_R2C : IResponse {}

	[Message(HotfixOpcode.A0003_LoginGate_C2G)]
	public partial class A0003_LoginGate_C2G : IRequest {}

	[Message(HotfixOpcode.A0003_LoginGate_G2C)]
	public partial class A0003_LoginGate_G2C : IResponse {}

//Test 测试类消息
	[Message(HotfixOpcode.T0001_Ping)]
	public partial class T0001_Ping : IMessage {}

	[Message(HotfixOpcode.T0002_Ping_C2R)]
	public partial class T0002_Ping_C2R : IRequest {}

	[Message(HotfixOpcode.T0002_Ping_R2C)]
	public partial class T0002_Ping_R2C : IResponse {}

	[Message(HotfixOpcode.T0003_Ping)]
	public partial class T0003_Ping : IActorMessage {}

	[Message(HotfixOpcode.T0004_Ping_C2R)]
	public partial class T0004_Ping_C2R : IActorRequest {}

	[Message(HotfixOpcode.T0004_Ping_R2C)]
	public partial class T0004_Ping_R2C : IActorResponse {}

	[Message(HotfixOpcode.T0005_Ping)]
	public partial class T0005_Ping : IActorLocationMessage {}

	[Message(HotfixOpcode.T0006_Ping_C2R)]
	public partial class T0006_Ping_C2R : IActorLocationRequest {}

	[Message(HotfixOpcode.T0006_Ping_R2C)]
	public partial class T0006_Ping_R2C : IActorLocationResponse {}

	[Message(HotfixOpcode.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(HotfixOpcode.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(HotfixOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	public partial class G2C_TestHotfixMessage : IMessage {}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	public partial class C2M_TestActorRequest : IActorLocationRequest {}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	public partial class M2C_TestActorResponse : IActorLocationResponse {}

	[Message(HotfixOpcode.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(HotfixOpcode.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(HotfixOpcode.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

}
namespace ETHotfix
{
	public static partial class HotfixOpcode
	{
		 public const ushort A1013_Attack_C2M = 10001;
		 public const ushort A1012_NewMonster_M2C = 10002;
		 public const ushort A1011_SpawnMonster_M2C = 10003;
		 public const ushort A1010_NewArmyWave_M2C = 10004;
		 public const ushort A1009_NewArmyWave_M2C = 10005;
		 public const ushort A1008_GameStart_M2C = 10006;
		 public const ushort A1007_GamerReadyMoba5V5_C2M = 10007;
		 public const ushort A1006_PathfindingResult_M2C = 10008;
		 public const ushort A1005_ClickMap_C2M = 10009;
		 public const ushort A1004_CreateMoba5V5Secene_M2C = 10010;
		 public const ushort GamerInfo = 10011;
		 public const ushort A1003_MatcherReduceOne_M2C = 10012;
		 public const ushort A1003_MatcherPlusOne_M2C = 10013;
		 public const ushort A1002_StartRobotMatch5V5_C2G = 10014;
		 public const ushort A1001_GetUserInfo_C2G = 10015;
		 public const ushort A1001_GetUserInfo_G2C = 10016;
		 public const ushort A0001_Register_C2R = 10017;
		 public const ushort A0001_Register_R2C = 10018;
		 public const ushort A0002_Login_C2R = 10019;
		 public const ushort A0002_Login_R2C = 10020;
		 public const ushort A0003_LoginGate_C2G = 10021;
		 public const ushort A0003_LoginGate_G2C = 10022;
		 public const ushort T0001_Ping = 10023;
		 public const ushort T0002_Ping_C2R = 10024;
		 public const ushort T0002_Ping_R2C = 10025;
		 public const ushort T0003_Ping = 10026;
		 public const ushort T0004_Ping_C2R = 10027;
		 public const ushort T0004_Ping_R2C = 10028;
		 public const ushort T0005_Ping = 10029;
		 public const ushort T0006_Ping_C2R = 10030;
		 public const ushort T0006_Ping_R2C = 10031;
		 public const ushort C2R_Login = 10032;
		 public const ushort R2C_Login = 10033;
		 public const ushort C2G_LoginGate = 10034;
		 public const ushort G2C_LoginGate = 10035;
		 public const ushort G2C_TestHotfixMessage = 10036;
		 public const ushort C2M_TestActorRequest = 10037;
		 public const ushort M2C_TestActorResponse = 10038;
		 public const ushort PlayerInfo = 10039;
		 public const ushort C2G_PlayerInfo = 10040;
		 public const ushort G2C_PlayerInfo = 10041;
	}
}

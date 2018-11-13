using ETModel;
namespace ETModel
{
//玩家请求自己的个人信息 不需要参数
	[Message(OuterOpcode.A0008_GetUserInfo_C2G)]
	public partial class A0008_GetUserInfo_C2G : IRequest {}

//获得用户名和用户等级
	[Message(OuterOpcode.A0008_GetUserInfo_G2C)]
	public partial class A0008_GetUserInfo_G2C : IResponse {}

//单个角色的简要描述
	[Message(OuterOpcode.CharacterInfo)]
	public partial class CharacterInfo {}

//职业编号
//宠物编号
//模型编号
//武器编号
//区域类型
	[Message(OuterOpcode.A0003_LoginGate_C2G)]
	public partial class A0003_LoginGate_C2G : IRequest {}

	[Message(OuterOpcode.A0003_LoginGate_G2C)]
	public partial class A0003_LoginGate_G2C : IResponse {}

//客户端登陆请求
	[Message(OuterOpcode.A0002_Login_C2R)]
	public partial class A0002_Login_C2R : IRequest {}

	[Message(OuterOpcode.A0002_Login_R2C)]
	public partial class A0002_Login_R2C : IResponse {}

//客户端注册请求
	[Message(OuterOpcode.A0001_Register_C2R)]
	public partial class A0001_Register_C2R : IRequest {}

//客户端注册请求回复
	[Message(OuterOpcode.A0001_Register_R2C)]
	public partial class A0001_Register_R2C : IResponse {}

//----斗地主
	[Message(OuterOpcode.Card)]
	public partial class Card {}

	[Message(OuterOpcode.G2C_GetUserInfo_Back)]
	public partial class G2C_GetUserInfo_Back : IResponse {}

	[Message(OuterOpcode.C2G_GetUserInfo_Req)]
	public partial class C2G_GetUserInfo_Req : IRequest {}

//----ET
	[Message(OuterOpcode.Actor_Test)]
	public partial class Actor_Test : IActorMessage {}

	[Message(OuterOpcode.C2M_TestRequest)]
	public partial class C2M_TestRequest : IActorLocationRequest {}

	[Message(OuterOpcode.M2C_TestResponse)]
	public partial class M2C_TestResponse : IActorLocationResponse {}

	[Message(OuterOpcode.Actor_TransferRequest)]
	public partial class Actor_TransferRequest : IActorLocationRequest {}

	[Message(OuterOpcode.Actor_TransferResponse)]
	public partial class Actor_TransferResponse : IActorLocationResponse {}

	[Message(OuterOpcode.C2G_EnterMap)]
	public partial class C2G_EnterMap : IRequest {}

	[Message(OuterOpcode.G2C_EnterMap)]
	public partial class G2C_EnterMap : IResponse {}

// 自己的unit id
// 所有的unit
	[Message(OuterOpcode.UnitInfo)]
	public partial class UnitInfo {}

	[Message(OuterOpcode.M2C_CreateUnits)]
	public partial class M2C_CreateUnits : IActorMessage {}

	[Message(OuterOpcode.Frame_ClickMap)]
	public partial class Frame_ClickMap : IActorLocationMessage {}

	[Message(OuterOpcode.M2C_PathfindingResult)]
	public partial class M2C_PathfindingResult : IActorMessage {}

	[Message(OuterOpcode.C2R_Ping)]
	public partial class C2R_Ping : IRequest {}

	[Message(OuterOpcode.R2C_Ping)]
	public partial class R2C_Ping : IResponse {}

	[Message(OuterOpcode.G2C_Test)]
	public partial class G2C_Test : IMessage {}

	[Message(OuterOpcode.C2M_Reload)]
	public partial class C2M_Reload : IRequest {}

	[Message(OuterOpcode.M2C_Reload)]
	public partial class M2C_Reload : IResponse {}

}
namespace ETModel
{
	public static partial class OuterOpcode
	{
		 public const ushort A0008_GetUserInfo_C2G = 101;
		 public const ushort A0008_GetUserInfo_G2C = 102;
		 public const ushort CharacterInfo = 103;
		 public const ushort A0003_LoginGate_C2G = 104;
		 public const ushort A0003_LoginGate_G2C = 105;
		 public const ushort A0002_Login_C2R = 106;
		 public const ushort A0002_Login_R2C = 107;
		 public const ushort A0001_Register_C2R = 108;
		 public const ushort A0001_Register_R2C = 109;
		 public const ushort Card = 110;
		 public const ushort G2C_GetUserInfo_Back = 111;
		 public const ushort C2G_GetUserInfo_Req = 112;
		 public const ushort Actor_Test = 113;
		 public const ushort C2M_TestRequest = 114;
		 public const ushort M2C_TestResponse = 115;
		 public const ushort Actor_TransferRequest = 116;
		 public const ushort Actor_TransferResponse = 117;
		 public const ushort C2G_EnterMap = 118;
		 public const ushort G2C_EnterMap = 119;
		 public const ushort UnitInfo = 120;
		 public const ushort M2C_CreateUnits = 121;
		 public const ushort Frame_ClickMap = 122;
		 public const ushort M2C_PathfindingResult = 123;
		 public const ushort C2R_Ping = 124;
		 public const ushort R2C_Ping = 125;
		 public const ushort G2C_Test = 126;
		 public const ushort C2M_Reload = 127;
		 public const ushort M2C_Reload = 128;
	}
}

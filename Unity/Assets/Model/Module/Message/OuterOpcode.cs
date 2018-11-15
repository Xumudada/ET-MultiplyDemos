using ETModel;
namespace ETModel
{
//客户端请求在当前账号下创建新的角色
	[Message(OuterOpcode.A0009_CreateNewCharacter_C2G)]
	public partial class A0009_CreateNewCharacter_C2G : IRequest {}

	[Message(OuterOpcode.A0009_CreateNewCharacter_G2C)]
	public partial class A0009_CreateNewCharacter_G2C : IResponse {}

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
		 public const ushort A0009_CreateNewCharacter_C2G = 101;
		 public const ushort A0009_CreateNewCharacter_G2C = 102;
		 public const ushort A0008_GetUserInfo_C2G = 103;
		 public const ushort A0008_GetUserInfo_G2C = 104;
		 public const ushort CharacterInfo = 105;
		 public const ushort A0003_LoginGate_C2G = 106;
		 public const ushort A0003_LoginGate_G2C = 107;
		 public const ushort A0002_Login_C2R = 108;
		 public const ushort A0002_Login_R2C = 109;
		 public const ushort A0001_Register_C2R = 110;
		 public const ushort A0001_Register_R2C = 111;
		 public const ushort Card = 112;
		 public const ushort G2C_GetUserInfo_Back = 113;
		 public const ushort C2G_GetUserInfo_Req = 114;
		 public const ushort Actor_Test = 115;
		 public const ushort C2M_TestRequest = 116;
		 public const ushort M2C_TestResponse = 117;
		 public const ushort Actor_TransferRequest = 118;
		 public const ushort Actor_TransferResponse = 119;
		 public const ushort C2G_EnterMap = 120;
		 public const ushort G2C_EnterMap = 121;
		 public const ushort UnitInfo = 122;
		 public const ushort M2C_CreateUnits = 123;
		 public const ushort Frame_ClickMap = 124;
		 public const ushort M2C_PathfindingResult = 125;
		 public const ushort C2R_Ping = 126;
		 public const ushort R2C_Ping = 127;
		 public const ushort G2C_Test = 128;
		 public const ushort C2M_Reload = 129;
		 public const ushort M2C_Reload = 130;
	}
}

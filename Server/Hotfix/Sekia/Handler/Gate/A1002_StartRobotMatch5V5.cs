using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class A1002_StartRobotMatch5V5 : AMHandler<A1002_StartRobotMatch5V5_C2G>
    {
        protected override void Run(Session session, A1002_StartRobotMatch5V5_C2G message)
        {
            User user = session.GetComponent<SessionUserComponent>().User;

            //向随机Map服务器发送创建房间请求
            Session gateSession = GateHelper.GetRandomMapSession();

            B1001_CreateRobotMatch5V5_G2M create5V5 = new B1001_CreateRobotMatch5V5_G2M()
            {
                UserID = user.UserID, //用于标记玩家
                ActorIDofUser = user.InstanceId, //用于给Gate发送消息
                ActorIDofClient = user.SelfGateSessionID //用于给客户端发消息
            };
            gateSession.Send(create5V5);
        }
    }
}
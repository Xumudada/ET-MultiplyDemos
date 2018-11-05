using System;
using ETModel;
using System.Net;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class A0003_LoginGate : AMRpcHandler<A0003_LoginGate_C2G, A0003_LoginGate_G2C>
    {
        protected override void Run(Session session, A0003_LoginGate_C2G message, Action<A0003_LoginGate_G2C> reply)
        {
            A0003_LoginGate_G2C response = new A0003_LoginGate_G2C();
            try
            {
                SessionKeyComponent GateSessionKeyComponent = Game.Scene.GetComponent<SessionKeyComponent>();
                //获取玩家的永久Id
                long userId = GateSessionKeyComponent.Get(message.GateLoginKey);

                //验证登录Key是否正确
                if (userId == 0)
                {
                    response.Error = ErrorCode.ERR_ConnectGateKeyError;
                    //客户端提示：连接网关服务器超时
                    reply(response);
                    return;
                }

                //Key过期
                GateSessionKeyComponent.Remove(message.GateLoginKey);
                
                //参数userId是数据库中的永久Id
                User user = ComponentFactory.Create<User, long>(userId);
                //将新上线的User添加到容器中
                Game.Scene.GetComponent<UserComponent>().Add(user);
                user.AddComponent<MailBoxComponent>();
                
                session.AddComponent<SessionUserComponent>().User = user;
                session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);
                
                //回复Realm服务器 玩家已上线 GateAppID帮助Realm服务器定位玩家的位置
                StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
                IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
                //2个参数 1：永久UserID 2：GateAppID
                realmSession.Send(new A0004_PlayerOnline_G2R() { UserID = user.UserID, GateAppID = config.StartConfig.AppId });
                
                //设置User的参数
                user.SelfGateAppID = config.StartConfig.AppId;
                user.SelfGateSessionID = session.InstanceId;
                user.ActorIDforClient = 0;

                //回复客户端
                response.UserID = user.UserID;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}

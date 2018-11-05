using ETModel;
using System.Net;

namespace ETHotfix
{
    [ObjectSystem]
    public class SessionUserComponentDestroySystem : DestroySystem<SessionUserComponent>
    {
        public override void Destroy(SessionUserComponent self)
        {
            try
            {
                Log.Info($"销毁User和Session{self.User.UserID}");
                //释放User对象时将User对象从管理组件中移除
                Game.Scene.GetComponent<UserComponent>()?.Remove(self.User.UserID);

                StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
                ActorMessageSenderComponent actorProxyComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();

                //正在匹配中发送玩家退出匹配请求
                /*
                if (self.User.IsMatching)
                {
                    IPEndPoint matchIPEndPoint = config.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                    Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchIPEndPoint);
                    await matchSession.Call(new G2M_PlayerExitMatch_Req() { UserID = self.User.UserID });
                }
                */

                //正在游戏中发送玩家退出房间请求
                if (self.User.ActorIDforClient != 0)
                {
                    ActorMessageSender actorProxy = actorProxyComponent.Get(self.User.ActorIDforClient);
                    //await actorProxy.Call(new Actor_PlayerExitRoom_Req() { UserID = self.User.UserID });
                }

                //向登录服务器发送玩家下线消息
                IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
                realmSession.Send(new A0005_PlayerOffline_G2R() { UserID = self.User.UserID });

                self.User.Dispose();
                self.User = null;
            }
            catch (System.Exception e)
            {
                Log.Trace(e.ToString());
            }
        }
    }
}

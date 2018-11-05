using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class A0007_KickOutPlayer : AMRpcHandler<A0007_KickOutPlayer_R2G, A0007_KickOutPlayer_G2R>
    {
        protected override void Run(Session session, A0007_KickOutPlayer_R2G message, Action<A0007_KickOutPlayer_G2R> reply)
        {
            A0007_KickOutPlayer_G2R response = new A0007_KickOutPlayer_G2R();
            try
            {
                //Gate服务器收到Realm服务器移除指定User.UserID的玩家
                //获取User对象
                User user = Game.Scene.GetComponent<UserComponent>().Get(message.UserID);
                //移除在线User对象
                Game.Scene.GetComponent<UserComponent>().Remove(user.UserID);

                //服务端主动断开客户端连接
                Game.Scene.GetComponent<NetOuterComponent>().Remove(user.SelfGateSessionID);
                //Log.Info($"将玩家{message.UserID}连接断开");
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }

        }
    }
}

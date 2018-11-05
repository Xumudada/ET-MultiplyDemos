using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class A0004_PlayerOnline : AMHandler<A0004_PlayerOnline_G2R>
    {
        protected override void Run(Session session, A0004_PlayerOnline_G2R message)
        {
            OnlineComponent onlineComponent = Game.Scene.GetComponent<OnlineComponent>();

            //检查玩家是否在线 如不在线则添加
            if (onlineComponent.GetGateAppId(message.UserID) == 0)
            {
                onlineComponent.Add(message.UserID, message.GateAppID);
            }
            else
            {
                Log.Error("玩家已在线 Realm服务器收到重复上线请求的异常");
            }
        }
    }
}

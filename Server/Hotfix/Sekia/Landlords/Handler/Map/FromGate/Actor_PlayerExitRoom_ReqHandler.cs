using System;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    //Gate通知Map玩家断开连接
    [ActorMessageHandler(AppType.Map)]
    public class Actor_PlayerExitRoom_ReqHandler : AMActorHandler<Gamer, Actor_PlayerExitRoom>
    {
        protected override void Run(Gamer gamer, Actor_PlayerExitRoom message)
        {
            LandlordsRoom room;
            if (Game.Scene.GetComponent<LandlordsComponent>().Playing.TryGetValue(gamer.UserID,out room))
            {
                //如果玩家在游戏中
                gamer.isOffline = true;
                //玩家断开添加自动出牌组件
                if (gamer.GetComponent<TrusteeshipComponent>() == null)
                    gamer.AddComponent<TrusteeshipComponent>();

                Log.Info($"玩家{gamer.UserID}断开，切换为自动模式");
            }
            else //玩家不在游戏中 处于待机状态
            {
                //房间移除玩家
                room.Remove(gamer.UserID);
                Game.Scene.GetComponent<LandlordsComponent>().Waiting.Remove(gamer.UserID);
                //消息广播给其他人
                room.Broadcast(new Actor_GamerExitRoom_Ntt() { UserID = gamer.UserID });
                
                gamer.Dispose();
            }
        }
    }
}

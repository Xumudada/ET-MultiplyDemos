using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class GamerReady_Landlords : AMActorHandler<Gamer, Actor_GamerReady_Landlords>
    {
        protected override void Run(Gamer gamer, Actor_GamerReady_Landlords message)
        {
            LandlordsComponent landordsMatchComponent = Game.Scene.GetComponent<LandlordsComponent>();
            LandlordsRoom room = landordsMatchComponent.GetWaitingRoom(gamer);
            if(room != null)
            {
                //找到玩家的座位顺序 设置其准备状态为真
                int seatIndex = room.GetGamerSeat(gamer.UserID);
                if (seatIndex >= 0)
                {
                    room.isReadys[seatIndex] = true;
                    //广播通知全房间玩家
                    room.Broadcast(new Actor_GamerReady_Landlords() { UserID = gamer.UserID });
                    //检测开始游戏
                    room.CheckGameStart();
                }
                else
                {
                    Log.Error("玩家不在正确的座位上");
                }
            }
        }
    }
}

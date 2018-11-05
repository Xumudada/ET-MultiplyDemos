using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class A1007_GamerReadyMoba5V5 : AMActorHandler<Gamer, A1007_GamerReadyMoba5V5_C2M>
    {
        protected override void Run(Gamer gamer, A1007_GamerReadyMoba5V5_C2M message)
        {
            Moba5V5Component moba = Game.Scene.GetComponent<Moba5V5Component>();
            Moba5V5Room room = moba.GetWaitingRoom(gamer);
            if (room != null)
            {
                //找到玩家的座位顺序 设置其准备状态为真
                int seatIndex = room.GetGamerSeat(gamer.UserID);
                if (seatIndex >= 0)
                {
                    room.isReadys[seatIndex] = true;
                    //检测开始游戏
                    room.GetComponent<MobaControllerComponent>().CheckGameStart();
                }
                else
                {
                    Log.Error("玩家不在正确的座位上");
                }
            }
        }
    }
}

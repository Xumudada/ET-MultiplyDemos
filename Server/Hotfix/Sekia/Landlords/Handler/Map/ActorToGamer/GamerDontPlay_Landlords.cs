using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class GamerDontPlay_Landlords : AMActorHandler<Gamer, Actor_GamerDontPlay_Ntt>
    {
        protected override async void Run(Gamer gamer, Actor_GamerDontPlay_Ntt message)
        {
            LandlordsRoom room = Game.Scene.GetComponent<LandlordsComponent>().GetGamingRoom(gamer);
            OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
            if (orderController.CurrentAuthority == gamer.UserID)
            {
                //转发玩家不出牌消息
                Actor_GamerDontPlay_Ntt transpond = new Actor_GamerDontPlay_Ntt();
                transpond.UserID = gamer.UserID;
                room.Broadcast(transpond);

                //轮到下位玩家出牌
                orderController.Turn();

                //判断是否先手
                bool isFirst = orderController.CurrentAuthority == orderController.Biggest;
                if (isFirst)
                {
                    room.GetComponent<DeskCardsCacheComponent>().Clear();
                }
                room.Broadcast(new Actor_AuthorityPlayCard_Ntt() { UserID = orderController.CurrentAuthority, IsFirst = isFirst });
            }
            await Task.CompletedTask;
        }
    }
}

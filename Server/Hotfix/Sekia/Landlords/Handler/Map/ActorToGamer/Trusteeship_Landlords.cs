using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Trusteeship_Landlords : AMActorHandler<Gamer, Actor_Trusteeship_Ntt>
    {
        protected override async void Run(Gamer gamer, Actor_Trusteeship_Ntt message)
        {
            LandlordsRoom room = Game.Scene.GetComponent<LandlordsComponent>().GetGamingRoom(gamer);

            //是否已经托管
            bool isTrusteeship = gamer.GetComponent<TrusteeshipComponent>() != null;
            if (message.IsTrusteeship && !isTrusteeship)
            {
                gamer.AddComponent<TrusteeshipComponent>();
                Log.Info($"玩家{gamer.UserID}切换为自动模式");
            }
            else if (isTrusteeship)
            {
                gamer.RemoveComponent<TrusteeshipComponent>();
                Log.Info($"玩家{gamer.UserID}切换为手动模式");
            }

            //当玩家切换为手动模式时 补发出牌权
            if (isTrusteeship)
            {
                OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
                if (gamer.UserID == orderController.CurrentAuthority)
                {
                    bool isFirst = gamer.UserID == orderController.Biggest;

                    //向客户端发送出牌权消息
                    ActorMessageSender actorProxy = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(gamer.ActorIDofClient);
                    actorProxy.Send(new Actor_AuthorityPlayCard_Ntt() { UserID = orderController.CurrentAuthority, IsFirst = isFirst });
                }
            }

            await Task.CompletedTask;
        }
    }
}

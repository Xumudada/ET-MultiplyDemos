using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class GamerGrab_Landlord : AMActorHandler<Gamer, Actor_GamerGrabLandlordSelect_Ntt>
    {
        protected override async void Run(Gamer gamer, Actor_GamerGrabLandlordSelect_Ntt message)
        {
            LandlordsRoom room = Game.Scene.GetComponent<LandlordsComponent>().GetGamingRoom(gamer);
            OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
            GameControllerComponent gameController = room.GetComponent<GameControllerComponent>();
            ActorMessageSenderComponent actorProxyComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();

            if (orderController.CurrentAuthority == gamer.UserID)
            {
                //保存抢地主状态
                orderController.GamerLandlordState[gamer.UserID] = message.IsGrab;

                if (message.IsGrab)
                {
                    orderController.Biggest = gamer.UserID;
                    gameController.Multiples *= 2;
                    room.Broadcast(new Actor_SetMultiples_Ntt() { Multiples = gameController.Multiples });
                }

                //转发消息
                Actor_GamerGrabLandlordSelect_Ntt transpond = new Actor_GamerGrabLandlordSelect_Ntt();
                transpond.IsGrab = message.IsGrab;
                transpond.UserID = gamer.UserID;
                room.Broadcast(transpond);

                if (orderController.SelectLordIndex >= room.Count)
                {
                    /*
                     * 地主：√ 农民1：× 农民2：×
                     * 地主：× 农民1：√ 农民2：√
                     * 地主：√ 农民1：√ 农民2：√ 地主：√
                     * 
                     * */
                    if (orderController.Biggest == 0)
                    {
                        //没人抢地主则重新发牌
                        gameController.BackToDeck();
                        gameController.DealCards();

                        //发送玩家手牌
                        Gamer[] gamers = room.gamers;
                        List<GamerCardNum> gamersCardNum = new List<GamerCardNum>();
                        Array.ForEach(gamers, _gamer => gamersCardNum.Add(new GamerCardNum()
                        {
                            UserID = _gamer.UserID,
                            Num = _gamer.GetComponent<HandCardsComponent>().GetAll().Length
                        }));
                        Array.ForEach(gamers, _gamer =>
                        {
                            ActorMessageSender actorProxy = actorProxyComponent.Get(_gamer.ActorIDofClient);
                            actorProxy.Send(new Actor_GameStart_Ntt()
                            {
                                HandCards = To.RepeatedField(_gamer.GetComponent<HandCardsComponent>().GetAll()),
                                GamersCardNum = To.RepeatedField(gamersCardNum)
                            });
                        });

                        //随机先手玩家
                        gameController.RandomFirstAuthority();
                        return;
                    }
                    else if ((orderController.SelectLordIndex == room.Count &&
                        ((orderController.Biggest != orderController.FirstAuthority.Key && !orderController.FirstAuthority.Value) ||
                        orderController.Biggest == orderController.FirstAuthority.Key)) ||
                        orderController.SelectLordIndex > room.Count)
                    {
                        gameController.CardsOnTable(orderController.Biggest);
                        return;
                    }
                }

                //当所有玩家都抢地主时先手玩家还有一次抢地主的机会
                if (gamer.UserID == orderController.FirstAuthority.Key && message.IsGrab)
                {
                    orderController.FirstAuthority = new KeyValuePair<long, bool>(gamer.UserID, true);
                }

                orderController.Turn();
                orderController.SelectLordIndex++;
                room.Broadcast(new Actor_AuthorityGrabLandlord_Ntt() { UserID = orderController.CurrentAuthority });
            }
            await Task.CompletedTask;
        }
    }
}

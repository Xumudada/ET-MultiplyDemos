using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class GamerPlayCard_Landlords : AMActorRpcHandler<Gamer, Actor_GamerPlayCard_Req, Actor_GamerPlayCard_Back>
    {
        protected override async ETTask Run(Gamer gamer, Actor_GamerPlayCard_Req message, Action<Actor_GamerPlayCard_Back> reply)
        {
            Actor_GamerPlayCard_Back response = new Actor_GamerPlayCard_Back();
            try
            {
                LandlordsRoom room = Game.Scene.GetComponent<LandlordsComponent>().GetGamingRoom(gamer);

                GameControllerComponent gameController = room.GetComponent<GameControllerComponent>();
                DeskCardsCacheComponent deskCardsCache = room.GetComponent<DeskCardsCacheComponent>();
                OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();

                //检测是否符合出牌规则
                if (CardHelper.PopEnable(To.Array(message.Cards), out CardsType type))
                {
                    //当前出牌牌型是否比牌桌上牌型的权重更大
                    bool isWeightGreater = CardHelper.GetWeight(To.Array(message.Cards), type) > deskCardsCache.GetTotalWeight();
                    //当前出牌牌型是否和牌桌上牌型的数量一样
                    bool isSameCardsNum = message.Cards.Count == deskCardsCache.GetAll().Length;
                    //当前出牌玩家是否是上局最大出牌者
                    bool isBiggest = orderController.Biggest == orderController.CurrentAuthority;
                    //当前牌桌牌型是否是顺子
                    bool isStraight = deskCardsCache.Rule == CardsType.Straight || deskCardsCache.Rule == CardsType.DoubleStraight || deskCardsCache.Rule == CardsType.TripleStraight;
                    //当前出牌牌型是否和牌桌上牌型一样
                    bool isSameCardsType = type == deskCardsCache.Rule;

                    if (isBiggest ||    //先手出牌玩家
                        type == CardsType.JokerBoom ||  //王炸
                        type == CardsType.Boom && isWeightGreater ||    //更大的炸弹
                        isSameCardsType && isStraight && isSameCardsNum && isWeightGreater ||   //更大的顺子
                        isSameCardsType && isWeightGreater)     //更大的同类型牌
                    {
                        if (type == CardsType.JokerBoom)
                        {
                            //王炸翻4倍
                            gameController.Multiples *= 4;
                            room.Broadcast(new Actor_SetMultiples_Ntt() { Multiples = gameController.Multiples });
                        }
                        else if (type == CardsType.Boom)
                        {
                            //炸弹翻2倍
                            gameController.Multiples *= 2;
                            room.Broadcast(new Actor_SetMultiples_Ntt() { Multiples = gameController.Multiples });
                        }
                    }
                    else
                    {
                        response.Error = ErrorCode.ERR_PlayCardError;
                        reply(response);
                        return;
                    }
                }
                else
                {
                    response.Error = ErrorCode.ERR_PlayCardError;
                    reply(response);
                    return;
                }

                //如果符合将牌从手牌移到出牌缓存区
                deskCardsCache.Clear();
                deskCardsCache.Rule = type;
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                foreach (var card in message.Cards)
                {
                    handCards.PopCard(card);
                    deskCardsCache.AddCard(card);
                }

                reply(response);

                //转发玩家出牌消息
                room.Broadcast(new Actor_GamerPlayCard_Ntt() { UserID = gamer.UserID, Cards = message.Cards });

                //游戏控制器继续游戏
                gameController.Continue(gamer);

                await ETTask.CompletedTask;
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}

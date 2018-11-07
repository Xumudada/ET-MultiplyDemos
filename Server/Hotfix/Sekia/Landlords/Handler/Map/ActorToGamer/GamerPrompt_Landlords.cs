using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class GamerPrompt_Landlords : AMActorRpcHandler<Gamer, Actor_GamerPrompt_Req, Actor_GamerPrompt_Back>
    {
        protected override async ETTask Run(Gamer gamer, Actor_GamerPrompt_Req message, Action<Actor_GamerPrompt_Back> reply)
        {
            Actor_GamerPrompt_Back response = new Actor_GamerPrompt_Back();
            try
            {
                LandlordsRoom room = Game.Scene.GetComponent<LandlordsComponent>().GetGamingRoom(gamer);
                OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
                DeskCardsCacheComponent deskCardsCache = room.GetComponent<DeskCardsCacheComponent>();

                List<Card> handCards = new List<Card>(gamer.GetComponent<HandCardsComponent>().GetAll());
                CardHelper.SortCards(handCards);

                if (gamer.UserID == orderController.Biggest)
                {
                    response.Cards = To.RepeatedField(handCards.Where(card => card.CardWeight == handCards[handCards.Count - 1].CardWeight).ToArray());
                }
                else
                {
                    List<Card[]> result = await CardHelper.GetPrompt(handCards, deskCardsCache, deskCardsCache.Rule);

                    if (result.Count > 0)
                    {
                        response.Cards = To.RepeatedField(result[RandomHelper.RandomNumber(0, result.Count)]);
                    }
                }

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}

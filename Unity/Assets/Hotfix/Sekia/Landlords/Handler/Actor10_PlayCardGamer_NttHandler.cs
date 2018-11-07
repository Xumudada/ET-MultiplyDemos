using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerPlayCard_NttHandler : AMHandler<Actor_GamerPlayCard_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerPlayCard_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent room = uiRoom.GetComponent<LandlordsRoomComponent>();
            Gamer gamer = room.GetGamer(message.UserID);
            if (gamer != null)
            {
                gamer.GetComponent<LandlordsGamerPanelComponent>().ResetPrompt();

                //本地玩家清空选中牌 关闭出牌按钮
                if (gamer.UserID == LandlordsRoomComponent.LocalGamer.UserID)
                {
                    LandlordsInteractionComponent interaction = uiRoom.GetComponent<LandlordsRoomComponent>().Interaction;
                    interaction.Clear();
                    interaction.EndPlay();
                }

                //出牌后更新玩家手牌
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                Card[] Tcards = new Card[message.Cards.Count];
                for (int i = 0; i < message.Cards.Count; i++)
                {
                    Tcards[i] = message.Cards[i];
                }
                handCards.PopCards(Tcards);
            }
        }
    }
}

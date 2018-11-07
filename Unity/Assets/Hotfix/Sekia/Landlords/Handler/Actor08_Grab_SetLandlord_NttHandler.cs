using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_SetLandlord_NttHandler : AMHandler<Actor_SetLandlord_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_SetLandlord_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent room = uiRoom.GetComponent<LandlordsRoomComponent>();
            Gamer gamer = room.GetGamer(message.UserID);

            if (gamer != null)
            {
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                if (gamer.UserID == LandlordsRoomComponent.LocalGamer.UserID)
                {
                    //本地玩家添加手牌
                    Card[] Tcards = new Card[message.LordCards.Count];
                    for (int i = 0; i < message.LordCards.Count; i++)
                    {
                        Tcards[i] = message.LordCards[i];
                    }
                    handCards.AddCards(Tcards);
                }
                else
                {
                    //其他玩家设置手牌数
                    handCards.SetHandCardsNum(20);
                }
            }

            //设置值玩家身份
            foreach (var _gamer in room.gamers)
            {
                HandCardsComponent handCardsComponent = _gamer.GetComponent<HandCardsComponent>();
                LandlordsGamerPanelComponent gamerUI = _gamer.GetComponent<LandlordsGamerPanelComponent>();
                if (_gamer.UserID == message.UserID)
                {
                    handCardsComponent.AccessIdentity = Identity.Landlord;
                    gamerUI.SetIdentity(Identity.Landlord);
                }
                else
                {
                    handCardsComponent.AccessIdentity = Identity.Farmer;
                    gamerUI.SetIdentity(Identity.Farmer);
                }
            }

            //重置玩家UI提示
            foreach (var _gamer in room.gamers)
            {
                _gamer.GetComponent<LandlordsGamerPanelComponent>().ResetPrompt();
            }

            //切换地主牌精灵
            GameObject lordPokers = uiRoom.GameObject.Get<GameObject>("Desk").Get<GameObject>("LordPokers");
            for (int i = 0; i < lordPokers.transform.childCount; i++)
            {
                Sprite lordCardSprite = CardHelper.GetCardSprite(message.LordCards[i].GetName());
                lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordCardSprite;
            }

            //显示切换游戏模式按钮
            uiRoom.GetComponent<LandlordsRoomComponent>().Interaction.GameStart();
        }
    }
}

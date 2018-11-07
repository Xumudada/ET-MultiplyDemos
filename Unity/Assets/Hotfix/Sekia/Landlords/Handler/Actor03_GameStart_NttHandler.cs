using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GameStart_NttHandler : AMHandler<Actor_GameStart_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GameStart_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent room = uiRoom.GetComponent<LandlordsRoomComponent>();

            //初始化玩家UI
            foreach (GamerCardNum gamerCardNum in message.GamersCardNum)
            {
                Gamer gamer = room.GetGamer(gamerCardNum.UserID);
                LandlordsGamerPanelComponent gamerUI = gamer.GetComponent<LandlordsGamerPanelComponent>();
                gamerUI.GameStart();

                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                if (handCards != null)
                {
                    handCards.Reset();
                }
                else
                {
                    //Log.Debug("没有可以复用的HandCardsComponent，创建一个。");
                    handCards = gamer.AddComponent<HandCardsComponent, GameObject>(gamerUI.Panel);
                }

                //显示牌背面或者手牌
                handCards.Appear();

                if (gamer.UserID == LandlordsRoomComponent.LocalGamer.UserID)
                {
                    //本地玩家添加手牌
                    Card[] Tcards = new Card[message.HandCards.Count];
                    for (int i = 0; i < message.HandCards.Count; i++)
                    {
                        Tcards[i] = message.HandCards[i];
                    }
                    //Log.Debug("当前玩家手牌数量"+message.HandCards.array.Length.ToString());
                    //array的数量为32 与牌数不符合
                    handCards.AddCards(Tcards);
                }
                else
                {
                    //设置其他玩家手牌数
                    handCards.SetHandCardsNum(gamerCardNum.Num);
                }
            }

            //显示牌桌UI
            GameObject desk = uiRoom.GameObject.Get<GameObject>("Desk");
            desk.SetActive(true);
            GameObject lordPokers = desk.Get<GameObject>("LordPokers");

            //重置地主牌
            Sprite lordSprite = CardHelper.GetCardSprite("None");
            for (int i = 0; i < lordPokers.transform.childCount; i++)
            {
                lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordSprite;
            }

            LandlordsRoomComponent uiRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();
            //清空选中牌
            uiRoomComponent.Interaction.Clear();
            //设置初始倍率
            uiRoomComponent.SetMultiples(1);
        }
    }
}

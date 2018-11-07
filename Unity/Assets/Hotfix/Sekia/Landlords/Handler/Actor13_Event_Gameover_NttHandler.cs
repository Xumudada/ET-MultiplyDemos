using System;
using System.Collections.Generic;
using ETModel;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_Gameover_NttHandler : AMHandler<Actor_Gameover_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_Gameover_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            ReferenceCollector rc = uiRoom.GameObject.GetComponent<ReferenceCollector>();
            //隐藏发牌桌
            rc.Get<GameObject>("Desk").SetActive(false);

            LandlordsRoomComponent room = uiRoom.GetComponent<LandlordsRoomComponent>();
            Identity localGamerIdentity = LandlordsRoomComponent.LocalGamer.GetComponent<HandCardsComponent>().AccessIdentity;
            UI uiEndPanel = LandlordsEndFactory.Create(UIType.LandlordsEnd, uiRoom, message.Winner==(int)localGamerIdentity);
            LandlordsEndComponent landlordsEndComponent = uiEndPanel.GetComponent<LandlordsEndComponent>();

            foreach (GamerScore gamerScore in message.GamersScore)
            {
                Gamer gamer = room.GetGamer(gamerScore.UserID);
                //更新玩家信息（金钱/头像）
                gamer.GetComponent<LandlordsGamerPanelComponent>().UpdatePanel();
                //清空出牌
                //gamer.GetComponent<HandCardsComponent>().ClearPlayCards();
                gamer.GetComponent<HandCardsComponent>().Hide();
                //根据GamersScore中玩家顺序进行排列
                landlordsEndComponent.CreateGamerContent(
                    gamer,
                    localGamerIdentity,
                    message.BasePointPerMatch,
                    message.Multiples,
                    gamerScore.Score);
            }

            room.Interaction.Gameover();
            room.ResetMultiples();
        }
    }
}

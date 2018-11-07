using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerReady_NttHandler : AMHandler<Actor_GamerReady_Landlords>
    {
        protected override void Run(ETModel.Session session, Actor_GamerReady_Landlords message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent room = uiRoom.GetComponent<LandlordsRoomComponent>();
            Gamer gamer = room.GetGamer(message.UserID);
            gamer.GetComponent<LandlordsGamerPanelComponent>().SetReady();

            //本地玩家准备,隐藏准备按钮
            if (gamer.UserID == LandlordsRoomComponent.LocalGamer.UserID)
            {
                uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(false);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_Trusteeship_NttHandler : AMHandler<Actor_Trusteeship_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_Trusteeship_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent room = uiRoom.GetComponent<LandlordsRoomComponent>();
            Gamer gamer = room.GetGamer(message.UserID);

            if (gamer.UserID == LandlordsRoomComponent.LocalGamer.UserID)
            {
                LandlordsInteractionComponent interaction = uiRoom.GetComponent<LandlordsRoomComponent>().Interaction;
                interaction.isTrusteeship = message.IsTrusteeship;
            }
        }
    }
}

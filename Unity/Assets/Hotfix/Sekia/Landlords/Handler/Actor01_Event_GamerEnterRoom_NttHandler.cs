using UnityEngine;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerEnterRoom_NttHandler : AMHandler<Actor_GamerEnterRoom_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerEnterRoom_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent landlordsRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();

            //从匹配状态中切换为准备状态
            if (landlordsRoomComponent.Matching)
            {
                landlordsRoomComponent.Matching = false;
                GameObject matchPrompt = uiRoom.GameObject.Get<GameObject>("MatchPrompt");
                if (matchPrompt.activeSelf)
                {
                    matchPrompt.SetActive(false);
                    uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(true);
                }
            }

            //服务端发过来3个GamerInfo 当前玩家为其中一个
            //{"Parser":{},"UserID":382339254124924,"IsReady":false}
            int localGamerIndex = -1;
            for (int i = 0; i < message.Gamers.Count; i++)
            {
                if(message.Gamers[i].UserID== LandlordsRoomComponent.LocalGamer.UserID)
                {
                    localGamerIndex = i;
                }
            }

            if(localGamerIndex == -1)
            {
                Log.Error("难道是旁观模式？");
            }

            //添加未显示玩家
            for (int i = 0; i < message.Gamers.Count; i++)
            {
                //如果服务端发来了默认空GamerInfo 跳过
                //{"Parser":{},"UserID":0,"IsReady":false}]}
                GamerInfo gamerInfo = message.Gamers[i];
                if (gamerInfo.UserID == 0)
                    continue;
                //如果这个ID的玩家不在桌上
                if (landlordsRoomComponent.GetGamer(gamerInfo.UserID) == null)
                {
                    Gamer gamer = ETModel.ComponentFactory.Create<Gamer, long>(gamerInfo.UserID);
                    //并没有判断玩家是否已准备

                    //localGamerIndex % 3可以理解为当前玩家在3个玩家（Gamers）中的顺序
                    //localGamerIndex + 1指当前玩家的下一个玩家的相对顺序
                    //如果本地玩家序列为2 localGamerIndex + 1) % 3=0 序列为0的玩家显示在2号位
                    if ((localGamerIndex + 1) % 3 == i)
                    {
                    //玩家在本地玩家右边
                        landlordsRoomComponent.AddGamer(gamer, 2);
                    }
                    else
                    {
                    //玩家在本地玩家左边
                        landlordsRoomComponent.AddGamer(gamer, 0);
                    }
                }
            }
        }
    }
}

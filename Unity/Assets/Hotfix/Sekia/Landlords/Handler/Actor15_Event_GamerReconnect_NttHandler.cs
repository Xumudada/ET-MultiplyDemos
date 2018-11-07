using UnityEngine;
using ETModel;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerReconnect_NttHandler : AMHandler<Actor_GamerReconnect_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerReconnect_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent room = uiRoom.GetComponent<LandlordsRoomComponent>();

            //关闭准备按钮
            uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(false);
            foreach (GamerState gamerState in message.GamersState)
            {
                //遍历玩家状态 设置地主身份和头像
                Gamer gamer = room.GetGamer(gamerState.UserID);
                HandCardsComponent gamerHandCards = gamer.GetComponent<HandCardsComponent>();
                LandlordsGamerPanelComponent gamerUI = gamer.GetComponent<LandlordsGamerPanelComponent>();
                Identity gamerIdentity = (Identity)gamerState.Identity;
                gamerHandCards.AccessIdentity = gamerIdentity;
                gamerUI.SetIdentity(gamerIdentity);

                //如果在牌局中 恢复上一个玩家的出牌行为（牌最大的玩家）
                //ID用于确认玩家 身份状态用于确认是否开始了牌局
                if (message.BiggstGamer == gamer.UserID && gamerIdentity != Identity.None)
                {
                    Card[] Tcards = new Card[message.DeskCards.Count];
                    for (int i = 0; i < message.DeskCards.Count; i++)
                    {
                        Tcards[i] = message.DeskCards[i];
                    }

                    if (Tcards != null)
                    {
                        gamerHandCards.PopCards(Tcards);  //本地出牌画面更新
                    }
                }
                else if (message.LordCards.Count == 0 && gamerState.GrabLandlordState)
                {
                    //如果牌局在抢地主阶段 恢复抢地主状态
                    gamerUI.SetGrab(gamerState.GrabLandlordState);
                }
            }

            //初始化界面

            room.SetMultiples(message.Multiples);
            //当抢完地主时才能显示托管按钮
            if (message.LordCards.Count != 0)
            {
                room.Interaction.GameStart();
            }

            //初始化地主牌
            if (message.LordCards != null)
            {
                GameObject lordPokers = uiRoom.GameObject.Get<GameObject>("Desk").Get<GameObject>("LordPokers");
                for (int i = 0; i < lordPokers.transform.childCount; i++)
                {
                    Sprite lordCardSprite = CardHelper.GetCardSprite(message.LordCards[i].GetName());
                    lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordCardSprite;
                }
            }
        }
    }
}

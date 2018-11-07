using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ETHotfix
{
    [ObjectSystem]
    public class LandlordsRoomComponentAwakeSystem : AwakeSystem<LandlordsRoomComponent>
    {
        public override void Awake(LandlordsRoomComponent self)
        {
            self.Awake();
        }
    }

    //斗地主客户端本地房间组件 管理房间内玩家和房间设置
    public class LandlordsRoomComponent : Component
    {
        private readonly Dictionary<long, int> seats = new Dictionary<long, int>();
        public readonly Gamer[] gamers = new Gamer[3];
        public static Gamer LocalGamer { get;private set; }

        public LandlordsInteractionComponent interaction;

        private Text multiples;

        public readonly GameObject[] GamersPanel = new GameObject[3];

        public bool Matching { get; set; }
        
        public LandlordsInteractionComponent Interaction
        {
            get
            {
                if (interaction == null)
                {
                    UI uiRoom = this.GetParent<UI>();
                    UI uiInteraction = LandlordsInteractionFactory.Create(UIType.LandlordsInteraction, uiRoom);
                    interaction = uiInteraction.GetComponent<LandlordsInteractionComponent>();
                }
                return interaction;
            }
        }

        public void Awake()
        {
            Matching = true; //见Actor1 进入房间后取消匹配状态

            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            GameObject quitButton = rc.Get<GameObject>("QuitButton");
            GameObject readyButton = rc.Get<GameObject>("ReadyButton");
            GameObject multiplesObj = rc.Get<GameObject>("Multiples");
            multiples = multiplesObj.GetComponent<Text>();

            //绑定事件
            quitButton.GetComponent<Button>().onClick.Add(OnQuit);
            readyButton.GetComponent<Button>().onClick.Add(OnReady);

            //默认隐藏UI
            multiplesObj.SetActive(false);
            readyButton.SetActive(false);
            rc.Get<GameObject>("Desk").SetActive(false);

            //添加玩家面板
            GameObject gamersPanel = rc.Get<GameObject>("Gamers");
            this.GamersPanel[0] = gamersPanel.Get<GameObject>("Left");
            this.GamersPanel[1] = gamersPanel.Get<GameObject>("Local");
            this.GamersPanel[2] = gamersPanel.Get<GameObject>("Right");

            //添加本地玩家
            Gamer localGamer = ETModel.ComponentFactory.Create<Gamer, long>(GamerComponent.Instance.MyUser.UserID);
            AddGamer(localGamer, 1);
            LocalGamer = localGamer;
        }

        public void AddGamer(Gamer gamer, int index)
        {
            seats[gamer.UserID] = index;
            gamer.AddComponent<LandlordsGamerPanelComponent>().SetPanel(GamersPanel[index]);
            gamers[index] = gamer;
        }
        
        public int GetGamerSeat(long id)
        {
            int seatIndex;
            if (seats.TryGetValue(id, out seatIndex))
            {
                return seatIndex;
            }

            return -1;
        }

        public Gamer GetGamer(long id)
        {
            int seatIndex = GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                return gamers[seatIndex];
            }

            return null;
        }

        public void RemoveGamer(long id)
        {
            int seatIndex = GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                Gamer gamer = gamers[seatIndex];
                gamers[seatIndex] = null;
                seats.Remove(id);
                gamer.Dispose();
            }
        }
        
        public void SetMultiples(int multiples)
        {
            this.multiples.gameObject.SetActive(true);
            this.multiples.text = multiples.ToString();
        }
        
        public void ResetMultiples()
        {
            this.multiples.gameObject.SetActive(false);
            this.multiples.text = "1";
        }
        
        public void OnQuit()
        {
            //发送退出房间消息
            SessionComponent.Instance.Session.Send(new C2G_ReturnLobby_Ntt());

            //切换到大厅界面
            Game.Scene.GetComponent<UIComponent>().Create(UIType.SekiaLobby);
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.LandlordsRoom);
        }
        
        private void OnReady()
        {
            //发送准备
            SessionComponent.Instance.Session.Send(new Actor_GamerReady_Landlords());
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.Matching = false;
            this.interaction = null;
            LocalGamer = null;

            this.seats.Clear();

            for (int i = 0; i < this.gamers.Length; i++)
            {
                if (gamers[i] != null)
                {
                    gamers[i].Dispose();
                    gamers[i] = null;
                }
            }
        }
    }
}

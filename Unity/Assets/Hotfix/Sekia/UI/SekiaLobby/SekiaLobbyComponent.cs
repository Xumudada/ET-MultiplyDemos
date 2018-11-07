using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class SekiaLobbyComponentAwakeSystem : AwakeSystem<SekiaLobbyComponent>
    {
        public override void Awake(SekiaLobbyComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 大厅界面组件
    /// </summary>
    public class SekiaLobbyComponent : Component
    {
        //提示文本
        public Text prompt;
        //玩家名称
        private Text name;
        //玩家等级
        private Text level;

        public bool isMatching;

        public async void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();
            name = rc.Get<GameObject>("Name").GetComponent<Text>();
            level = rc.Get<GameObject>("Level").GetComponent<Text>();

            //添加事件
            rc.Get<GameObject>("Moba5V5").GetComponent<Button>().onClick.Add(OnStartMatchMoba5V5);
            rc.Get<GameObject>("Landlords").GetComponent<Button>().onClick.Add(OnStartMatchLandlords);
            //添加新的匹配目标

            //获取玩家数据
            A1001_GetUserInfo_C2G GetUserInfo_Req = new A1001_GetUserInfo_C2G();
            A1001_GetUserInfo_G2C GetUserInfo_Ack = (A1001_GetUserInfo_G2C)await SessionComponent.Instance.Session.Call(GetUserInfo_Req);

            //显示用户名和用户等级
            name.text = GetUserInfo_Ack.UserName;
            level.text = GetUserInfo_Ack.UserLevel.ToString();
        }

        /// <summary>
        /// 匹配Moba5V5
        /// </summary>
        public void OnStartMatchMoba5V5()
        {
            try
            {
                //发送人机匹配5V5消息
                SessionComponent.Instance.Session.Send(new A1002_StartRobotMatch5V5_C2G());

                isMatching = true; //提示：正有0个玩家排队中...
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
            }
        }

        /// <summary>
        /// 匹配斗地主
        /// </summary>
        public async void OnStartMatchLandlords()
        {
            try
            {
                //发送开始匹配消息
                C2G_StartMatch_Landlords_Req c2G_StartMatch_Req = new C2G_StartMatch_Landlords_Req();
                G2C_StartMatch_Landlords_Back g2C_StartMatch_Ack = (G2C_StartMatch_Landlords_Back)await SessionComponent.Instance.Session.Call(c2G_StartMatch_Req);

                if (g2C_StartMatch_Ack.Error == ErrorCode.ERR_UserMoneyLessError)
                {
                    Log.Error("余额不足");
                    return;
                }

                //切换到房间界面
                UI room = Game.Scene.GetComponent<UIComponent>().Create(UIType.LandlordsRoom);
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.SekiaLobby);
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
            }
        }

        //添加新的匹配目标...
    }
}

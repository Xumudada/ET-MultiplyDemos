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
            rc.Get<GameObject>("RobotMatch").GetComponent<Button>().onClick.Add(OnStartRobotMatch5V5);

            //获取玩家数据
            A1001_GetUserInfo_C2G GetUserInfo_Req = new A1001_GetUserInfo_C2G();
            A1001_GetUserInfo_G2C GetUserInfo_Ack = (A1001_GetUserInfo_G2C)await SessionComponent.Instance.Session.Call(GetUserInfo_Req);

            //显示用户名和用户等级
            name.text = GetUserInfo_Ack.UserName;
            level.text = GetUserInfo_Ack.UserLevel.ToString();
        }

        /// <summary>
        /// 开始匹配按钮事件
        /// </summary>
        public void OnStartRobotMatch5V5()
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
    }
}

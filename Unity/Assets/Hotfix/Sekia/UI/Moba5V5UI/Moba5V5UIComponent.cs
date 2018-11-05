using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class Moba5V5UIComponentAwakeSystem : AwakeSystem<Moba5V5UIComponent>
    {
        public override void Awake(Moba5V5UIComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// Moba5V5UI界面
    /// </summary>
    public class Moba5V5UIComponent : Component
    {
        //游戏当前时间 秒
        public Text gameTime; //gameTime.text
        //网络延迟 毫秒
        public Text netDelay;
        //本地玩家击杀数
        public Text kill;
        //本地玩家助攻数
        public Text assist;
        //本地玩家助攻数
        private Text death;
        //技能图标 可以设置图标/透明度/CD
        public Image skill1; //skill1.sprite
        public Image skill2;
        public Image skill3;
        public Image skill4;
        public Image summon1;
        public Image summon2;
        //技能CD
        public Text skillCD1;
        public Text skillCD2;
        public Text skillCD3;
        public Text skillCD4;
        public Text summonCD1;
        public Text summonCD2;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            gameTime = rc.Get<GameObject>("gameTime").GetComponent<Text>();
            netDelay = rc.Get<GameObject>("netDelay").GetComponent<Text>();
            kill = rc.Get<GameObject>("kill").GetComponent<Text>();
            assist = rc.Get<GameObject>("assist").GetComponent<Text>();
            death = rc.Get<GameObject>("death").GetComponent<Text>();
            skill1 = rc.Get<GameObject>("skill1").GetComponent<Image>();
            skill2 = rc.Get<GameObject>("skill2").GetComponent<Image>();
            skill3 = rc.Get<GameObject>("skill3").GetComponent<Image>();
            skill4 = rc.Get<GameObject>("skill4").GetComponent<Image>();
            summon1 = rc.Get<GameObject>("summon1").GetComponent<Image>();
            summon2 = rc.Get<GameObject>("summon2").GetComponent<Image>();
            skillCD1 = skill1.GetComponentInChildren<Text>();
            skillCD2 = skill2.GetComponentInChildren<Text>();
            skillCD3 = skill3.GetComponentInChildren<Text>();
            skillCD4 = skill4.GetComponentInChildren<Text>();
            summonCD1 = summon1.GetComponentInChildren<Text>();
            summonCD2 = summon2.GetComponentInChildren<Text>();
        }
    }
}

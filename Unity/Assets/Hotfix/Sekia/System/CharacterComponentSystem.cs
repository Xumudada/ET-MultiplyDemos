using ETModel;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;

namespace ETHotfix
{
    public static class CharacterComponentSystem
    {
        public static void test1(this CharacterComponent self)
        {

        }

        public static async ETVoid test(this CharacterComponent self)
        {
            TimerComponent timerComponent = ETModel.Game.Scene.GetComponent<TimerComponent>();
            await timerComponent.WaitAsync(1000);
        }

        //释放技能
        public static void OnSkill(this CharacterComponent self,int skillID)
        {

        }

        //普通攻击一个目标 目标ID和普攻连击数
        public static void OnAttack(this CharacterComponent self, int targetMobaID, int combo)
        {
            //播放普攻动画
            self.anime.SetIntValue("Attack", combo);

            //向目标施加被攻击特效
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("Unit.unity3d", "Unit");
            Moba5V5Component moba = Moba5V5Component.instance;
            
            GameObject effectPrefab = bundleGameObject.Get<GameObject>("Hit1");
            effectPrefab.SetActive(false);
            GameObject effectHit1 = UnityEngine.Object.Instantiate(effectPrefab);
            Gamer target;
            moba.mobaIDGamers.TryGetValue(targetMobaID, out target);
            if (target == null || effectHit1 == null) return;
            effectHit1.transform.SetParent(target.GameObject.transform.Find("Effect_OnHit").gameObject.transform, false);
            effectHit1.SetActive(true);
        }
        
        //角色被锁定
        public static void OnTargeted(this CharacterComponent self)
        {
            GameObject effect = self.GetParent<Gamer>().GameObject.transform.Find("Effect_OnTargeted").gameObject;
            effect.SetActive(true);
        }

        public static void OnUnTargeted(this CharacterComponent self)
        {
            GameObject effect = self.GetParent<Gamer>().GameObject.transform.Find("Effect_OnTargeted").gameObject;
            effect.SetActive(false);
        }
    }
}
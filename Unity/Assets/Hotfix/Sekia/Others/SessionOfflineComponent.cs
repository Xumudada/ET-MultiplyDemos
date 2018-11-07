using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ETModel;

namespace ETHotfix
{
    /// <summary>
    /// Session辅助组件 用于处理掉线后的操作
    /// </summary>
    public class SessionOfflineComponent : Component
    {
        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            //移除连接组件
            Game.Scene.RemoveComponent<SessionComponent>();
            Game.Scene.ModelScene.RemoveComponent<ETModel.SessionComponent>();

            //释放本地玩家对象
            GamerComponent gamerComponent = ETModel.Game.Scene.GetComponent<GamerComponent>();
            if (gamerComponent.MyUser != null)
            {
                gamerComponent.MyUser.Dispose();
                gamerComponent.MyUser = null;
            }

            UIComponent uiComponent = Game.Scene.GetComponent<UIComponent>();
            //游戏关闭，不用回到登录界面
            if (uiComponent == null || uiComponent.IsDisposed)
            {
                return;
            }

            UI uiLogin = uiComponent.Create(UIType.SekiaLogin);
            uiLogin.GetComponent<SekiaLoginComponent>().SetPrompt("连接断开");
            
            if (uiComponent.Get(UIType.Moba5V5UI) != null)
            {
                //移除Moba组件
                //切换场景
                uiComponent.Remove(UIType.Moba5V5UI);
            }
            else if (uiComponent.Get(UIType.SekiaLobby) != null)
            {
                uiComponent.Remove(UIType.SekiaLobby);
            }
            else if (uiComponent.Get(UIType.LandlordsRoom) != null)
            {
                uiComponent.Remove(UIType.LandlordsRoom);
            }

        }
    }
}
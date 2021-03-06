﻿using System;
using ETModel;

namespace ETHotfix
{
    public static partial class Init
    {
        public static void Sekia_Start()
        {
            try
            {
                Game.Scene.ModelScene = ETModel.Game.Scene;

                // 注册热更层回调
                ETModel.Game.Hotfix.Update = () => { Update(); };
                ETModel.Game.Hotfix.LateUpdate = () => { LateUpdate(); };
                ETModel.Game.Hotfix.OnApplicationQuit = () => { OnApplicationQuit(); };

                Game.Scene.AddComponent<UIComponent>();
                Game.Scene.AddComponent<FUIComponent>();
                Game.Scene.AddComponent<OpcodeTypeComponent>();
                Game.Scene.AddComponent<MessageDispatcherComponent>();

                // 加载热更配置
                ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
                Game.Scene.AddComponent<ConfigComponent>();
                ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
                
                //Game.EventSystem.Run(EventIdType.SekiaInitSceneStart);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
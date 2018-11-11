using ETModel;
using System;
using UnityEngine;
using FairyGUI;

namespace ETHotfix
{
    public static class SekiaLoginFactory
    {
        public static async ETTask<FUI> Create()
        {
            await ETModel.Game.Scene.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIType.Login);

            //await ETTask.CompletedTask;
            //ETModel.Game.Scene.GetComponent<FUIPackageComponent>().AddPackage(FUIType.Login);

            FUI fui = ComponentFactory.Create<FUI, GObject>(UIPackage.CreateObject(FUIType.Login, FUIType.Login));
            fui.Name = FUIType.Login;

            // 这里可以根据UI逻辑的复杂度关联性，拆分成多个小组件来写逻辑,这里逻辑比较简单就只使用一个组件了
            fui.AddComponent<FUILoginComponent>();

            return fui;
        }
    }

    [UIFactory(UIType.SekiaLogin)]
    public class SekiaLoginLoginFactory : IUIFactory
    {
        public UI Create(Scene scene, string type, GameObject parent)
        {
            try
            {
                //加载AB包
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle($"{type}.unity3d");

                //加载登录界面预设并生成实例
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset($"{type}.unity3d", $"{type}");
                GameObject login = UnityEngine.Object.Instantiate(bundleGameObject);

                //设置UI层级，只有UI摄像机可以渲染
                login.layer = LayerMask.NameToLayer(LayerNames.UI);

                //创建登录界面实体
                UI ui = ComponentFactory.Create<UI, GameObject>(login);

                //添加登录界面组件
                ui.AddComponent<SekiaLoginComponent>();
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
                return null;
            }
        }

        public void Remove(string type)
        {
            //卸载AB包
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"{type}.unity3d");
        }
    }
}

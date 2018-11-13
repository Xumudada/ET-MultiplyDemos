using System;
using UnityEngine;
using FairyGUI;

namespace ETModel
{
    public static class SekiaLoginFactory
    {
        public static async ETTask<FUI> Create()
        {
            await ETModel.Game.Scene.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIType.Sekia);
            FUI fui = ComponentFactory.Create<FUI, GObject>(UIPackage.CreateObject(FUIType.Sekia, FUIType.SekiaLogin));
            fui.Name = FUIType.SekiaLogin;
            fui.AddComponent<SekiaLoginComponent>();

            return fui;
        }
    }
}

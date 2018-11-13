using System;
using System.Collections.Generic;

namespace ETModel
{
    public static partial class SceneType
    {
        public const string Moba5V5Map = "Moba5V5Map";
    }

    public static partial class EventIdType
    {
        public const string SekiaInitSceneStart = "SekiaInitSceneStart";
        public const string SelectHandCard = "SelectHandCard";
        public const string CancelHandCard = "CancelHandCard";

    }

    public static partial class FUIType
    {
        public const string Sekia = "Sekia";
        public const string SekiaLogin = "SekiaLogin";
    }
    
    [Event(EventIdType.SekiaInitSceneStart)]
    public class InitSceneStart_CreateSekiaLogin : AEvent
    {
        public override void Run()
        {
            RunAsync().NoAwait();
        }

        public async ETVoid RunAsync()
        {
            FUIComponent fuiComponent = Game.Scene.GetComponent<FUIComponent>();
            FUI ui = await SekiaLoginFactory.Create();
            fuiComponent.Add(ui);
        }
    }
}
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
        public const string SekiaLoginFinish = "SekiaLoginFinish";
        public const string CreateCharacterFinish = "CreateCharacterFinish";
        public const string SelectCharacterFinish = "SelectCharacterFinish";
        public const string SelectHandCard = "SelectHandCard";
        public const string CancelHandCard = "CancelHandCard";
    }

    public static partial class FUIType
    {
        public const string Sekia = "Sekia";
        public const string SekiaLogin = "SekiaLogin";
        public const string CreateCharacter = "CreateCharacter";
        public const string SelectCharacter = "SelectCharacter";
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
            //实际比例
            float ratioCurrent = fuiComponent.Root.GObject.actualWidth / fuiComponent.Root.GObject.actualHeight;
            //设计比例
            float ratioDesign = (float)1280 / (float)720;
            if (ratioCurrent > ratioDesign) //当前屏幕宽度超出
            {
                fuiComponent.Root.GObject.x -= (fuiComponent.Root.GObject.actualHeight * ratioDesign - fuiComponent.Root.GObject.actualWidth) / 2;
            }
            else if (ratioCurrent < ratioDesign)//当前屏幕高度超出
            {
                //Log.Debug("物体高度：" + self.Root.GObject.height + "物体宽度：" + self.Root.GObject.width + "真实高度" + self.Root.GObject.actualHeight + "真实宽度" + self.Root.GObject.actualWidth + "屏幕高度" + Screen.height + "屏幕宽度" + Screen.width);
                //真实设计高度 - 屏幕真实高度
                fuiComponent.Root.GObject.y -= (fuiComponent.Root.GObject.actualWidth / ratioDesign - fuiComponent.Root.GObject.actualHeight) / 2;
                //Log.Debug("理想高度为：" + self.Root.GObject.width / ratioDesign);
            }

            //创建登陆界面
            FUI ui = await SekiaLoginFactory.Create();
            fuiComponent.Add(ui); 
        }
    }
}
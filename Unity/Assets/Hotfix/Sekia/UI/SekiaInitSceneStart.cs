using ETModel;

namespace ETHotfix
{
    public static partial class EventIdType
    {
        public const string SekiaInitSceneStart = "SekiaInitSceneStart";
    }

    [Event(EventIdType.SekiaInitSceneStart)]
    public class InitSceneStart_CreateSekiaLogin : AEvent
    {
        /*
        public override void Run()
        {
            //创建登录界面
            UI ui = Game.Scene.GetComponent<UIComponent>().Create(UIType.SekiaLogin);
            BackgroundImageSet.BackgroundImageOff();
        }
        */

        public override void Run()
        {
            RunAsync().NoAwait();
        }

        public async ETVoid RunAsync()
        {
            FUIComponent fuiComponent = Game.Scene.GetComponent<FUIComponent>();
            // 使用工厂创建一个Login UI
            FUI ui = await SekiaLoginFactory.Create();
            fuiComponent.Add(ui);
            BackgroundImageSet.BackgroundImageOff();
        }
    }
}

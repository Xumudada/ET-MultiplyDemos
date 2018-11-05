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
        public override void Run()
        {
            //创建登录界面
            UI ui = Game.Scene.GetComponent<UIComponent>().Create(UIType.SekiaLogin);
            BackgroundImageSet.BackgroundImageOff();
        }
    }
}

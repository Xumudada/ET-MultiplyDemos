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

using FairyGUI;

namespace ETModel
{
    public static class CreateCharacterFactory
    {
        public static void Create(A0008_GetUserInfo_G2C message)
        {
            FUIComponent fuiComponent = Game.Scene.GetComponent<FUIComponent>();
            FUI fui = ComponentFactory.Create<FUI, GObject>(UIPackage.CreateObject(FUIType.Sekia, FUIType.CreateCharacter));
            fui.Name = FUIType.CreateCharacter;
            fui.AddComponent<CreateCharacterComponent, A0008_GetUserInfo_G2C>(message);
            fuiComponent.Add(fui);
        }
    }

    [Event(EventIdType.CreateCharacterFinish)]
    public class CreateCharacterFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<FUIComponent>().Remove(FUIType.CreateCharacter);
        }
    }
}
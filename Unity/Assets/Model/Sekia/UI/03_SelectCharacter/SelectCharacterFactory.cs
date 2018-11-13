using FairyGUI;

namespace ETModel
{
    public static class SelectCharacterFactory
    {
        public static void Create(A0008_GetUserInfo_G2C message)
        {
            FUIComponent fuiComponent = Game.Scene.GetComponent<FUIComponent>();
            FUI fui = ComponentFactory.Create<FUI, GObject>(UIPackage.CreateObject(FUIType.Sekia, FUIType.SelectCharacter));
            fui.Name = FUIType.SelectCharacter;
            fui.AddComponent<SelectCharacterComponent, A0008_GetUserInfo_G2C>(message);
            fuiComponent.Add(fui);
        }
    }

    [Event(EventIdType.SelectCharacterFinish)]
    public class SelectCharacterFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<FUIComponent>().Remove(FUIType.SelectCharacter);
        }
    }
}
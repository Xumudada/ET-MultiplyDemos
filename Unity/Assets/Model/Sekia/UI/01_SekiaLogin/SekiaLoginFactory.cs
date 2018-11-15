using FairyGUI;

namespace ETModel
{
    public static class SekiaLoginFactory
    {
        public static void Create()
        {
            FUIComponent fuiComponent = Game.Scene.GetComponent<FUIComponent>();
            FUI fui = ComponentFactory.Create<FUI, GObject>(UIPackage.CreateObject(FUIType.Sekia, FUIType.SekiaLogin));
            //fui.GObject.Center();
            fui.Name = FUIType.SekiaLogin;
            fui.AddComponent<SekiaLoginComponent>();
            fuiComponent.Add(fui);
        }
    }

    [Event(EventIdType.SekiaLoginFinish)]
    public class SekiaLoginFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<FUIComponent>().Remove(FUIType.SekiaLogin);
            //移除包后该包下的所有原件将找不到纹理
            //Game.Scene.GetComponent<FUIPackageComponent>().RemovePackage(FUIType.Sekia);
        }
    }
}

using FairyGUI;

namespace ETModel
{
    public static class SekiaLoginFactory
    {
        public static async ETTask<FUI> Create()
        {
            await Game.Scene.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIType.Sekia);
            FUI fui = ComponentFactory.Create<FUI, GObject>(UIPackage.CreateObject(FUIType.Sekia, FUIType.SekiaLogin));
            fui.Name = FUIType.SekiaLogin;
            fui.AddComponent<SekiaLoginComponent>();

            return fui;
        }
    }

    [Event(EventIdType.SekiaLoginFinish)]
    public class SekiaLoginFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<FUIComponent>().Remove(FUIType.SekiaLogin);
            //移除包
            Game.Scene.GetComponent<FUIPackageComponent>().RemovePackage(FUIType.Sekia);
        }
    }
}

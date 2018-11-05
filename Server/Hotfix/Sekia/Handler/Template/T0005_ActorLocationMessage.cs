using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.AllServer)]
    public class T0005_ActorLocationMessage : AMActorLocationHandler<Gamer, T0005_Ping>
    {
        protected override void Run(Gamer gamer, T0005_Ping message)
        {
            Log.Debug("ping");
        }
    }
}
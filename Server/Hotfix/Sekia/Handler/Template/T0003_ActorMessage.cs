using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.AllServer)]
    public class T0003_ActorMessage : AMActorHandler<Gamer, T0003_Ping>
    {
        protected override void Run(Gamer gamer, T0003_Ping message)
        {
            Log.Debug("ping");
        }
    }
}
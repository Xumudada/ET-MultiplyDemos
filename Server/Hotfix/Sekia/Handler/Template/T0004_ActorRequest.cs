using System;
using System.Net;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.AllServer)]
    public class T0004_ActorRequest : AMActorRpcHandler<Gamer, T0004_Ping_C2R, T0004_Ping_R2C>
    {
        protected override async ETTask Run(Gamer gamer, T0004_Ping_C2R message, Action<T0004_Ping_R2C> reply)
        {
            T0004_Ping_R2C response = new T0004_Ping_R2C();

            try
            {
                await ETTask.CompletedTask;
                Log.Debug("ping");
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
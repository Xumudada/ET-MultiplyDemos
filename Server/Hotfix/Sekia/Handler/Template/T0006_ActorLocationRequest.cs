using System;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.AllServer)]
    public class T0006_ActorLocationRequest : AMActorLocationRpcHandler<Gamer, T0006_Ping_C2R, T0006_Ping_R2C>
    {
        protected override async ETTask Run(Gamer gamer, T0006_Ping_C2R message, Action<T0006_Ping_R2C> reply)
        {
            T0006_Ping_R2C response = new T0006_Ping_R2C();

            try
            {
                await ETTask.CompletedTask;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    public class T0002_Request : AMRpcHandler<T0002_Ping_C2R, T0002_Ping_R2C>
    {
        protected override void Run(Session session, T0002_Ping_C2R message, Action<T0002_Ping_R2C> reply)
        {
            T0002_Ping_R2C response = new T0002_Ping_R2C();
            try
            {
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
using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class A0006_GetLoginKey : AMRpcHandler<A0006_GetLoginKey_R2G, A0006_GetLoginKey_G2R>
    {
        protected override void Run(Session session, A0006_GetLoginKey_R2G message, Action<A0006_GetLoginKey_G2R> reply)
        {
            A0006_GetLoginKey_G2R response = new A0006_GetLoginKey_G2R();
            try
            {
                long key = RandomHelper.RandInt64();
                Game.Scene.GetComponent<SessionKeyComponent>().Add(key, message.UserID);
                response.GateLoginKey = key;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}

using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    public class T0001_Message : AMHandler<T0001_Ping>
    {
        protected override void Run(Session session, T0001_Ping message)
        {
            Log.Debug("ping");
        }
    }
}
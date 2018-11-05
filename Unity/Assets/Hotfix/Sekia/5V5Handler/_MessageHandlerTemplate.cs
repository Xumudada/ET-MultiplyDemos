using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class MessageHandlerTemplate : AMHandler<T0003_Ping>
    {
        protected override void Run(ETModel.Session session, T0003_Ping message)
        {
            
        }
    }
}
using ETModel;
using PF;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class A1005_ClickMap : AMActorLocationHandler<Gamer, A1005_ClickMap_C2M>
    {
        protected override void Run(Gamer gamer, A1005_ClickMap_C2M message)
        {
            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            gamer.GetComponent<GamerPathComponent>().MoveTo(target).NoAwait();
        }
    }
}
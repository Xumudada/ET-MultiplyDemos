using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class TrusteeshipComponentStartSystem : StartSystem<TrusteeshipComponent>
    {
        public override void Start(TrusteeshipComponent self)
        {
            self.Start();
        }
    }

    public static class TrusteeshipComponentSystem
    {
        public static async void Start(this TrusteeshipComponent self)
        {
            //找到玩家所在房间
            LandlordsComponent landordsMatchComponent = Game.Scene.GetComponent<LandlordsComponent>();
            Gamer gamer = self.GetParent<Gamer>();
            LandlordsRoom room = landordsMatchComponent.GetGamingRoom(self.GetParent<Gamer>());
            ActorMessageSender actorProxy = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(gamer.InstanceId);
            OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
            
            //这个托管组件是通过定时器实现的
            while (true)
            {
                //延迟1秒
                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(3000);

                if (self.IsDisposed)
                {
                    return;
                }

                if (gamer.UserID != orderController?.CurrentAuthority)
                {
                    continue;
                }

                //给Map上的Gamer发送Actor消息
                //自动提示出牌
                Actor_GamerPrompt_Back response = (Actor_GamerPrompt_Back)await actorProxy.Call(new Actor_GamerPrompt_Req());
                if (response.Error > 0 || response.Cards.Count ==0)
                {
                    actorProxy.Send(new Actor_GamerDontPlay_Ntt());
                }
                else
                {
                    await actorProxy.Call(new Actor_GamerPlayCard_Req() { Cards = response.Cards });
                }
            }
        }
    }
}

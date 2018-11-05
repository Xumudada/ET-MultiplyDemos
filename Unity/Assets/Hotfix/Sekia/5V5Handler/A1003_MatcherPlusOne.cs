using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class A1003_MatcherPlusOne : AMHandler<A1003_MatcherPlusOne_M2C>
    {
        protected override void Run(ETModel.Session session, A1003_MatcherPlusOne_M2C message)
        {
            UIComponent uiComponent = Game.Scene.GetComponent<UIComponent>();
            UI ui = uiComponent.Get(UIType.SekiaLobby);
            if (ui != null)
            {
                ui.GetComponent<SekiaLobbyComponent>().prompt.text = $"有{message.MatchingNumber}位玩家排队中...";
            }
        }
    }
}
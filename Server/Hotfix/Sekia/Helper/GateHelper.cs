using ETModel;
using System.Net;

namespace ETHotfix
{
    public static class GateHelper
    {
        /// <summary>
        /// 验证Session是否绑定了玩家 玩家是否在线
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool SignSession(Session session)
        {
            SessionUserComponent sessionUser = session.GetComponent<SessionUserComponent>();
            if (sessionUser == null || Game.Scene.GetComponent<UserComponent>().Get(sessionUser.User.UserID) == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取随机Map服务器的Session
        /// </summary>
        /// <returns></returns>
        public static Session GetRandomMapSession()
        {
            int randomMapAppId = RandomHelper.RandomNumber(0, StartConfigComponent.Instance.MapConfigs.Count);
            IPEndPoint ipEndPoint = StartConfigComponent.Instance.MapConfigs[randomMapAppId].GetComponent<InnerConfig>().IPEndPoint;
            return Game.Scene.GetComponent<NetInnerComponent>().Get(ipEndPoint);
        }
    }
}

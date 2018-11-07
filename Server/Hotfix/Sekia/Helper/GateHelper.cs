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
        /// 获取斗地主房间配置 不满足要求不能进入房间
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static RoomConfig GetLandlordsConfig(RoomLevel level)
        {
            RoomConfig config = new RoomConfig();
            switch (level)
            {
                case RoomLevel.Lv100:
                    config.BasePointPerMatch = 100;
                    config.Multiples = 1;
                    config.MinThreshold = 100 * 10;
                    break;
            }

            return config;
        }

        /// <summary>
        /// 获取斗地主游戏专用Map服务器的Session
        /// </summary>
        /// <returns></returns>
        public static Session GetLandlordsSession()
        {
            StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
            IPEndPoint matchIPEndPoint = config.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
            Log.Debug(matchIPEndPoint.ToString());
            Session LandlordsSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchIPEndPoint);
            return LandlordsSession;
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

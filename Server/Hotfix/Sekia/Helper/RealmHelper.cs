using ETModel;
using System.Net;
using System.Threading.Tasks;

namespace ETHotfix
{
    public static class RealmHelper
    {
        /// <summary>
        /// 将已在线的玩家踢下线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static async Task KickOutPlayer(long userId)
        {
            //验证账号是否在线，在线则踢下线
            //Real服务器上需要有在线玩家管理组件
            
            int gateAppId = Game.Scene.GetComponent<OnlineComponent>().GetGateAppId(userId);
            if (gateAppId != 0) //如果玩家在线 则返回其所在的AppID
            {
                Log.Debug($"玩家{userId}已在线 将执行踢下线操作");
                //如果Realm在线就移除掉
                Game.Scene.GetComponent<OnlineComponent>().Remove(userId);
                //获取玩家所在Gate服务器的Session
                StartConfig userGateConfig = Game.Scene.GetComponent<StartConfigComponent>().Get(gateAppId);
                IPEndPoint userGateIPEndPoint = userGateConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session userGateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(userGateIPEndPoint);
                //通知Gate服务器移除指定User
                await userGateSession.Call(new A0007_KickOutPlayer_R2G() { UserID = userId });
            }
        }

        private static int value;
        /// <summary>
        /// 随机生成区号 1~
        /// </summary>
        public static long GenerateId()
        {
            //随机获得GateId 1~2
            int randomGateAppId = RandomHelper.RandomNumber(0, StartConfigComponent.Instance.GateConfigs.Count) + 1;
            long time = TimeHelper.ClientNowSeconds();
            //1540 2822 75   时间为10位数
            //区号取第11位数
            return (randomGateAppId *100000000000 + time + ++value);
        }

        /// <summary>
        /// 生成指定大区的账号 参数为1以上的整数
        /// </summary>
        /// <param name="GateAppId"></param>
        /// <returns></returns>
        public static long GenerateId(int GateAppId)
        {
            long time = TimeHelper.ClientNowSeconds();
            //1540 2822 75   时间为10位数
            //区号取第11位数
            return (GateAppId * 100000000000 + time + ++value);
        }


        /// <summary>
        /// 查询账号所在大区 参数为1以上的整数
        /// </summary>
        public static int GetGateAppIdFromUserId(long userID)
        {
            return (int)(userID/100000000000);
        }
    }
}

using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Gate)]
    public class B1002_Match5V5Sucess : AMActorHandler<User, B1002_Match5V5Sucess_M2G>
    {
        protected override void Run(User user, B1002_Match5V5Sucess_M2G message)
        {
            user.ActorIDforClient = message.ActorIDofGamer;
            Log.Info($"玩家{user.UserID}匹配成功 更新客户端Actor 消息转发向Gamer");
        }
    }
}
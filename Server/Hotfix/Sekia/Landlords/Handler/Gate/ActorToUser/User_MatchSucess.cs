using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Gate)]
    public class User_MatchSucess : AMActorHandler<User, Actor_LandlordsMatchSucess>
    {
        protected override void Run(User user, Actor_LandlordsMatchSucess message)
        {
            user.ActorIDforClient = message.ActorIDofGamer;
            Log.Info($"玩家{user.UserID}匹配成功 更新客户端Actor转发向Gamer");
        }
    }
}

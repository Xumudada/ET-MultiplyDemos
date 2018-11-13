using System;
using ETModel;

namespace ETHotfix
{
    //客户端已知玩家UserID 尝试读取玩家资料
    //消息内容 玩家的属性 世界位置 任务栏 技能栏 装备栏 背包
    [MessageHandler(AppType.Gate)]
    public class A0008_GetUserInfo : AMRpcHandler<A0008_GetUserInfo_C2G, A0008_GetUserInfo_G2C>
    {
        protected override async void Run(Session session, A0008_GetUserInfo_C2G message, Action<A0008_GetUserInfo_G2C> reply)
        {
            A0008_GetUserInfo_G2C response = new A0008_GetUserInfo_G2C();
            try
            {
                //验证Session
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_UserNotOnline;
                    reply(response);
                    return;
                }

                //获取玩家对象
                User user = session.GetComponent<SessionUserComponent>().User;

                DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                UserInfo userInfo = await dbProxyComponent.Query<UserInfo>(user.UserID);
                
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
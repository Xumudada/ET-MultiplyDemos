using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_GetUserInfo_Handler : AMRpcHandler<C2G_GetUserInfo_Req, G2C_GetUserInfo_Back>
    {
        protected override async void Run(Session session, C2G_GetUserInfo_Req message, Action<G2C_GetUserInfo_Back> reply)
        {
            G2C_GetUserInfo_Back response = new G2C_GetUserInfo_Back();
            try
            {
                //验证Session
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    //Log.Debug("登陆错误");
                    reply(response);
                    return;
                }

                //查询用户信息
                //需要给Gate服务器添加数据库代理组件
                DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                UserInfo userInfo = await dbProxyComponent.Query<UserInfo>(message.UserID);
                //Log.Debug("玩家信息：" + JsonHelper.ToJson(userInfo));

                response.NickName = userInfo.UserName;
                response.Money = userInfo.Money;

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}

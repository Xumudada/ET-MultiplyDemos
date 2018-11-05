using System;
using System.Net;
using ETModel;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class A0002_Login : AMRpcHandler<A0002_Login_C2R, A0002_Login_R2C>
    {
        protected override async void Run(Session session, A0002_Login_C2R message, Action<A0002_Login_R2C> reply)
        {
            A0002_Login_R2C response = new A0002_Login_R2C();
            try
            {
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                //有多种操作数据库的方式 Json字符串模式可以提供多个条件/ID查找模式可以以Entity.Id查找：
                //UserInfo userInfo = await dbProxy.Query<UserInfo>(gamer.UserID, false);
                //先声明一个数据库操作Entity对象AccountInfo

                //验证假定的账号和密码
                List<ComponentWithId> result = await dbProxy.Query<AccountInfo>($"{{Account:'{message.Account}',Password:'{message.Password}'}}");

                if (result.Count != 1)
                {
                    response.Error = ErrorCode.ERR_AccountOrPasswordError;
                    reply(response);
                    return;
                }

                AccountInfo account = (AccountInfo)result[0];
                await RealmHelper.KickOutPlayer(account.Id);

                int GateAppId;
                StartConfig config;
                //获取账号所在区服的AppId 索取登陆Key
                if (StartConfigComponent.Instance.GateConfigs.Count ==1)
                { //只有一个Gate服务器时当作AllServer配置处理
                    config = StartConfigComponent.Instance.StartConfig;
                }
                else
                { //有多个Gate服务器时当作分布式配置处理
                    GateAppId = RealmHelper.GetGateAppIdFromUserId(account.Id);
                    config = StartConfigComponent.Instance.GateConfigs[GateAppId - 1];
                }
                IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
                Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);
                string outerAddress = config.GetComponent<OuterConfig>().Address2;

                A0006_GetLoginKey_G2R g2RGetLoginKey = (A0006_GetLoginKey_G2R)await gateSession.Call(new A0006_GetLoginKey_R2G() { UserID = account.Id });

                response.GateAddress = outerAddress;
                response.GateLoginKey = g2RGetLoginKey.GateLoginKey;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
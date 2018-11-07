using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_StartMatch_Landlords_Handler : AMRpcHandler<C2G_StartMatch_Landlords_Req, G2C_StartMatch_Landlords_Back>
    {
        protected override async void Run(Session session, C2G_StartMatch_Landlords_Req message, Action<G2C_StartMatch_Landlords_Back> reply)
        {
            G2C_StartMatch_Landlords_Back response = new G2C_StartMatch_Landlords_Back();
            try
            {
                Log.Debug("玩家开始匹配");
                //验证Session
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    reply(response);
                    return;
                }

                User user = session.GetComponent<SessionUserComponent>().User;

                //验证玩家是否符合进入房间要求,默认为100底分局
                RoomConfig roomConfig = GateHelper.GetLandlordsConfig(RoomLevel.Lv100);
                UserInfo userInfo = await Game.Scene.GetComponent<DBProxyComponent>().Query<UserInfo>(user.UserID);
                if (userInfo.Money < roomConfig.MinThreshold)
                {
                    response.Error = ErrorCode.ERR_UserMoneyLessError;
                    reply(response);
                    return;
                }
                
                reply(response);

                //获取斗地主专用Map服务器的Session
                //通知Map服务器创建新的Gamer
                Session LandlordsSession = GateHelper.GetLandlordsSession();
                LandlordsSession.Send(new G2M_EnterMatch_Landords()
                {
                    UserID = user.UserID,
                    ActorIDofUser = user.InstanceId,
                    ActorIDofClient = user.SelfGateSessionID
                });
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}

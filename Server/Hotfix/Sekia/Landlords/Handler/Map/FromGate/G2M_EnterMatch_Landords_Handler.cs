using System;
using ETModel;
using System.Threading.Tasks;

namespace ETHotfix
{
    /// <summary>
    /// Gate向Map发送“玩家请求匹配”消息
    /// </summary>
    [MessageHandler(AppType.Map)]
    public class G2M_EnterMatch_Landords_Handler : AMHandler<G2M_EnterMatch_Landords>
    {
        protected override async void Run(Session session, G2M_EnterMatch_Landords message)
        {
            //Log.Debug("Map服务器收到第一条消息");
            LandlordsComponent matchComponent = Game.Scene.GetComponent<LandlordsComponent>();

            //玩家是否已经开始游戏
            if (matchComponent.Playing.ContainsKey(message.UserID))
            {
                LandlordsRoom room;
                matchComponent.Playing.TryGetValue(message.UserID,out room);
                Gamer gamer = room.GetGamerFromUserID(message.UserID);

                //更新玩家的属性
                gamer.ActorIDofUser = message.ActorIDofUser;
                gamer.ActorIDofClient = message.ActorIDofClient;

                //帮助玩家恢复牌桌




                //向Gate上的User发送匹配成功消息 使User更新绑定的ActorID
                //Map上的Gamer需要保存User的InstanceID给其发消息
                //生成Gamer的时候 需要设置ActorIDofUser
                ActorMessageSender actorProxy = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(gamer.ActorIDofUser);
                actorProxy.Send(new Actor_LandlordsMatchSucess() { ActorIDofGamer = gamer.InstanceId });
            }
            else
            {
                //新建玩家
                Gamer newgamer = ComponentFactory.Create<Gamer, long>(message.UserID);
                newgamer.ActorIDofUser = message.ActorIDofUser;
                newgamer.ActorIDofClient = message.ActorIDofClient;

                //为Gamer添加组件
                await newgamer.AddComponent<MailBoxComponent>().AddLocation();

                //添加玩家到匹配队列 广播一遍正在匹配中的玩家
                matchComponent.AddGamerToMatchingQueue(newgamer);
            }


        }
    }
}

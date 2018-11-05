using System;
using ETModel;
using System.Collections.Generic;
using PF;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class B1001_CreateRobotMatch5V5 : AMHandler<B1001_CreateRobotMatch5V5_G2M>
    {
        protected override async void Run(Session session, B1001_CreateRobotMatch5V5_G2M message)
        {
            Moba5V5Component moba = Game.Scene.GetComponent<Moba5V5Component>();

            //创建玩家Entity
            Gamer roomCreater = ComponentFactory.Create<Gamer, long>(message.UserID);
            //设置玩家参数
            roomCreater.ActorIDofUser = message.ActorIDofUser;
            roomCreater.ActorIDofClient = message.ActorIDofClient;
            await roomCreater.AddComponent<MailBoxComponent>().AddLocation();
            moba.AddGamerToMatchingQueue(roomCreater); //需要先设置ActorIDofClient才能使用Actor消息

            //创建9个机器人加入匹配 机器人的UserID为0
            for (int i = 0; i < 9; i++)
            {
                Gamer robot = ComponentFactory.Create<Gamer, long>(0);
                await robot.AddComponent<MailBoxComponent>().AddLocation();
                moba.AddGamerToMatchingQueue(robot);
            }
        }
    }
}
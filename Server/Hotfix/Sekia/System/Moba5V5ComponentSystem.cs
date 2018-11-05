using ETModel;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace ETHotfix
{
    public static class Moba5V5ComponentSystem
    {
        /// <summary>
        /// 已开始游戏的玩家可以迅速找到自己的房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gamer"></param>
        /// <returns></returns>
        public static Moba5V5Room GetGamingRoom(this Moba5V5Component self, Gamer gamer)
        {
            Moba5V5Room room;
            if (!self.Playing.TryGetValue(gamer.UserID, out room))
            {
                Log.Error("玩家不在已经开始游戏的房间中");
            }
            return room;
        }

        /// <summary>
        /// 返回未开始游戏的房间 用于处理准备消息
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gamer"></param>
        /// <returns></returns>
        public static Moba5V5Room GetWaitingRoom(this Moba5V5Component self, Gamer gamer)
        {
            Moba5V5Room room;
            if (!self.Waiting.TryGetValue(gamer.UserID, out room))
            {
                Log.Error("玩家不在待机的房间中");
            }
            return room;
        }

        /// <summary>
        /// 获取一个可以添加座位的房间 没有则返回null
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Moba5V5Room GetFreeRoom(this Moba5V5Component self)
        {
            return self.FreeRooms.Where(p => p.Value.Count < self.StartNumber).FirstOrDefault().Value;
        }

        /// <summary>
        /// 匹配队列人数加一 队列模式没有插队/离队操作
        /// 队列满足匹配条件时 创建新房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gamer"></param>
        public static void AddGamerToMatchingQueue(this Moba5V5Component self, Gamer gamer)
        {
            //添加玩家到队列
            self.MatchingQueue.Enqueue(gamer);
            Log.Debug("一位玩家加入队列");
            //广播通知所有匹配中的玩家
            self.Broadcast(new A1003_MatcherPlusOne_M2C() { MatchingNumber = self.MatchingQueue.Count });

            //检查匹配状态
            self.MatchingCheck();
        }

        /// <summary>
        /// 检查匹配状态 每当有新排队玩家加入时执行一次
        /// </summary>
        /// <param name="self"></param>
        public static void MatchingCheck(this Moba5V5Component self)
        {
            //如果有空房间 且 正在排队的玩家>0
            Moba5V5Room room = self.GetFreeRoom();
            if (room != null)
            {
                while (self.MatchingQueue.Count > 0 && room.Count < self.StartNumber)
                {
                    self.JoinRoom(room, self.MatchingQueue.Dequeue());
                }
            } //else 如果没有空房间 且 正在排队的玩家>=开局人数
            else if (self.MatchingQueue.Count >= self.StartNumber)
            {
                //创建新房间
                room = ComponentFactory.Create<Moba5V5Room>();
                room.AddComponent<MailBoxComponent>();
                self.FreeRooms.Add(room.Id, room);

                while (self.MatchingQueue.Count > 0 && room.Count < self.StartNumber)
                {
                    self.JoinRoom(room, self.MatchingQueue.Dequeue());
                }
            }
        }

        /// <summary>
        /// 匹配队列广播
        /// </summary>
        /// <param name="self"></param>
        /// <param name="message"></param>
        public static void Broadcast(this Moba5V5Component self, IActorMessage message)
        {
            foreach (Gamer gamer in self.MatchingQueue)
            {
                if(gamer.UserID == 0)
                {
                    continue;
                }

                //像Gate服务器中User绑定的Seesion发送Actor消息
                ActorMessageSenderComponent actorProxyComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
                ActorMessageSender actorProxy = actorProxyComponent.Get(gamer.ActorIDofClient);
                actorProxy.Send(message);
            }
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="room"></param>
        /// <param name="matcher"></param>
        public static void JoinRoom(this Moba5V5Component self, Moba5V5Room room, Gamer gamer)
        {
            //玩家可能掉线
            if (gamer == null)
            {
                return;
            }

            //玩家绑定待机房间 机器人不用绑定房间
            if(gamer.UserID != 0)
            {
                self.Waiting.Add(gamer.UserID, room);
            }
            
            //为玩家添加座位 机器人也有座位
            room.Add(gamer);

            //通知Gate服务器玩家匹配成功 参数：Gamer的InstanceId
            //Gate服务器将Actor类消息转发给Gamer
            if(gamer.UserID != 0)
            {
                ActorMessageSenderComponent actorProxyComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
                ActorMessageSender gamerActorProxy = actorProxyComponent.Get(gamer.ActorIDofUser);
                gamerActorProxy.Send(new B1002_Match5V5Sucess_M2G() { ActorIDofGamer = gamer.InstanceId });
            }
        }
    }
}

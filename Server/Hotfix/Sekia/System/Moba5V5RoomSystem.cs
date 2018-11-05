using ETModel;

namespace ETHotfix
{
    public static class Moba5V5RoomSystem
    {
        /// <summary>
        /// 添加玩家 没有空位时检查游戏开始
        /// </summary>
        /// <param name="gamer"></param>
        public static void Add(this Moba5V5Room self, Gamer gamer)
        {
            int seatIndex = self.GetEmptySeat();
            //玩家需要获取一个座位坐下
            if (seatIndex >= 0)
            {
                self.gamers[seatIndex] = gamer;
                self.isReadys[seatIndex] = false;
                self.seats[gamer.UserID] = seatIndex;
            }
            else
            {
                Log.Error("房间已满无法加入");
            }

            //房间满员时 通知客户端加载场景
            if(self.GetEmptySeat() == -1)
            {
                //为房间添加游戏组件
                if(self.GetComponent<MobaControllerComponent>() == null)
                {
                    self.AddComponent<MobaControllerComponent>().RoomReady();
                }
            }
        }

        /// <summary>
        /// 获取房间中的玩家
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Gamer GetGamerFromUserID(this Moba5V5Room self, long id)
        {
            int seatIndex = self.GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                return self.gamers[seatIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取玩家座位索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetGamerSeat(this Moba5V5Room self, long id)
        {
            if (self.seats.TryGetValue(id, out int seatIndex))
            {
                return seatIndex;
            }

            return -1;
        }

        /// <summary>
        /// 返回玩家是否已准备 玩家不在房间时返回false
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gamer"></param>
        /// <returns></returns>
        public static bool IsGamerReady(this Moba5V5Room self, Gamer gamer)
        {
            int seatIndex = self.GetGamerSeat(gamer.UserID);
            if (seatIndex > 0)
            {
                return self.isReadys[seatIndex];
            }
            return false;
        }

        /// <summary>
        /// 移除玩家并返回 玩家离开房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Gamer Remove(this Moba5V5Room self, long id)
        {
            int seatIndex = self.GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                Gamer gamer = self.gamers[seatIndex];
                self.gamers[seatIndex] = null;
                self.seats.Remove(id);
                return gamer;
            }

            return null;
        }

        /// <summary>
        /// 获取空座位 没有空位时返回-1
        /// </summary>
        /// <returns>返回座位索引，没有空座位时返回-1</returns>
        public static int GetEmptySeat(this Moba5V5Room self)
        {
            for (int i = 0; i < self.gamers.Length; i++)
            {
                if (self.gamers[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 广播消息 通知客户端
        /// </summary>
        /// <param name="message"></param>
        public static void Broadcast(this Moba5V5Room self, IActorMessage message)
        {
            foreach (Gamer gamer in self.gamers)
            {
                //如果玩家不存在或者不在线
                if (gamer == null || gamer.isOffline || gamer.UserID == 0)
                {
                    continue;
                }
                ActorMessageSenderComponent actorProxyComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
                ActorMessageSender actorProxy = actorProxyComponent.Get(gamer.ActorIDofClient);
                actorProxy.Send(message);
            }
        }
    }
}

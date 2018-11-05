using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 匹配组件
    /// </summary>
    public class Moba5V5Component : Component
    {
        /// <summary>
        /// 所有游戏中的房间列表
        /// </summary>
        public readonly Dictionary<long, Moba5V5Room> GamingRooms = new Dictionary<long, Moba5V5Room>();

        /// <summary>
        /// 所有游戏没有开始的房间列表 Room.Id/Room
        /// </summary>
        public readonly Dictionary<long, Moba5V5Room> FreeRooms = new Dictionary<long, Moba5V5Room>();

        /// <summary>
        /// 所有在房间中待机的玩家 UserID/Room
        /// </summary>
        public readonly Dictionary<long, Moba5V5Room> Waiting = new Dictionary<long, Moba5V5Room>();

        /// <summary>
        /// 所有正在游戏的玩家 UserID/Room
        /// </summary>
        public readonly Dictionary<long, Moba5V5Room> Playing = new Dictionary<long, Moba5V5Room>();

        /// <summary>
        /// 匹配中的玩家队列
        /// </summary>
        public readonly Queue<Gamer> MatchingQueue = new Queue<Gamer>();

        /// <summary>
        /// 开局人数
        /// </summary>
        public int StartNumber = 10;
    }
}

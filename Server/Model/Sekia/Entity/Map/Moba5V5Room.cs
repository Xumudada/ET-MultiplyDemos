using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    /// <summary>
    /// 房间对象
    /// </summary>
    public sealed class Moba5V5Room : Entity
    {
        /// <summary>
        /// 当前房间的10个座位 前5个位蓝方玩家 后5个为红方玩家
        /// </summary>
        public readonly Dictionary<long, int> seats = new Dictionary<long, int>();
        /// <summary>
        /// 当前房间的所有所有玩家 空位为null
        /// </summary>
        public readonly Gamer[] gamers = new Gamer[10];
        public readonly bool[] isReadys = new bool[10];

        /// <summary>
        /// 房间中玩家的数量
        /// </summary>
        public int Count { get { return seats.Values.Count; } }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            seats.Clear();

            for (int i = 0; i < gamers.Length; i++)
            {
                if (gamers[i] != null)
                {
                    gamers[i].Dispose();
                    gamers[i] = null;
                }
            }

            for (int i = 0; i < isReadys.Length; i++)
            {
                isReadys[i] = false;
            }
        }
    }
}

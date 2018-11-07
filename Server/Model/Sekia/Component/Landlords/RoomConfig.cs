namespace ETModel
{
    /// <summary>
    /// 房间配置
    /// </summary>
    public struct RoomConfig
    {
        /// <summary>
        /// 倍率
        /// </summary>
        public int Multiples { get; set; }

        /// <summary>
        /// 基础分
        /// </summary>
        public long BasePointPerMatch { get; set; }

        /// <summary>
        /// 房间最低门槛
        /// </summary>
        public long MinThreshold { get; set; }
    }
}

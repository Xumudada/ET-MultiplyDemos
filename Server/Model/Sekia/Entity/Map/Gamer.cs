using MongoDB.Bson.Serialization.Attributes;
using PF;

namespace ETModel
{
    [ObjectSystem]
    public class GamerAwakeSystem : AwakeSystem<Gamer, long>
    {
        public override void Awake(Gamer self, long userid)
        {
            self.Awake(userid);
        }
    }

    /// <summary>
    /// 房间玩家对象
    /// </summary>
    public sealed class Gamer : Entity
    {
        /// <summary>
        /// 来自数据库中的永久ID
        /// </summary>
        public long UserID { get; private set; }


        //-------------------------
        //5V5相关参数
        [BsonIgnore]
        public Vector3 Position { get; set; }
        public long HeroID;
        //-------------------------
        

        /// <summary>
        /// Gamer对应在Gate上的User的InstanceID
        /// 常规事务：更新客户端转发ActorID
        /// </summary>
        public long ActorIDofUser { get; set; }

        /// <summary>
        /// Gamer对应在Gate上Session的InstanceID 用于转发消息给Client
        /// </summary>
        public long ActorIDofClient { get; set; }

        /// <summary>
        /// 默认为假 Session断开/离开房间时触发离线
        /// </summary>
        public bool isOffline { get; set; }

        public void Awake(long userid)
        {
            this.UserID = userid;
        }

        public override void Dispose()
        {
            if(this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.UserID = 0;
            this.ActorIDofUser = 0;
            this.isOffline = false;
        }
    }
}

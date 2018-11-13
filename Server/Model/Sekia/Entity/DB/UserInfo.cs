using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ETModel
{
    [ObjectSystem]
    public class UserInfoAwakeSystem : AwakeSystem<UserInfo, string>
    {
        public override void Awake(UserInfo self, string name)
        {
            self.Awake(name);
        }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : Entity
    {
        //昵称
        public string UserName { get; set; }

        //等级
        public int UserLevel { get; set; }

        //余额
        public long Money { get; set; }

        //最多可创建3个角色
        public long CharacterID1 { get; set; }
        public long CharacterID2 { get; set; }
        public long CharacterID3 { get; set; }

        //public List<Ca>
        public void Awake(string name)
        {
            UserName = name;
            UserLevel = 1;
            Money = 10000;
            CharacterID1 = 0;
            CharacterID2 = 0;
            CharacterID3 = 0;
        }

    }
}

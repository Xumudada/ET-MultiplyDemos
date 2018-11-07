using MongoDB.Bson.Serialization.Attributes;

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

        public void Awake(string name)
        {
            UserName = name;
            UserLevel = 1;
            Money = 10000;
        }

    }
}

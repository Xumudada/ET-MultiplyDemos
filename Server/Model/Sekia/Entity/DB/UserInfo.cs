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

        //胜场
        public int UserLevel { get; set; }

        public void Awake(string name)
        {
            this.UserName = name;
            this.UserLevel = 1;
        }

    }
}

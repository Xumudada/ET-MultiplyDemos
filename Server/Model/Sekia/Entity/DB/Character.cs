namespace ETModel
{
    /// <summary>
    /// 角色信息 从数据库中读取
    /// </summary>
    public class Character : Entity
    {
        //角色名
        public string Name { get; set; }

        //角色等级
        public string Level { get; set; }

        //职业
        public string Career { get; set; }

    }
}
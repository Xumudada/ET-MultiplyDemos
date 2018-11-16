namespace ETModel
{
    [ObjectSystem]
    public class CharacterAwakeSystem : AwakeSystem<Character, long>
    {
        public override void Awake(Character self, long id)
        {
            self.Awake(id);
        }
    }

    /// <summary>
    /// 角色信息 从数据库中读取
    /// </summary>
    public class Character : Entity
    {
        public long UserID { get; private set; }

        //MMO金钱
        public int Money { get; set; }

        //未读邮件个数
        public int Mail { get; set; }

        //角色名
        public string Name { get; set; }

        //角色等级
        public int Level { get; set; }

        //职业编号
        public CareerType Career { get; set; }

        //宠物编号
        public PetType Pet { get; set; }

        //模型编号
        public SkeletonType Skeleton { get; set; }

        //武器编号
        public WeaponType Weapon { get; set; }

        //装备编号
        public HeadType Head { get; set; }
        public ChestType Chest { get; set; }
        public HandType Hand { get; set; }
        public FeetType Feet { get; set; }

        //区域编号
        public RegionType Region { get; set; }

        //坐标 *1000 保存为int 计算时换算为float
        public int X;
        public int Y;
        public int Z;

        public void Awake(long id)
        {
            this.UserID = id;
        }
    }
}
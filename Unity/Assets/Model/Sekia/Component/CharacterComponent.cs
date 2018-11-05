using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 用于管理角色的游戏行为
    /// </summary>
    public class CharacterComponent : Component
    {
        public int mobaID; //角色ID
        public bool isAlive = true; //是否存活
        public int startingHealth =100; //初始生命值
        public int currentHealth = 100; //当前生命值
        public int attack = 10; //攻击力
        public int group; //阵营 1蓝方 2红方 3中立
        public int type; //类型 1玩家 2NPC 3建筑
        public int route; //自动导航线路 1上路 2中路 3下路
        public float attackRange = 6f; //普攻范围 1代表Unity中1单位的宽度 
        public int attackSpeed = 1500; //普攻速度 普攻所需的毫秒数
        public float moveSpeed = 5f; //移动速度
        //public float viewRange = 10f; //统一视野范围
        public float characterWith = 1f; //角色模型宽度

        public float efficiencyTime = 1f; //技能效果持续时间
        public bool isAttackable = true; //是否可被攻击 在拔掉外层塔以前塔是无敌的
        public bool isInvisible = false; //是否为隐身状态 隐身状态只对盟友可见
        public AudioSource characterAudio; //音乐
        //public GameObject hit; //受击特效
        public GamerAnimatorComponent anime; //动画
        public List<int> skills = new List<int>(); //技能列表
        public List<int> items = new List<int>(); //背包中物品列表
        public List<int> buffs = new List<int>(); //buff列表
    }
}
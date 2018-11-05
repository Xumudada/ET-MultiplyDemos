using System.Collections.Generic;

namespace ETModel
{
    public class MobaControllerComponent : Component
    {
        //容器
        public List<int> HeroList = new List<int>();
        public List<long> Timer = new List<long>();

        public bool isGameStarted = false; //游戏是否已开始
        public long gameStartTime; //游戏开始时间
        public long lastFrameStartTime; //上一帧开始时间
        public long lastFrameCostTime; //上一帧花费时间
        public bool isGameOver = false; //游戏是否已结束

        //关卡设置
        public int wave = 1; //当前波次
        public long wildRebornTime = 70000; //野怪复活时间

        //倒计时定时器

    }
}

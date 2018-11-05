using ETModel;
using PF;

namespace ETHotfix
{
    [ObjectSystem]
    public class MobaControllerComponentUpdateSystem : UpdateSystem<MobaControllerComponent>
    {
        public override void Update(MobaControllerComponent self)
        {
            self.Update();
        }
    }

    public static class MobaControllerComponenttSystem
    {
        public static void Update(this MobaControllerComponent self)
        {
            if (self.isGameStarted) return;
            long timeNow = TimeHelper.ClientNow();
            self.lastFrameCostTime = timeNow - self.lastFrameStartTime;

            //复活野怪

            //复活英雄

            //
        }

        /// <summary>
        /// 刷新野怪
        /// </summary>
        public static async ETVoid SpawnWild(this MobaControllerComponent self)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            Moba5V5Room room = self.GetParent<Moba5V5Room>();
            await timerComponent.WaitAsync(30000);
            room.Broadcast(new A1011_SpawnMonster_M2C());
        }

        /// <summary>
        /// 刷新士兵
        /// </summary>
        public static async ETVoid SpawnArmy(this MobaControllerComponent self)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            long instanceId = self.InstanceId;
            Moba5V5Room room = self.GetParent<Moba5V5Room>();

            //刷新初始波士兵
            await timerComponent.WaitAsync(10000);
            room.Broadcast(new A1009_NewArmyWave_M2C());
            self.wave += 1;

            while (true)
            {
                await timerComponent.WaitAsync(35000); //士兵每波刷新周期
                if(self.InstanceId != instanceId) return;
                if (self.isGameOver) return;
                if (self.wave > 6)
                {
                    room.Broadcast(new A1009_NewArmyWave_M2C());
                }
                else //第7波开始刷新炮车
                {
                    room.Broadcast(new A1010_NewArmyWave_M2C());
                }
                self.wave += 1; //波次+1
            }
        }

        /// <summary>
        /// 房间满员时通知客户端加载场景
        /// </summary>
        /// <param name="self"></param>
        public static void RoomReady(this MobaControllerComponent self)
        {
            Moba5V5Room room = self.GetParent<Moba5V5Room>();
            
            //机器人设置为已准备
            for (int i = 0; i < room.gamers.Length; i++)
            {
                if (room.gamers[i].UserID == 0)
                {
                    room.isReadys[i] = true;
                }
            }

            //向玩家发送加载请求 等待玩家准备
            self.HeroList = BattleHelp.GetRandomTeam(10); //排除一个时：9, new List<long>() { mainId }
            A1004_CreateMoba5V5Secene_M2C message = new A1004_CreateMoba5V5Secene_M2C();
            for(int i=0;i<self.HeroList.Count;i++)
            {
                message.Gamers.Add(new GamerInfo() {
                    UserID = room.gamers[i].UserID,
                    HeroID = self.HeroList[i],
                    Index = i+1
                });
            }
            room.Broadcast(message);
        }

        /// <summary>
        /// 当收到玩家准备消息时 检查游戏是否可以开始
        /// </summary>
        /// <param name="self"></param>
        public static void CheckGameStart(this MobaControllerComponent self)
        {
            Moba5V5Room room = self.GetParent<Moba5V5Room>();

            bool isOK = true;

            for (int i = 0; i < room.isReadys.Length; i++)
            {
                //遍历所有准备状态 任何一个状态为false结果都是false
                if (room.isReadys[i] == false)
                {
                    isOK = false;
                }
            }

            if (isOK)
            {
                self.GameStart();
            }
        }

        /// <summary>
        /// 所有玩家已就绪游戏开始
        /// </summary>
        /// <param name="self"></param>
        public static void GameStart(this MobaControllerComponent self)
        {
            //正式开始游戏前的准备
            Moba5V5Room room = self.GetParent<Moba5V5Room>();
            Moba5V5Component moba = Game.Scene.GetComponent<Moba5V5Component>();

            //更改房间状态为游戏中
            moba.FreeRooms.Remove(room.Id);
            moba.GamingRooms.Add(room.Id, room);

            //更改玩家状态为游戏中
            for (int i = 0; i < room.gamers.Length; i++)
            {
                if (room.gamers[i].UserID != 0)
                {
                    Gamer gamer = room.gamers[i];
                    moba.Waiting.Remove(gamer.UserID);
                    moba.Playing.Add(gamer.UserID, room);
                }
            }

            //服务端加载游戏逻辑
            for (int i = 0; i < room.gamers.Length; i++)
            {
                room.gamers[i].AddComponent<GamerMoveComponent>();
                room.gamers[i].AddComponent<GamerPathComponent>();
                room.gamers[i].Position = new Vector3(-10, 0, -10);
            }

            //开始服务端游戏计时
            self.isGameStarted = true;
            self.gameStartTime = TimeHelper.ClientNow();
            self.lastFrameCostTime = self.gameStartTime;
            
            self.SpawnArmy().NoAwait(); //周期刷新士兵
            self.SpawnWild().NoAwait(); //刷新野怪
            room.Broadcast(new A1008_GameStart_M2C());
        }
    }
}
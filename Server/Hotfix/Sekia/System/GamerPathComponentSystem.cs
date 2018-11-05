using System.Collections.Generic;
using System.Threading;
using ETModel;
using PF;

namespace ETHotfix
{
    public static class GamerPathComponentSystem
    {
        /// <summary>
        /// 玩家寻路组件对外接口 消息处理方法中使用
        /// </summary>
        public static async ETVoid MoveTo(this GamerPathComponent self, Vector3 target)
        {
            if ((self.Target - target).magnitude < 0.1f)
            {
                return;
            }

            self.Target = target;

            Gamer gamer = self.GetParent<Gamer>();

            //全局寻路组件 用于计算路径
            PathfindingComponent pathfindingComponent = Game.Scene.GetComponent<PathfindingComponent>();
            self.ABPath = ComponentFactory.Create<ETModel.ABPath, Vector3, Vector3>(gamer.Position,
                new Vector3(target.x, target.y, target.z)); //创建路径 寻路组件的接口
            pathfindingComponent.Search(self.ABPath);
            //Log.Debug($"寻路查询结果: {self.ABPath.Result.ListToString()}");

            self.CancellationTokenSource?.Cancel(); //取消当前寻路
            self.CancellationTokenSource = new CancellationTokenSource();
            await self.MoveAsync(self.ABPath.Result); //开始移动
            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        /// <summary>
        /// 移动任务处理方法
        /// </summary>
        public static async ETTask MoveAsync(this GamerPathComponent self, List<Vector3> path)
        {
            Gamer gamer = self.GetParent<Gamer>();
            if (path.Count == 0)
            {
                return;
            }
            // 第一个点是unit的当前位置，所以不用发送
            for (int i = 1; i < path.Count; ++i)
            {
                // 每移动3个点发送下3个点给客户端
                if (i % 3 == 1)
                {
                    self.BroadcastPath(path, i, 3);
                }
                Vector3 v3 = path[i];
                await gamer.GetComponent<GamerMoveComponent>().MoveToAsync(v3, self.CancellationTokenSource.Token);
            }
        }

        /// <summary>
        /// 玩家移动房间广播 index当前移动进度 offset发送坐标的数量
        /// </summary>
        public static void BroadcastPath(this GamerPathComponent self, List<Vector3> path, int index, int offset)
        {
            Gamer gamer = self.GetParent<Gamer>();
            Vector3 unitPos = gamer.Position;
            A1006_PathfindingResult_M2C pathfindingResult = new A1006_PathfindingResult_M2C();
            pathfindingResult.X = unitPos.x; //当前位置
            pathfindingResult.Y = unitPos.y;
            pathfindingResult.Z = unitPos.z;
            pathfindingResult.UserID = gamer.UserID;

            for (int i = 0; i < offset; ++i) //预测的坐标个数 先广播后移动 不包含当前坐标
            {
                if (index + i >= self.ABPath.Result.Count) //预测坐标的序列不能大于坐标总数
                {
                    break;
                }
                Vector3 v = self.ABPath.Result[index + i];
                pathfindingResult.Xs.Add(v.x);
                pathfindingResult.Ys.Add(v.y);
                pathfindingResult.Zs.Add(v.z);
            }

            //找到玩家所在房间进行广播
            Moba5V5Room room = Game.Scene.GetComponent<Moba5V5Component>().GetGamingRoom(gamer);
            room.Broadcast(pathfindingResult);
        }
    }
}
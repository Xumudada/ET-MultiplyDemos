using ETModel;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;

namespace ETHotfix
{
    public static class GamerPathComponentSystem
    {
        /// <summary>
        /// 寻路组件对外接口
        /// </summary>
        public static async ETVoid StartMove(this GamerPathComponent self, A1006_PathfindingResult_M2C message)
        {
            // 取消之前的移动协程
            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();

            self.Path.Clear();
            for (int i = 0; i < message.Xs.Count; ++i)
            {
                self.Path.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
            }
            self.ServerPos = new Vector3(message.X, message.Y, message.Z); //设置服务端当前坐标

            await self.StartMove(self.CancellationTokenSource.Token); //执行移动
            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        /// <summary>
        /// 客户端移动操作
        /// </summary>
        public static async ETTask StartMove(this GamerPathComponent self, CancellationToken cancellationToken)
        {
            for (int i = 0; i < self.Path.Count; ++i)
            {
                Vector3 v = self.Path[i]; //下一个预测点

                float speed = 5;

                if (i == 0) //对第一个坐标进行特殊处理
                {
                    // 矫正移动速度
                    Vector3 clientPos = self.GetParent<Gamer>().Position;
                    float serverf = (self.ServerPos - v).magnitude;
                    if (serverf > 0.1f) //预计路程
                    {
                        float clientf = (clientPos - v).magnitude; //实际路程
                        speed = clientf / serverf * speed; //相对速度
                    }
                }

                self.Entity.GetComponent<GamerTurnComponent>().Turn(v);
                await self.Entity.GetComponent<GamerMoveComponent>().MoveToAsync(v, speed, cancellationToken);
            }

            //移动结束后的操作
            self.Entity.GetComponent<GamerAnimatorComponent>().SetIntValue("Speed", 0);
        }
    }
}

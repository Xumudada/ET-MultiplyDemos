using System;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class GamerMoveComponentUpdateSystem : UpdateSystem<GamerMoveComponent>
    {
        public override void Update(GamerMoveComponent self)
        {
            self.Update();
        }
    }

    public class GamerMoveComponent : Component
    {
        public Vector3 Target;

        // 开启移动协程的时间
        public long StartTime;

        // 开启移动协程的Unit的位置
        public Vector3 StartPos;

        public long needTime;

        // 当前的移动速度
        public float Speed = 5;

        public ETTaskCompletionSource moveTcs;


        public void Update()
        {
            if (this.moveTcs == null)
            {
                return;
            }

            Gamer gamer = this.GetParent<Gamer>();
            long timeNow = TimeHelper.Now();

            if (timeNow - this.StartTime >= this.needTime)
            {
                gamer.Position = this.Target;
                ETTaskCompletionSource tcs = this.moveTcs;
                this.moveTcs = null;
                tcs.SetResult();
                return;
            }

            float amount = (timeNow - this.StartTime) * 1f / this.needTime;
            //插值 Vector3.Lerp返回已走过的百分比的路径 amount为已走过的百分比
            gamer.Position = Vector3.Lerp(this.StartPos, this.Target, amount);
        }

        public ETTask MoveToAsync(Vector3 target, float speedValue, CancellationToken cancellationToken)
        {
            Gamer gamer = this.GetParent<Gamer>();

            if ((target - this.Target).magnitude < 0.1f)
            {
                return ETTask.CompletedTask;
            }

            this.Target = target;


            this.StartPos = gamer.Position;
            this.StartTime = TimeHelper.Now();
            float distance = (this.Target - this.StartPos).magnitude;
            if (Math.Abs(distance) < 0.1f)
            {
                return ETTask.CompletedTask;
            }

            this.needTime = (long)(distance / this.Speed * 1000);

            this.moveTcs = new ETTaskCompletionSource();

            //注册取消动作 移动结束并不会执行本段
            cancellationToken.Register(() =>
            {
                this.moveTcs = null;
            });
            return this.moveTcs.Task;
        }
    }
}
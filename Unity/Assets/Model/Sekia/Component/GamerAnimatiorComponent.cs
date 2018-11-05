using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    public enum GamerMotionType
    {
        None,
        Idle,
        Run,
    }

    [ObjectSystem]
    public class GamerAnimatorComponentAwakeSystem : AwakeSystem<GamerAnimatorComponent>
    {
        public override void Awake(GamerAnimatorComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class GamerAnimatorComponentUpdateSystem : UpdateSystem<GamerAnimatorComponent>
    {
        public override void Update(GamerAnimatorComponent self)
        {
            self.Update();
        }
    }

    public class GamerAnimatorComponent : Component
    {
        public Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>();
        public HashSet<string> Parameter = new HashSet<string>();

        public GamerMotionType gamerMotionType;
        public float MontionSpeed;
        public bool isStop;
        public float stopSpeed; //动画暂停前的动作
        public Animator Animator;

        public void Awake()
        {
            Animator animator = this.GetParent<Gamer>().GameObject.GetComponent<Animator>();

            if (animator == null)
            {
                return;
            }

            if (animator.runtimeAnimatorController == null)
            {
                return;
            }

            if (animator.runtimeAnimatorController.animationClips == null)
            {
                return;
            }
            //Log.Debug("已确认Animator："+this.GetParent<Gamer>().GameObject.name);
            this.Animator = animator;
            foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
            {
                this.animationClips[animationClip.name] = animationClip;
            }
            //Log.Debug("包含动画片段数量个数：" + this.animationClips.Count.ToString());
            foreach (AnimatorControllerParameter animatorControllerParameter in animator.parameters)
            {
                this.Parameter.Add(animatorControllerParameter.name);
            }
            //Log.Debug("包含动画状态变量个数：" + this.Parameter.Count.ToString());
            //foreach(var a in this.Parameter)
            //{
            //    Log.Debug("动画变量名：" + a);
            //}
        }

        public void Update()
        {
            if (this.isStop)
            {
                return;
            }

            if (this.gamerMotionType == GamerMotionType.None)
            {
                return;
            }

            try
            {
                //this.Animator.SetFloat("MotionSpeed", this.MontionSpeed); //设置一次参数

                this.Animator.SetTrigger(this.gamerMotionType.ToString()); //触发一次

                //this.MontionSpeed = 1; //默认1秒一次
                this.gamerMotionType = GamerMotionType.None; //默认无操作
            }
            catch (Exception ex)
            {
                throw new Exception($"动作播放失败: {this.gamerMotionType}", ex);
            }
        }

        //是否包含逻辑变量
        public bool HasParameter(string parameter)
        {
            return this.Parameter.Contains(parameter);
        }

        public void PlayInTime(GamerMotionType motionType, float time)
        {
            AnimationClip animationClip;
            if (!this.animationClips.TryGetValue(motionType.ToString(), out animationClip))
            {
                throw new Exception($"找不到该动作: {motionType}");
            }

            float motionSpeed = animationClip.length / time;
            if (motionSpeed < 0.01f || motionSpeed > 1000f)
            {
                Log.Error($"motionSpeed数值异常, {motionSpeed}, 此动作跳过");
                return;
            }
            this.gamerMotionType = motionType;
            this.MontionSpeed = motionSpeed;
        }

        public void Play(GamerMotionType motionType, float motionSpeed = 1f)
        {
            if (!this.HasParameter(motionType.ToString()))
            {
                return;
            }
            this.gamerMotionType = motionType;
            this.MontionSpeed = motionSpeed;
        }

        public float AnimationTime(GamerMotionType motionType)
        {
            AnimationClip animationClip;
            if (!this.animationClips.TryGetValue(motionType.ToString(), out animationClip))
            {
                throw new Exception($"找不到该动作: {motionType}");
            }
            return animationClip.length;
        }

        public void PauseAnimator()
        {
            if (this.isStop)
            {
                return;
            }
            this.isStop = true;

            if (this.Animator == null)
            {
                return;
            }
            this.stopSpeed = this.Animator.speed;
            this.Animator.speed = 0;
        }

        public void RunAnimator()
        {
            if (!this.isStop)
            {
                return;
            }

            this.isStop = false;

            if (this.Animator == null)
            {
                return;
            }
            this.Animator.speed = this.stopSpeed;
        }

        /// <summary>
        /// 设置动画状态变量
        /// </summary>
        public void SetBoolValue(string name, bool state)
        {
            if (!this.HasParameter(name))
            {
                return;
            }

            this.Animator.SetBool(name, state);
        }

        public void SetFloatValue(string name, float state)
        {
            if (!this.HasParameter(name))
            {
                return;
            }

            this.Animator.SetFloat(name, state);
        }

        public void SetIntValue(string name, int value)
        {
            if (!this.HasParameter(name))
            {
                Log.Debug("不存在这个状态");
                return;
            }

            this.Animator.SetInteger(name, value);
        }

        public void SetTrigger(string name)
        {
            if (!this.HasParameter(name))
            {
                return;
            }

            this.Animator.SetTrigger(name);
        }

        public void SetAnimatorSpeed(float speed)
        {
            this.stopSpeed = this.Animator.speed;
            this.Animator.speed = speed;
        }

        public void ResetAnimatorSpeed()
        {
            this.Animator.speed = this.stopSpeed;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            this.animationClips = null;
            this.Parameter = null;
            this.Animator = null;
        }
    }
}
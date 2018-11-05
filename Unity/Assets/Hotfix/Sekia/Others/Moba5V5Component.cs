using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class Moba5V5ComponentAwakeSystem : AwakeSystem<Moba5V5Component>
    {
        public override void Awake(Moba5V5Component self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class Moba5V5ComponentUpdateSystem : UpdateSystem<Moba5V5Component>
    {
        public override void Update(Moba5V5Component self)
        {
            self.Update();
        }
    }

    /// <summary>
    /// 管理本地游戏进程和处理玩家键盘操作
    /// </summary>
    public class Moba5V5Component : Component
    {
        public static Moba5V5Component instance;

        //计时器
        public long gameCostTime = 0; //本地游戏持续时间
        public bool isGameStarted = false; //游戏是否已开始
        public long lastFrameStartTime;
        public long lastFrameCostTime;

        //任务处理
        public Dictionary<int, Gamer> mobaIDGamers = new Dictionary<int, Gamer>(); //全部角色的容器
        public TouchHandler touchHandler; //按键处理方法
        private int _mobaid = 0;
        public int MobaID //下一个出生的角色ID 使用后自动+1
        {
            get
            {
                _mobaid += 1;
                return _mobaid;
            }
            private set
            {
                _mobaid = value;
            }
        }
        public Gamer myGamer; //本地玩家
        public Gamer target; //当前锁定的目标
        Moba5V5UIComponent moba5V5UI; //战斗UI

        //关于移动
        public Vector3 movement;  //存储玩家移动方向
        public bool isMoving = false;
        public long StartRunTime; //开始移动的时间
        public float xoff = 0;
        public float zoff = 0;
        public float speed = 5f; //玩家移动速度
        public bool canMove = true; //是否可以移动

        public void Awake()
        {
            instance = this;
            touchHandler = TouchHandler.GetInstance(); //按键消息处理
            touchHandler.OnKeyChanged = OnTouch;
            lastFrameStartTime = TimeHelper.Now(); //本地计时
            moba5V5UI = Game.Scene.GetComponent<UIComponent>().Get(UIType.Moba5V5UI).GetComponent<Moba5V5UIComponent>();
        }
        
        public void Update()
        {
            if (myGamer == null) //需要先绑定本地玩家才能控制角色
                return;

            long time = TimeHelper.ClientNow();
            lastFrameCostTime = time - lastFrameStartTime; //计算本地一帧的时间
            lastFrameStartTime = time;
            if (isGameStarted && moba5V5UI != null)
            {
                gameCostTime += lastFrameCostTime; //计算本地游戏时间
                moba5V5UI.gameTime.text = $"{(int)(gameCostTime / 60000)}:{(int)((gameCostTime / 1000) % 60)}";
            }

            CheckTarget(); //检查锁定目标是否超出距离

            #region 响应用户操作
            //PC上WASD按键移动 手机摇杆移动
#if UNITY_STANDALONE
            xoff = Input.GetAxis("Horizontal"); //取横轴输入值 按A键时为-1 按D键时为1
            zoff = Input.GetAxis("Vertical"); //取纵轴输入值 按S键时为-1 按W键时为1
#elif UNITY_ANDROID
            xoff = up.upPosition.x;
            zoff = up.upPosition.y;
#elif UNITY_IPHONE
            xoff = up.upPosition.x;
            zoff = up.upPosition.y;
#endif
            if (xoff != 0 || zoff != 0)
            {
                if (isMoving == false) //开始移动 只执行一次
                    touchHandler.Touch(TOUCH_KEY.Run);
                if (time - StartRunTime > 200 && isMoving == true) //持续移动 反复执行
                    touchHandler.OnKeyChanged(TouchEvent.Press, TOUCH_KEY.Run);

                if (canMove) //在可以移动的情况下才执行
                {
                    isMoving = true;
                    movement.Set(xoff, 0f, zoff);
                    movement = movement.normalized + myGamer.Position; //计算移动方向
                    myGamer.GetComponent<GamerTurnComponent>().Turn(movement);
                    myGamer.Position = Vector3.Lerp(myGamer.Position, movement, speed * lastFrameCostTime / 1000);
                }
            }
            else
            {
                if (isMoving == true) //停止移动
                {
                    isMoving = false;
                    touchHandler.Release(TOUCH_KEY.Run);
                }
            }

            //键盘技能按键消息 不处理也释放键位
            if (Input.GetKeyDown(KeyCode.Alpha1)) touchHandler.Touch(TOUCH_KEY.Skill1);
            if (Input.GetKeyUp(KeyCode.Alpha1)) touchHandler.Release(TOUCH_KEY.Skill1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) touchHandler.Touch(TOUCH_KEY.Skill2);
            if (Input.GetKeyUp(KeyCode.Alpha2)) touchHandler.Release(TOUCH_KEY.Skill2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) touchHandler.Touch(TOUCH_KEY.Skill3);
            if (Input.GetKeyUp(KeyCode.Alpha3)) touchHandler.Release(TOUCH_KEY.Skill3);
            if (Input.GetKeyDown(KeyCode.Alpha4)) touchHandler.Touch(TOUCH_KEY.Skill4);
            if (Input.GetKeyUp(KeyCode.Alpha4)) touchHandler.Release(TOUCH_KEY.Skill4);
            if (Input.GetKeyDown(KeyCode.Space)) touchHandler.Touch(TOUCH_KEY.Attack); //攻击按钮
            if (Input.GetKeyUp(KeyCode.Space)) touchHandler.Release(TOUCH_KEY.Attack);
            if (Input.GetKeyDown(KeyCode.F)) touchHandler.Touch(TOUCH_KEY.Summon1); //召唤师技能1
            if (Input.GetKeyDown(KeyCode.F)) touchHandler.Release(TOUCH_KEY.Summon1);
            if (Input.GetKeyDown(KeyCode.G)) touchHandler.Touch(TOUCH_KEY.Summon2); //召唤师技能2
            if (Input.GetKeyDown(KeyCode.G)) touchHandler.Release(TOUCH_KEY.Summon2);
            if (Input.GetKeyDown(KeyCode.B)) touchHandler.Touch(TOUCH_KEY.Summon3); //回城
            if (Input.GetKeyDown(KeyCode.G)) touchHandler.Release(TOUCH_KEY.Summon3);
            if (canMove && !isMoving) touchHandler.Touch(TOUCH_KEY.Idle); //休息
            #endregion
        }

        public void OnTouch(TouchEvent action, TOUCH_KEY key)
        {
            switch (key)
            {
                case TOUCH_KEY.Attack:
                    switch (action)
                    {
                        case TouchEvent.Down: //持续按住按键不会触发按下
                            if (LockAttackTarget())
                            {
                                int targetMobaID = target.GetComponent<CharacterComponent>().mobaID;
                                //SessionComponent.Instance.Session.Send(new A1013_Attack_C2M() { MobaID = targetMobaID });
                                target.GetComponent<CharacterComponent>().OnTargeted();
                                myGamer.GetComponent<CharacterComponent>().OnAttack(targetMobaID, 1);
                            }
                            break;
                        case TouchEvent.Up:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Idle:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Run: //关于移动
                    switch (action)
                    {
                        case TouchEvent.Down: //开始移动
                            break;
                        case TouchEvent.Up: //停止移动
                            break;
                        case TouchEvent.Press: //持续移动
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Skill1:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        case TouchEvent.Up:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Skill2:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        case TouchEvent.Up:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Skill3:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        case TouchEvent.Up:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Skill4:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        case TouchEvent.Up:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Summon1:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Summon2:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        default:
                            break;
                    }
                    break;
                case TOUCH_KEY.Summon3:
                    switch (action)
                    {
                        case TouchEvent.Down:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    throw new Exception("没有注册这个按键");
            }
        }
        
        public void SendNewPosition()
        {
            A1005_ClickMap_C2M message = new A1005_ClickMap_C2M();
            message.X = myGamer.Position.x;
            message.Y = myGamer.Position.y;
            message.Z = myGamer.Position.z;
            SessionComponent.Instance.Session.Send(message);
        }

        //锁定一个普攻范围内目标 优先英雄 适用于技能 不可选择建筑
        public bool LockHeroTarget()
        {
            CharacterComponent myself = myGamer.GetComponent<CharacterComponent>();
            List<Gamer> inViewGamers = new List<Gamer>();
            foreach (var a in mobaIDGamers.Values)
            {
                //排除法 去除本地玩家 隐身中的玩家 无敌的玩家 同阵营玩家
                CharacterComponent enemy = a.GetComponent<CharacterComponent>();
                if (enemy == null) continue;
                if (!enemy.isAlive) continue;
                if (a.UserID == myGamer.UserID) continue;
                if (enemy.type == 3) continue;
                if (enemy.isInvisible) continue;
                if (!enemy.isAttackable) continue;
                if (enemy.group == myself.group) continue;
                if (Vector3.Distance(a.Position, myGamer.Position) > myself.attackRange) continue;
                if (enemy.type == 1) //优先返回英雄目标
                {
                    target = a;
                    return true;
                }
                inViewGamers.Add(a);
            }

            if (inViewGamers.Count == 0) return false;

            //使用比较器选出最近的角色

            target = inViewGamers[0];
            return true;
        }

        //锁定一个普通攻击范围内的目标 可选择建筑
        public bool LockAttackTarget()
        {
            CharacterComponent myself = myGamer.GetComponent<CharacterComponent>();
            List<Gamer> inViewGamers = new List<Gamer>();
            foreach(var a in mobaIDGamers.Values)
            {
                //排除法 去除本地玩家 隐身中的玩家 无敌的玩家 同阵营玩家
                CharacterComponent enemy = a.GetComponent<CharacterComponent>();
                if (enemy == null) continue;
                if (!enemy.isAlive) continue;
                if (a.UserID == myGamer.UserID) continue;
                if (enemy.isInvisible) continue;
                if (!enemy.isAttackable) continue;
                if (enemy.group == myself.group) continue;
                if (Vector3.Distance(a.Position, myGamer.Position) > myself.attackRange) continue;
                if (enemy.type == 1) //优先返回英雄目标
                {
                    target = a;
                    return true;
                }
                else if (enemy.type == 3) //次优先返回建筑目标
                {
                    target = a;
                    return true;
                }
                inViewGamers.Add(a);
            }
            
            if (inViewGamers.Count == 0) return false;

            //使用比较器选出最近的角色

            target = inViewGamers[0];
            return true;
        }

        public void CheckTarget()
        {
            if (target == null) return;
            CharacterComponent myself = myGamer.GetComponent<CharacterComponent>();
            if (Vector3.Distance(target.Position, myGamer.Position) > myself.attackRange)
            {
                target.GetComponent<CharacterComponent>().OnUnTargeted();
                target = null;
            }
        }
    }
}
using UnityEngine;
using FairyGUI;
using System;

namespace ETModel
{
    [ObjectSystem]
    public class SelectCharacterComponentAwakeSystem : AwakeSystem<SelectCharacterComponent, A0008_GetUserInfo_G2C>
    {
        public override void Awake(SelectCharacterComponent self, A0008_GetUserInfo_G2C message)
        {
            self.Awake(message);
        }
    }

    public class SelectCharacterComponent : Component
    {
        A0008_GetUserInfo_G2C messageUser; //帐号角色消息
        public FUI Controller; //控制器
        public Controller SeatSelect; //角色编号选择
        public Controller NoneorExist1; //第x个角色是否存在
        public Controller NoneorExist2;
        public Controller NoneorExist3;
        public FUI Holder1; //模型容器
        public FUI Holder2;
        public FUI Holder3;
        public FUI Money1; //游戏币数量
        public FUI Money2;
        public FUI Money3;
        public FUI Mail1; //未读邮件数量
        public FUI Mail2;
        public FUI Mail3;
        public FUI Level1; //字符串： Lv.1 XXX
        public FUI Level2;
        public FUI Level3;
        public FUI Location1; //字符串： Career Region
        public FUI Location2;
        public FUI Location3;
        
        //点击选项后等待服务器返回消息
        public bool isWaiting;

        public void Awake(A0008_GetUserInfo_G2C message)
        {
            //保存帐号信息
            messageUser = message;

            //获取父级"包"
            FUI SelectCharacter = this.GetParent<FUI>();
            Controller = SelectCharacter.Get("Controller"); //Get有先后顺序
            SeatSelect = Controller.GObject.asCom.GetController("c1");
            NoneorExist1 = Controller.GObject.asCom.GetController("c2");
            NoneorExist2 = Controller.GObject.asCom.GetController("c3");
            NoneorExist3 = Controller.GObject.asCom.GetController("c4");
            Holder1 = Controller.Get("Holder1");
            Holder2 = Controller.Get("Holder2");
            Holder3 = Controller.Get("Holder3");
            Money1 = Controller.Get("Money1");
            Money2 = Controller.Get("Money2");
            Money3 = Controller.Get("Money3");
            Mail1 = Controller.Get("Mail1");
            Mail2 = Controller.Get("Mail2");
            Mail3 = Controller.Get("Mail3");
            Level1 = Controller.Get("Level1");
            Level2 = Controller.Get("Level2");
            Level3 = Controller.Get("Level3");
            Location1 = Controller.Get("Location1");
            Location2 = Controller.Get("Location2");
            Location3 = Controller.Get("Location3");

            //设置当前激活序列 由上次登陆游戏/注册的角色决定
            SeatSelect.SetSelectedIndex(messageUser.LastPlay - 1);

            //加载帐号设置 形象/金钱/邮件/等级/位置
            OnLoadCharacters(messageUser.Characters[0], Holder1, NoneorExist1, Money1, Mail1, Level1, Location1);
            OnLoadCharacters(messageUser.Characters[1], Holder2, NoneorExist2, Money2, Mail2, Level2, Location2);
            OnLoadCharacters(messageUser.Characters[2], Holder3, NoneorExist3, Money3, Mail3, Level3, Location3);

            //添加事件
            Controller.Get("Select1").GObject.asButton.onClick.Add(OnSelect1); //选择一个角色位置 进入游戏或创建角色
            Controller.Get("Select2").GObject.asButton.onClick.Add(OnSelect2);
            Controller.Get("Select3").GObject.asButton.onClick.Add(OnSelect3);

            Log.Debug("加载完选择角色界面");
        }

        //加载3个角色空位中的人物形象
        public void OnLoadCharacters(CharacterInfo message, FUI holder, Controller controller, FUI money, FUI mail, FUI level, FUI location)
        {
            if(message.Level == 0)
            {
                controller.SetSelectedIndex(0);
            }
            else
            {
                controller.SetSelectedIndex(1);
                //获得资源设置
                string skeleton = SekiaHelper.GetSkeletonName(message.Skeleton);
                string weapon = SekiaHelper.GetWeaponName(message.Weapon);
                string head = SekiaHelper.GetHeadName(message.Head);
                string chest = SekiaHelper.GetChestName(message.Chest);
                string hand = SekiaHelper.GetHandName(message.Hand);
                string feet = SekiaHelper.GetFeetName(message.Feet);

                //设置模型到指定位置
                GameObject demo = SekiaHelper.CreateCharacter(skeleton, weapon, head, chest, hand, feet);
                demo.transform.localPosition = new Vector3(30, -125, 1000); //模型的原点在脚下 向下位移半个身高 向右位移1/4身宽
                //Log.Debug($"Holder位置 X：{holder.GObject.x}  Y：{holder.GObject.y}"); //编辑器中的坐标 以左上角为原点 Y为正值
                demo.transform.localScale = new Vector3(125, 125, 125); //大小
                demo.transform.localEulerAngles = new Vector3(0, 180, 0); //角度
                GoWrapper wrapper = new GoWrapper(demo);
                holder.GObject.asGraph.SetNativeObject(wrapper);

                //设置各项参数
                money.GObject.asTextField.text = message.Money.ToString();
                mail.GObject.asTextField.text = message.Mail.ToString();
                level.GObject.asTextField.text = $"Lv.{message.Level} {message.Name}";
                location.GObject.asTextField.text = $"{Enum.GetName(message.Career.GetType(), message.Career)}·{Enum.GetName(message.Region.GetType(), message.Region)}";
            }
        }

        //在3个空位处各有一个透明的大按钮
        public void OnSelect1()
        {
            OnSelect(0);
        }

        public void OnSelect2()
        {
            OnSelect(1);
        }

        public void OnSelect3()
        {
            OnSelect(2);
        }

        public void OnSelect(int index)
        {
            //等待响应
            if (this.isWaiting || this.IsDisposed)
            {
                return;
            }
            this.isWaiting = true;

            if (messageUser.Characters[index].Level == 0)
            {
                //有空闲角色位置 进入角色注册界面
                CreateCharacterFactory.Create(messageUser);
                CreateCharacterComponent creater = Game.Scene.GetComponent<FUIComponent>().Get(FUIType.CreateCharacter).GetComponent<CreateCharacterComponent>();
                creater.Seat = index + 1;
                Log.Debug("创建更多角色 位置：" + creater.Seat.ToString());

                Game.EventSystem.Run(EventIdType.SelectCharacterFinish);
            }
            else
            {
                Log.Debug("等待进入游戏世界");
                //请求使用指定角色进入游戏
                //SekiaHelper.Register(this.AccountInput.Get("Input").GObject.asTextInput.text, this.PasswordInput.Get("Input").GObject.asTextInput.text).NoAwait();
            }
        }
        
        //退出界面需要重置所有元件属性
        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            Log.Debug("退出选择角色界面");
            Controller.Get("Select1").GObject.asButton.onClick.Remove(OnSelect1);
            Controller.Get("Select2").GObject.asButton.onClick.Remove(OnSelect2);
            Controller.Get("Select3").GObject.asButton.onClick.Remove(OnSelect3);
            Controller.Get("Select1").Dispose();
            Controller.Get("Select2").Dispose();
            Controller.Get("Select3").Dispose();

            messageUser = null;
            Controller.Dispose();
            SeatSelect = null;
            NoneorExist1 = null;
            NoneorExist2 = null;
            NoneorExist3 = null;
            Holder1.Dispose();
            Holder2.Dispose();
            Holder3.Dispose();
            Money1.Dispose();
            Money2.Dispose();
            Money3.Dispose();
            Mail1.Dispose();
            Mail2.Dispose();
            Mail3.Dispose();
            Level1.Dispose();
            Level2.Dispose();
            Level3.Dispose();
            Location1.Dispose();
            Location2.Dispose();
            Location3.Dispose();
            isWaiting = false;
        }
    }
}

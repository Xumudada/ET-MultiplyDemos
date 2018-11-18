using UnityEngine;
using FairyGUI;

namespace ETModel
{
    [ObjectSystem]
    public class CreateCharacterComponentAwakeSystem : AwakeSystem<CreateCharacterComponent, A0008_GetUserInfo_G2C>
    {
        public override void Awake(CreateCharacterComponent self, A0008_GetUserInfo_G2C message)
        {
            self.Awake(message);
        }
    }

    public class CreateCharacterComponent : Component
    {
        //创建角色界面 包括职业/外形可供选择
        public FUI Holder; //模型容器
        GameObject Demo; //玩家角色预览
        GoWrapper Wrapper; //UI用于包装3D物体的容器
        bool isCreateComplete; //是否加载完成
        public bool isCreatingCharacter; //是否正在等待服务器回复消息
        A0008_GetUserInfo_G2C messageUser; //帐号角色消息

        public FUI Controller; //控制器
        public Controller Gender; //模型编号选择
        public Controller Career; //职业编号选择
        public Controller Step; //步骤
        
        public FUI NameInput; //角色名输入框
        public FUI Prompt; //提示消息

        public int Seat; //当前创建角色界面对应的位置 由外部设置
        
        public void Awake(A0008_GetUserInfo_G2C message)
        {
            //保存帐号信息
            messageUser = message;

            //获取父级"包"
            FUI CreateCharacter = this.GetParent<FUI>();
            Controller = CreateCharacter.Get("Controller"); //Get有先后顺序
            Gender = Controller.GObject.asCom.GetController("c1");
            Career = Controller.GObject.asCom.GetController("c2");
            Step = Controller.GObject.asCom.GetController("c3");
            Holder = CreateCharacter.Get("Holder");
            NameInput = Controller.Get("NameInput");
            Prompt = Controller.Get("Prompt");

            //加载默认模型 男/战士
            OnControllerChanged();

            //添加事件
            //Gender.selectedIndex = 1; //设置被选中的页面
            //Gender.SetSelectedIndex(1); //设置被选中的页面而不触发Change事件
            Controller.Get("Back").GObject.asButton.onClick.Add(OnBack); //返回角色列表或者退出游戏
            Controller.Get("Complete").GObject.asButton.onClick.Add(OnCheckCreate); //检验角色名称是否合法
            Gender.onChanged.Add(OnControllerChanged);
            Career.onChanged.Add(OnControllerChanged);
            //aObject.OnGearStop.Add(OnGearStop); //缓动结束通知

            isCreateComplete = true;
            Log.Debug("加载完创建角色界面");
        }

        //切换身体部分模型事件
        public void OnControllerChanged()
        {
            //默认设置
            string skeleton = "ch_pc_hou_ZhanShi";
            string weapon = "ch_we_one_hou_004";
            string head = "ch_pc_hou_004_tou";
            string chest = "ch_pc_hou_004_shen";
            string hand = "ch_pc_hou_004_shou";
            string feet = "ch_pc_hou_004_jiao";
            
            //Demo被销毁 Weapon还在
            if(isCreateComplete)
            {
                UnityEngine.Object.Destroy(Wrapper.wrapTarget);
                UnityEngine.Object.Destroy(Demo);
            }
            int genderindex = Gender.selectedIndex;
            string gender = "";
            switch (genderindex)
            {
                case 0: //男
                    gender = "男";
                    break;
                case 1: //女
                    gender = "女";
                    head = "ch_pc_hou_006_tou";
                    chest = "ch_pc_hou_006_shen";
                    hand = "ch_pc_hou_006_shou";
                    feet = "ch_pc_hou_006_jiao";
                    break;
                default:
                    break;
            }

            int careerindex = Career.selectedIndex;
            switch (careerindex)
            {
                case 0: //战士
                    Log.Debug(gender + "战士");
                    break;
                case 1: //法师
                    Log.Debug(gender + "法师");
                    skeleton = "ch_pc_hou_FaShi";
                    weapon = "ch_we_one_hou_006";
                    break;
                default:
                    break;
            }

            Demo = SekiaHelper.CreateCharacter(skeleton, weapon, head, chest, hand, feet);
            Demo.transform.localPosition = new Vector3(30, -125, 1000); //位置
            Demo.transform.localScale = new Vector3(125, 125, 125); //大小
            Demo.transform.localEulerAngles = new Vector3(0, 180, 0); //角度
            Wrapper = new GoWrapper(Demo);
            Holder.GObject.asGraph.SetNativeObject(Wrapper);

            //或者通过更新GoWrapper包装对象
            //参考：http://www.fairygui.com/guide/unity/insert3d.html
        }

        //返回角色界面或者返回登陆界面
        public void OnBack()
        {
            if(messageUser.Characters.Count == 0)
            {
                SekiaLoginFactory.Create();
                Game.EventSystem.Run(EventIdType.CreateCharacterFinish);
            }
            else
            {
                SelectCharacterFactory.Create(messageUser);
                Game.EventSystem.Run(EventIdType.CreateCharacterFinish);
            }
        }

        //检测角色名称是否合法
        public void OnCheckCreate()
        {
            if (this.isCreatingCharacter || this.IsDisposed)
            {
                return;
            }
            this.isCreatingCharacter = true;
            SekiaHelper.CreateNewCharacter(Seat, NameInput.Get("Input").GObject.asTextInput.text, Gender.selectedIndex, Career.selectedIndex).NoAwait();
        }
        
        //退出界面需要重置所有元件属性
        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            Log.Debug("退出创建角色界面");
            Controller.Get("Back").GObject.asButton.onClick.Remove(OnBack);
            Controller.Get("Complete").GObject.asButton.onClick.Remove(OnCheckCreate);
            Controller.Get("Back").Dispose();
            Controller.Get("Complete").Dispose();

            Holder.Dispose();
            Demo = null;
            Wrapper = null;
            isCreateComplete = false;
            isCreatingCharacter = false;
            messageUser = null;
            Controller.Dispose();
            Gender = null;
            Career = null;
            Step = null;
            NameInput.Dispose();
            Prompt.Dispose();
            Seat = 0;
        }
    }
}

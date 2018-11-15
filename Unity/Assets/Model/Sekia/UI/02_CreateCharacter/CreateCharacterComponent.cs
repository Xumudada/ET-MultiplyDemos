using UnityEngine;
using FairyGUI;

namespace ETModel
{
    [ObjectSystem]
    public class CreateCharacterComponentAwakeSystem : AwakeSystem<CreateCharacterComponent>
    {
        public override void Awake(CreateCharacterComponent self)
        {
            self.Awake();
        }
    }

    public class CreateCharacterComponent : Component
    {
        //创建角色界面 包括职业/外形可供选择
        public FUI Holder; //模型容器
        GameObject Demo; //玩家角色预览
        GoWrapper Wrapper;
        bool isCreateComplete; //是否加载完成 

        public FUI Controller; //控制器
        public Controller Gender;
        public Controller Career;
        public Controller Step;


        public FUI PasswordInput;
        public FUI Prompt;
        

        //是否正在登录中（避免登录请求还没响应时连续点击登录）
        public bool isLogining;
        //是否正在注册中（避免登录请求还没响应时连续点击注册）
        public bool isRegistering;

        public void Awake()
        {
            //获取父级"包"
            FUI CreateCharacter = this.GetParent<FUI>();
            Controller = CreateCharacter.Get("Controller"); //Get有先后顺序
            Gender = Controller.GObject.asCom.GetController("c1");
            Career = Controller.GObject.asCom.GetController("c2");
            Step = Controller.GObject.asCom.GetController("c3");
            Holder = CreateCharacter.Get("Holder");

            //加载默认模型 男/战士
            OnControllerChanged();

            //添加事件
            //Gender.selectedIndex = 1; //设置被选中的页面
            //Gender.setSelectedIndex(1); 设置被选中的页面而不触发Change事件
            Controller.Get("Back").GObject.asButton.onClick.Add(() => LoginBtnOnClick()); //返回角色列表或者退出游戏
            Controller.Get("Complete").GObject.asButton.onClick.Add(() => LoginBtnOnClick()); //检验角色名称是否合法
            Gender.onChanged.Add(OnControllerChanged);
            Career.onChanged.Add(OnControllerChanged);
            //aObject.OnGearStop.Add(OnGearStop); //缓动结束通知

            isCreateComplete = true;
        }

        //切换身体部分模型事件
        public void OnControllerChanged()
        {
            //默认设置
            string skeleton = "ch_pc_hou_004";
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
                    weapon = "ch_we_one_hou_006";
                    break;
                default:
                    break;
            }

            Demo = SekiaHelper.CreateCharacter(skeleton, weapon, head, chest, hand, feet);
            Demo.transform.localPosition = new Vector3(200, -400, 1000); //位置
            Demo.transform.localScale = new Vector3(200, 200, 200); //大小
            Demo.transform.localEulerAngles = new Vector3(0, 180, 0); //角度
            Wrapper = new GoWrapper(Demo);
            Holder.GObject.asGraph.SetNativeObject(Wrapper);
        }

        public void LoginBtnOnClick()
        {
            if (this.isLogining || this.IsDisposed)
            {
                return;
            }
            this.isLogining = true;
            //SekiaHelper.Login(this.AccountInput.Get("Input").GObject.asTextInput.text, this.PasswordInput.Get("Input").GObject.asTextInput.text).NoAwait();
        }

        public void RegisterBtnOnClick()
        {
            if (this.isRegistering || this.IsDisposed)
            {
                return;
            }
            this.isRegistering = true;
            //SekiaHelper.Register(this.AccountInput.Get("Input").GObject.asTextInput.text, this.PasswordInput.Get("Input").GObject.asTextInput.text).NoAwait();
        }
    }
}

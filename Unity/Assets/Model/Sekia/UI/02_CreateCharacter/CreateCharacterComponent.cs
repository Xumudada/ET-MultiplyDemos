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
        public FUI AccountInput;
        public FUI PasswordInput;
        public FUI Prompt;

        //是否正在登录中（避免登录请求还没响应时连续点击登录）
        public bool isLogining;
        //是否正在注册中（避免登录请求还没响应时连续点击注册）
        public bool isRegistering;

        public void Awake()
        {
            //获取父级"包"
            FUI Sekialogin = this.GetParent<FUI>();
            
        }

        public void LoginBtnOnClick()
        {
            if (this.isLogining || this.IsDisposed)
            {
                return;
            }
            this.isLogining = true;
            SekiaHelper.Login(this.AccountInput.Get("Input").GObject.asTextInput.text, this.PasswordInput.Get("Input").GObject.asTextInput.text).NoAwait();
        }

        public void RegisterBtnOnClick()
        {
            if (this.isRegistering || this.IsDisposed)
            {
                return;
            }
            this.isRegistering = true;
            SekiaHelper.Register(this.AccountInput.Get("Input").GObject.asTextInput.text, this.PasswordInput.Get("Input").GObject.asTextInput.text).NoAwait();
        }
    }
}

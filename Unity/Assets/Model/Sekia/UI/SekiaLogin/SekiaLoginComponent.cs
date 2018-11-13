namespace ETModel
{
    [ObjectSystem]
    public class SekiaLoginComponentAwakeSystem : AwakeSystem<SekiaLoginComponent>
    {
        public override void Awake(SekiaLoginComponent self)
        {
            self.Awake();
        }
    }

    public class SekiaLoginComponent : Component
    {
        //获取组件
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

            //初始化数据
            this.AccountInput = Sekialogin.Get("AccountInput");
            this.PasswordInput = Sekialogin.Get("PasswordInput");
            this.Prompt = Sekialogin.Get("Prompt");
            this.isLogining = false;
            this.isRegistering = false;

            //添加事件
            Sekialogin.Get("LoginButton").GObject.asButton.onClick.Add(() => LoginBtnOnClick());
            Sekialogin.Get("RegisterButton").GObject.asButton.onClick.Add(() => RegisterBtnOnClick());
        }

        public void LoginBtnOnClick()
        {

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

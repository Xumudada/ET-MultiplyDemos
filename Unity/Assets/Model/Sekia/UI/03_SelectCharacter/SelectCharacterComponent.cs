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
        //获取组件
        public FUI AccountInput;
        public FUI PasswordInput;
        public FUI Prompt;

        //是否正在登录中（避免登录请求还没响应时连续点击登录）
        public bool isLogining;
        //是否正在注册中（避免登录请求还没响应时连续点击注册）
        public bool isRegistering;

        public void Awake(A0008_GetUserInfo_G2C message)
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

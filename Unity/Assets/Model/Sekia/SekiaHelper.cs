namespace ETModel
{
    public static class SekiaHelper
    {
        public static async ETVoid Login(string account, string password)
        {
            SekiaLoginComponent login = Game.Scene.GetComponent<FUIComponent>().Get(FUIType.SekiaLogin).GetComponent<SekiaLoginComponent>();

            Session sessionRealm = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
            A0002_Login_R2C messageRealm = (A0002_Login_R2C)await sessionRealm.Call(new A0002_Login_C2R() { Account = account, Password = password });
            sessionRealm.Dispose();
            login.Prompt.GObject.asTextField.text = "正在登录中...";

            //判断Realm服务器返回结果
            if (messageRealm.Error == ErrorCode.ERR_AccountOrPasswordError)
            {
                login.Prompt.GObject.asTextField.text = "登录失败,账号或密码错误";
                login.AccountInput.Get("Input").GObject.asTextInput.text = "";
                login.PasswordInput.Get("Input").GObject.asTextInput.text = "";
                login.isLogining = false;
                return;
            }
            //判断通过则登陆Realm成功

            Session sessionGate = Game.Scene.GetComponent<NetOuterComponent>().Create(messageRealm.GateAddress);
            Game.Scene.AddComponent<SessionComponent>().Session = sessionGate;
            A0003_LoginGate_G2C messageGate = (A0003_LoginGate_G2C)await sessionGate.Call(new A0003_LoginGate_C2G() { GateLoginKey = messageRealm.GateLoginKey });

            //判断登陆Gate服务器返回结果
            if (messageGate.Error == ErrorCode.ERR_ConnectGateKeyError)
            {
                login.Prompt.GObject.asTextField.text = "连接网关服务器超时";
                login.AccountInput.Get("Input").GObject.asTextInput.text = "";
                login.PasswordInput.Get("Input").GObject.asTextInput.text = "";
                sessionGate.Dispose();
                login.isLogining = false;
                return;
            }
            //判断通过则登陆Gate成功

            login.isLogining = false;
            login.Prompt.GObject.asTextField.text = "";
            User user = ComponentFactory.Create<User, long>(messageGate.UserID);
            GamerComponent.Instance.MyUser = user;
            Log.Debug("登陆成功");

            //获取角色信息判断应该进入哪个界面
            A0008_GetUserInfo_G2C messageUser = (A0008_GetUserInfo_G2C)await sessionGate.Call(new A0008_GetUserInfo_C2G());
            
            if (messageUser.Characters.Count == 0)
            {
                //进入创建角色界面
                CreateCharacterFactory.Create();
            }
            else
            {
                //进入角色选择界面
                SelectCharacterFactory.Create(messageUser);
            }

            Game.EventSystem.Run(EventIdType.SekiaLoginFinish);
        }

        public static async ETVoid Register(string account, string password)
        {
            Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
            A0001_Register_R2C message = (A0001_Register_R2C)await session.Call(new A0001_Register_C2R() { Account = account, Password = password });
            session.Dispose();

            SekiaLoginComponent login = Game.Scene.GetComponent<FUIComponent>().Get(FUIType.SekiaLogin).GetComponent<SekiaLoginComponent>();
            login.isRegistering = false;

            if (message.Error == ErrorCode.ERR_AccountAlreadyRegisted)
            {
                login.Prompt.GObject.asTextField.text = "注册失败，账号已被注册";
                login.AccountInput.Get("Input").GObject.asTextInput.text = "";
                login.PasswordInput.Get("Input").GObject.asTextInput.text = "";
                return;
            }

            if (message.Error == ErrorCode.ERR_RepeatedAccountExist)
            {
                login.Prompt.GObject.asTextField.text = "注册失败，出现重复账号";
                login.AccountInput.Get("Input").GObject.asTextInput.text = "";
                login.PasswordInput.Get("Input").GObject.asTextInput.text = "";
                return;
            }
            
            login.Prompt.GObject.asTextField.text = "注册成功";
        }
    }
}
using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

namespace ETHotfix
{
    [ObjectSystem]
    public class SekiaLoginComponentAwakeSystem : AwakeSystem<SekiaLoginComponent>
    {
        public override void Awake(SekiaLoginComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 登录界面组件
    /// </summary>
    public class SekiaLoginComponent : Component
    {
        //账号输入框
        private InputField account;
        //密码输入框
        private InputField password;
        //提示文本
        private Text prompt;
        //是否正在登录中（避免登录请求还没响应时连续点击登录）
        private bool isLogining;
        //是否正在注册中（避免登录请求还没响应时连续点击注册）
        private bool isRegistering;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            //热更测试
            Text hotfixPrompt = rc.Get<GameObject>("HotfixPrompt").GetComponent<Text>();
#if ILRuntime
            hotfixPrompt.text = "Sekia4.0 (ILRuntime模式)";
#else
            hotfixPrompt.text = "Sekia4.0 (Mono模式)";
#endif

            //绑定关联对象
            account = rc.Get<GameObject>("Account").GetComponent<InputField>();
            password = rc.Get<GameObject>("Password").GetComponent<InputField>();
            prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();

            //添加事件
            rc.Get<GameObject>("LoginButton").GetComponent<Button>().onClick.Add(OnLogin);
            rc.Get<GameObject>("RegisterButton").GetComponent<Button>().onClick.Add(OnRegister);
        }

        /// <summary>
        /// 设置提示
        /// </summary>
        /// <param name="str"></param>
        public void SetPrompt(string str)
        {
            this.prompt.text = str;
        }

        /// <summary>
        /// 登录按钮事件
        /// </summary>
        public async void OnLogin()
        {
            if (isLogining || this.IsDisposed)
            {
                return;
            }
            NetOuterComponent netOuterComponent = Game.Scene.ModelScene.GetComponent<NetOuterComponent>();

            //设置登录中状态
            isLogining = true;
            Session sessionWrap = null;
            try
            {
                //创建Realm服务器连接
                //避免重复按登陆按钮
                IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalConfigComponent.Instance.GlobalProto.Address);
                ETModel.Session session = netOuterComponent.Create(connetEndPoint);
                sessionWrap = ComponentFactory.Create<Session, ETModel.Session>(session);
                sessionWrap.session.GetComponent<SessionCallbackComponent>().DisposeCallback += s =>
                {
                    if (Game.Scene.GetComponent<UIComponent>()?.Get(UIType.SekiaLogin) != null)
                    {
                        //prompt.text = "连接失败";
                        isLogining = false;
                    }
                };

                //发送登录请求
                prompt.text = "正在登录中....";
                A0002_Login_R2C r2C_Login_Ack = (A0002_Login_R2C)await sessionWrap.Call(new A0002_Login_C2R() { Account = account.text, Password = password.text });

                if (this.IsDisposed)
                {
                    return;
                }

                if (r2C_Login_Ack.Error == ErrorCode.ERR_AccountOrPasswordError)
                {
                    prompt.text = "登录失败,账号或密码错误";
                    password.text = "";
                    isLogining = false;
                    return;
                }

                //创建Gate服务器连接
                connetEndPoint = NetworkHelper.ToIPEndPoint(r2C_Login_Ack.GateAddress);
                ETModel.Session gateSession = netOuterComponent.Create(connetEndPoint);
                Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);
                //Session添加连接断开组件，用于处理客户端连接断开
                Game.Scene.GetComponent<SessionComponent>().Session.AddComponent<SessionOfflineComponent>();
                Game.Scene.ModelScene.AddComponent<ETModel.SessionComponent>().Session = gateSession;

                //登录Gate服务器
                A0003_LoginGate_G2C g2C_LoginGate_Ack = (A0003_LoginGate_G2C)await SessionComponent.Instance.Session.Call(new A0003_LoginGate_C2G() { GateLoginKey = r2C_Login_Ack.GateLoginKey });
                if (g2C_LoginGate_Ack.Error == ErrorCode.ERR_ConnectGateKeyError)
                {
                    prompt.text = "连接网关服务器超时";
                    password.text = "";
                    Game.Scene.GetComponent<SessionComponent>().Session.Dispose();
                    return;
                }

                Log.Info("登录Gate成功");

                //保存本地玩家
                User user = ETModel.ComponentFactory.Create<User, long>(g2C_LoginGate_Ack.UserID);
                GamerComponent.Instance.MyUser = user;

                //跳转到大厅界面
                Game.Scene.GetComponent<UIComponent>().Create(UIType.SekiaLobby);
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.SekiaLogin);

            }
            catch (Exception e)
            {
                prompt.text = "登录异常";
                Log.Error(e.ToStr());
            }
            finally
            {
                //断开验证服务器的连接
                sessionWrap.Dispose();
                //设置登录处理完成状态
                isLogining = false;
            }
        }

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        public async void OnRegister()
        {
            if (isRegistering || this.IsDisposed)
            {
                return;
            }

            //设置登录中状态
            isRegistering = true;
            Session sessionWrap = null;
            prompt.text = "";
            try
            {
                //创建登录服务器连接
                IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalConfigComponent.Instance.GlobalProto.Address);
                ETModel.Session session = Game.Scene.ModelScene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
                sessionWrap = ComponentFactory.Create<Session, ETModel.Session>(session);

                //发送注册请求
                prompt.text = "正在注册中....";
                A0001_Register_R2C r2C_Register_Ack = (A0001_Register_R2C)await sessionWrap.Call(new A0001_Register_C2R() { Account = account.text, Password = password.text });
                prompt.text = "";

                if (this.IsDisposed)
                {
                    return;
                }

                if (r2C_Register_Ack.Error == ErrorCode.ERR_AccountAlreadyRegisted)
                {
                    prompt.text = "注册失败，账号已被注册";
                    account.text = "";
                    password.text = "";
                    return;
                }
                if (r2C_Register_Ack.Error == ErrorCode.ERR_RepeatedAccountExist)
                {
                    prompt.text = "注册失败，出现重复账号";
                    account.text = "";
                    password.text = "";
                    return;
                }

                prompt.text = "注册成功";
            }
            catch (Exception e)
            {
                prompt.text = "注册异常";
                Log.Error(e.ToStr());
            }
            finally
            {
                //断开验证服务器的连接
                sessionWrap?.Dispose();
                //设置注册处理完成状态
                isRegistering = false;
            }
        }
    }
}

using System;
using ETModel;

namespace ETHotfix
{
    public static class SekiaHelper
    {
        public static async ETVoid Login(string account, string password)
        {
            
        }

        public static async ETVoid Register(string account, string password)
        {
            ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

            // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
            Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
            A0001_Register_R2C message = (A0001_Register_R2C)await realmSession.Call(new A0001_Register_C2R() { Account = account, Password = password });
            realmSession.Dispose();

            SekiaLoginComponent login = Game.Scene.GetComponent<FUIComponent>().Get(FUIType.SekiaLogin).GetComponent<SekiaLoginComponent>();
            login.isRegistering = false;

            if (login.IsDisposed)
            {
                return;
            }

            if (message.Error == ErrorCode.ERR_AccountAlreadyRegisted)
            {
                login.Prompt.GObject.asTextInput.text = "注册失败，账号已被注册";
                login.AccountInput.Get("Input").GObject.asTextInput.text = "";
                login.PasswordInput.Get("Input").GObject.asTextInput.text = "";
                return;
            }
            if (message.Error == ErrorCode.ERR_RepeatedAccountExist)
            {
                login.Prompt.GObject.asTextInput.text = "注册失败，出现重复账号";
                login.AccountInput.Get("Input").GObject.asTextInput.text = "";
                login.PasswordInput.Get("Input").GObject.asTextInput.text = "";
                return;
            }

            //这里Prompt必须设置为“输入文本” 可触摸才能设置内容
            login.Prompt.GObject.asTextInput.text = "注册成功";
        }
    }
}
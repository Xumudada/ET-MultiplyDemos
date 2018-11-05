using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class A1006_PathfindingResult : AMHandler<A1006_PathfindingResult_M2C>
    {
        protected override void Run(ETModel.Session session, A1006_PathfindingResult_M2C message)
        {
            Gamer gamer = ETModel.Game.Scene.GetComponent<GamerComponent>().Get(message.UserID);

            //如果本地玩家收到服务器转发自己的消息 不处理
            if (gamer.UserID == GamerComponent.Instance.MyUser.UserID)
            {
                return;
            }

            gamer.GetComponent<GamerAnimatorComponent>().SetIntValue("Speed", 1);
            GamerPathComponent gamerPathComponent = gamer.GetComponent<GamerPathComponent>();

            gamerPathComponent.StartMove(message).NoAwait();

            GizmosDebug.Instance.Path.Clear();
            GizmosDebug.Instance.Path.Add(new Vector3(message.X, message.Y, message.Z));
            for (int i = 0; i < message.Xs.Count; ++i)
            {
                GizmosDebug.Instance.Path.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
            }
        }
    }
}

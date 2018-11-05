using ETModel;
using PF;
using Vector3 = UnityEngine.Vector3;
using UnityEngine;
using UnityEngine.AI;

namespace ETHotfix
{
    [MessageHandler]
    public class A1008_GameStart : AMHandler<A1008_GameStart_M2C>
    {
        protected override void Run(ETModel.Session session, A1008_GameStart_M2C message)
        {
            //本地开始游戏计时
            Moba5V5Component.instance.isGameStarted = true;
            //销毁空气墙
            UnityEngine.Object.Destroy(GameObject.Find($"/Global/Unit/Qiang1(Clone)"));
            UnityEngine.Object.Destroy(GameObject.Find($"/Global/Unit/Qiang2(Clone)"));
        }
    }
}
using ETModel;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;

namespace ETHotfix
{
    public static class TemplateComponentSystem
    {
        public static void test1(this TemplateComponent self)
        {

        }

        public static async ETVoid test(this TemplateComponent self)
        {
            TimerComponent timerComponent = ETModel.Game.Scene.GetComponent<TimerComponent>();
            await timerComponent.WaitAsync(1000);
        }
    }
}
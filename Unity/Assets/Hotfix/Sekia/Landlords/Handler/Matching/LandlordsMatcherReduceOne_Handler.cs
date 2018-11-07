using System;
using System.Collections.Generic;
using ETModel;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class LandlordsMatcherReduceOne_Handler : AMHandler<Actor_LandlordsMatcherReduceOne>
    {
        protected override void Run(ETModel.Session session, Actor_LandlordsMatcherReduceOne message)
        {
            Log.Debug("匹配玩家-1");
        }
    }
}

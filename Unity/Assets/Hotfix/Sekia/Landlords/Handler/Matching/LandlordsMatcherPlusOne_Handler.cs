using System;
using System.Collections.Generic;
using ETModel;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class LandlordsMatcherPlusOne_Handler : AMHandler<Actor_LandlordsMatcherPlusOne>
    {
        protected override void Run(ETModel.Session session, Actor_LandlordsMatcherPlusOne message)
        {
            Log.Debug("匹配玩家+1");
        }
    }
}

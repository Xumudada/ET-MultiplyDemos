using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class GamerPathComponent : Component
    {
        public List<Vector3> Path = new List<Vector3>();

        public Vector3 ServerPos;

        //取消委托
        public CancellationTokenSource CancellationTokenSource;
    }
}
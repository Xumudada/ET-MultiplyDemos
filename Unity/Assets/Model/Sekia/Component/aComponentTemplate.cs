using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class TemplateComponentAwakeSystem : AwakeSystem<TemplateComponent>
    {
        public override void Awake(TemplateComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class TemplateComponentUpdateSystem : UpdateSystem<TemplateComponent>
    {
        public override void Update(TemplateComponent self)
        {
            self.Update();
        }
    }
    /// <summary>
    /// 模版组件 用于复制粘贴
    /// </summary>
    public class TemplateComponent : Component
    {
        public int ID = 1;

        public void Awake()
        {

        }

        public void Update()
        {

        }
    }
}
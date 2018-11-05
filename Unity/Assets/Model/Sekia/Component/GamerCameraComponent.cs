using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class GamerCameraComponentSystem : AwakeSystem<GamerCameraComponent>
    {
        public override void Awake(GamerCameraComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class GamerCameraComponentUpdateSystem : UpdateSystem<GamerCameraComponent>
    {
        public override void Update(GamerCameraComponent self)
        {
            self.Update();
        }
    }

    public class GamerCameraComponent : Component
    {
        public Camera mainCamera; //主相机
        public Transform target; //主相机跟随目标 默认为玩家
        public float smoothing = 4f; //相机移动速度 略低于玩家移动速度
        Vector3 offset = new Vector3(0, 10, -5);

        public void Awake()
        {
            this.mainCamera = UnityEngine.Camera.main;
            this.mainCamera.gameObject.transform.position = this.GetParent<Gamer>().GameObject.transform.position + offset;
        }

        public void Update()
        {
            Vector3 targetCamPos = this.GetParent<Gamer>().GameObject.transform.position + offset;   //相机的移动目标
            this.mainCamera.gameObject.transform.position = Vector3.Lerp(this.mainCamera.gameObject.transform.position, targetCamPos, smoothing * Time.deltaTime);  //平滑移动相机
        }
    }
}
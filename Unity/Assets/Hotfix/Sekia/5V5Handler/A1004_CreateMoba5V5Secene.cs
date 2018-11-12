using ETModel;
using PF;
using Vector3 = UnityEngine.Vector3;
using UnityEngine;
using UnityEngine.AI;

namespace ETHotfix
{
    [MessageHandler]
    public class A1004_CreateMoba5V5Secene : AMHandler<A1004_CreateMoba5V5Secene_M2C>
    {
        protected override async void Run(ETModel.Session session, A1004_CreateMoba5V5Secene_M2C message)
        {
            //开启加载界面 切换UI
            UIComponent uiComponent = Game.Scene.GetComponent<UIComponent>();
            UI ui = uiComponent.Get(UIType.SekiaLobby);
            if (ui == null)
            {
                return;
            }
            if(ui.GetComponent<SekiaLobbyComponent>().isMatching == false)
            {
                return;
            }
            uiComponent.Create(UIType.Moba5V5UI);
            uiComponent.Remove(UIType.SekiaLobby);

            //切换场景
            using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
            {
                await sceneChangeComponent.ChangeSceneAsync(SceneType.Moba5V5Map);
            }

            //加载配置
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            await resourcesComponent.LoadBundleAsync($"unit.unity3d");
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("Unit.unity3d", "Unit");
            ConfigComponent config = Game.Scene.GetComponent<ConfigComponent>();
            Moba5V5Component moba = Game.Scene.AddComponent<Moba5V5Component>();

            //创建空气墙 编号73和74
            for (int i=0;i<2;i++)
            {
                Moba5V5Config qiang = (Moba5V5Config)config.Get(typeof(Moba5V5Config), 73+i);
                GameObject qiangPrefab = bundleGameObject.Get<GameObject>(qiang.Prefab);
                qiangPrefab.transform.position = new Vector3((float)qiang.Position[0], (float)qiang.Position[1], (float)qiang.Position[2]);
                qiangPrefab.transform.eulerAngles = new Vector3(0, qiang.Rotation, 0);
                qiangPrefab.name = $"Qiang{(i + 1).ToString()}";
                UnityEngine.Object.Instantiate(qiangPrefab).transform.SetParent(GameObject.Find($"/Global/Unit").transform, false);
            }

            //创建10个英雄的GameObject和Gamer MobaID编号1-10
            for (int i = 0; i < message.Gamers.Count; i++)
            {
                //获取英雄配置 角色配置 技能配置
                HeroConfig heroConfig = (HeroConfig)config.Get(typeof(HeroConfig), message.Gamers[i].HeroID);
                Moba5V5Config moba5V5Config = (Moba5V5Config)config.Get(typeof(Moba5V5Config), i+1);
                GameObject prefab = bundleGameObject.Get<GameObject>(heroConfig.Prefab);
                prefab.transform.position = new Vector3((float)moba5V5Config.Position[0], (float)moba5V5Config.Position[1], (float)moba5V5Config.Position[2]);
                prefab.transform.eulerAngles = new Vector3(0, moba5V5Config.Rotation, 0);   

                //创建角色
                Gamer gamer = ETModel.ComponentFactory.Create<Gamer, long>(message.Gamers[i].UserID);
                gamer.GameObject = UnityEngine.Object.Instantiate(prefab);
                gamer.GameObject.transform.SetParent(GameObject.Find($"/Global/Unit").transform, false);
                int mobaID = moba.MobaID; //获取ID
                moba.mobaIDGamers.Add(mobaID, gamer); //添加到词典

                //添加Unity组件
                NavMeshAgent navMeshAgent = gamer.GameObject.AddComponent<NavMeshAgent>();
                navMeshAgent.radius = 0.2f;
                navMeshAgent.height = 0.8f;
                navMeshAgent.angularSpeed = 360f;
                navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

                //添加ET组件
                GamerAnimatorComponent anime = gamer.AddComponent<GamerAnimatorComponent>();
                gamer.AddComponent<GamerMoveComponent>();
                gamer.AddComponent<GamerTurnComponent>();
                gamer.AddComponent<GamerPathComponent>();

                //设置角色
                CharacterComponent character = gamer.AddComponent<CharacterComponent>();
                character.mobaID = mobaID;
                character.group = moba5V5Config.Group; //阵营
                character.type = moba5V5Config.Type; //类型
                character.route = moba5V5Config.Route; //默认导航线路
                character.anime = anime;

                //确认本地玩家
                if (gamer.UserID == GamerComponent.Instance.MyUser.UserID)
                {
                    gamer.AddComponent<GamerCameraComponent>();
                    moba.myGamer = gamer;
                }
            }
            
            //创建攻击类建筑 MobaID编号11-30
            for(int i=0;i<20;i++)
            {
                //获取角色配置
                Moba5V5Config moba5V5Config = (Moba5V5Config)config.Get(typeof(Moba5V5Config), i + 11);

                //创建角色
                Gamer gamer = ETModel.ComponentFactory.Create<Gamer>();
                gamer.GameObject = GameObject.Find($"/World/5v5_Map/mobaTower{i+1}");
                gamer.GameObject.transform.SetParent(GameObject.Find($"/Global/Unit").transform, false);
                int mobaID = moba.MobaID; //获取ID
                moba.mobaIDGamers.Add(mobaID, gamer); //添加到词典

                //添加ET组件
                gamer.AddComponent<GamerAnimatorComponent>();
                CharacterComponent character = gamer.AddComponent<CharacterComponent>();

                //设置角色
                character.mobaID = mobaID;
                character.group = moba5V5Config.Group; //阵营
                character.type = moba5V5Config.Type; //类型
            }

            //初始加载完成
            SessionComponent.Instance.Session.Send(new A1007_GamerReadyMoba5V5_C2M());
            //等待服务端通知游戏计时开始
            //等待服务端通知刷新第一波小兵
            //等待服务端通知刷新普通野怪
            //等待服务端通知刷新大龙
        }
    }
}
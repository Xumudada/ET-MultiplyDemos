using ETModel;
using PF;
using Vector3 = UnityEngine.Vector3;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace ETHotfix
{
    [MessageHandler]
    public class A1012_NewMonster : AMHandler<A1012_NewMonster_M2C>
    {
        protected override void Run(ETModel.Session session, A1012_NewMonster_M2C message)
        {
            //服务端通知客户端刷新一窝野怪
            TimerComponent timerComponent = ETModel.Game.Scene.GetComponent<TimerComponent>();
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("Unit.unity3d", "Unit");
            ConfigComponent config = Game.Scene.GetComponent<ConfigComponent>();
            Moba5V5Component moba = Moba5V5Component.instance;

            //野怪点ID 编号分别为：49 51 52 54 57 58 59 60 61 62 63 64 65 68 70 71
            int configID; //配置编号
            int monsterNumber; //野怪数量
            switch(message.MonsterID)
            {
                case 49: //蜥蜴
                    configID = 49;
                    monsterNumber = 2;
                    break;
                case 51: //蓝Buff
                    configID = 51;
                    monsterNumber = 1;
                    break;
                case 52: //狼
                    configID = 52;
                    monsterNumber = 2;
                    break;
                case 54: //熊
                    configID = 54;
                    monsterNumber = 3;
                    break;
                case 57: //红Buff
                    configID = 57;
                    monsterNumber = 1;
                    break;
                case 58: //鸟
                    configID = 58;
                    monsterNumber = 1;
                    break;
                case 59: //河道蟹
                    configID = 59;
                    monsterNumber = 1;
                    break;
                case 60: //河道蟹
                    configID = 60;
                    monsterNumber = 1;
                    break;
                case 61: //大Boss
                    configID = 61;
                    monsterNumber = 1;
                    break;
                case 62: //小Boss
                    configID = 62;
                    monsterNumber = 1;
                    break;
                case 63: //鸟
                    configID = 63;
                    monsterNumber = 1;
                    break;
                case 64: //红buff
                    configID = 64;
                    monsterNumber = 1;
                    break;
                case 65: //熊
                    configID = 65;
                    monsterNumber = 3;
                    break;
                case 68: //狼
                    configID = 68;
                    monsterNumber = 2;
                    break;
                case 70: //蓝Buff
                    configID = 70;
                    monsterNumber = 1;
                    break;
                case 71: //蜥蜴
                    configID = 71;
                    monsterNumber = 2;
                    break;
                default:
                    throw new Exception("未知野怪点");
            }

            for (int i = 0; i < monsterNumber; i++)
            {
                if (i == 12) continue;

                //获取角色配置
                Moba5V5Config moba5V5Config = (Moba5V5Config)config.Get(typeof(Moba5V5Config), i + configID);
                GameObject prefab = bundleGameObject.Get<GameObject>(moba5V5Config.Prefab);
                prefab.transform.position = new Vector3((float)moba5V5Config.Position[0], (float)moba5V5Config.Position[1], (float)moba5V5Config.Position[2]);
                prefab.transform.eulerAngles = new Vector3(0, moba5V5Config.Rotation, 0);

                //创建角色
                Gamer gamer = ETModel.ComponentFactory.Create<Gamer>();
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
                gamer.AddComponent<GamerAnimatorComponent>();
                gamer.AddComponent<GamerMoveComponent>();
                gamer.AddComponent<GamerTurnComponent>();
                gamer.AddComponent<GamerPathComponent>();

                //设置角色
                CharacterComponent character = gamer.AddComponent<CharacterComponent>();
                character.mobaID = mobaID;
                character.group = moba5V5Config.Group; //阵营
                character.type = moba5V5Config.Type; //类型
            }
        }
    }
}
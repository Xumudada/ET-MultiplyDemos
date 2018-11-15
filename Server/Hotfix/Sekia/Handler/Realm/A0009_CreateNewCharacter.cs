using System;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    //客户端请求在当前账号下创建新的角色
    //参数：假定的角色名、默认模型编号、职业编号
    //模型编号：记录了玩家的骨骼类型 玩家在今后的游戏过程中将使用相匹配的mesh和材质
    [MessageHandler(AppType.Gate)]
    public class A0009_CreateNewCharacter : AMRpcHandler<A0009_CreateNewCharacter_C2G, A0009_CreateNewCharacter_G2C>
    {
        protected override async void Run(Session session, A0009_CreateNewCharacter_C2G message, Action<A0009_CreateNewCharacter_G2C> reply)
        {
            A0009_CreateNewCharacter_G2C response = new A0009_CreateNewCharacter_G2C();
            try
            {
                //验证Session
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_UserNotOnline;
                    reply(response);
                    return;
                }

                //获取玩家对象
                User user = session.GetComponent<SessionUserComponent>().User;

                //获取玩家所在大区编号
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                UserInfo userInfo = await dbProxy.Query<UserInfo>(user.UserID);
                int GateAppId = RealmHelper.GetGateAppIdFromUserId(userInfo.Id);

                //检查角色名是否可用
                //会得到全部大区的同名角色 需遍历排除
                List<ComponentWithId> result = await dbProxy.Query<Character>($"{{Name:'{message.Name}'}}");
                foreach(var a in result)
                {
                    if(RealmHelper.GetGateAppIdFromUserId(((Character)a).UserID) == GateAppId)
                    {
                        //出现同名角色
                        response.Error = ErrorCode.ERR_CreateNewCharacter;
                        reply(response);
                        return;
                    }
                }

                //检查玩家是否有资格创建新角色
                bool canCreate = false;
                int characterSeat = 0; //玩家的可创建新角色的空位
                if(userInfo.CharacterID1 == 0)
                {
                    canCreate = true;
                    characterSeat = 1;
                }
                if (userInfo.CharacterID2 == 0 && characterSeat == 0) //前面的位置优先
                {
                    canCreate = true;
                    characterSeat = 2;
                }
                if (userInfo.CharacterID3 == 0 && characterSeat == 0) //前面的位置优先
                {
                    canCreate = true;
                    characterSeat = 3;
                }

                //判定为无法创建角色时返回错误消息
                if(!canCreate)
                {
                    //玩家角色位置已满 理应不该出现这个错误
                    //当玩家位置满时 点击创建角色按钮应有提示 无法进入创建角色界面
                    Log.Error("玩家角色位置已满");
                    response.Error = ErrorCode.ERR_CreateNewCharacter;
                    reply(response);
                    return;
                }

                //新建角色数据 角色可以通过UserID来识别区号 可以不使用CreateWithId方法
                Character character = ComponentFactory.CreateWithId<Character, long>(RealmHelper.GenerateId(), userInfo.Id);
                character.Name = message.Name;
                character.Level = 1;
                character.Career = message.Career;
                character.Pet = PetType.NonePet;
                character.Model = message.Model;
                switch(character.Career) //初始武器是绑定职业的
                {
                    case CareerType.Warror:
                        character.Weapon = WeaponType.Sword;
                        break;
                    case CareerType.Mage:
                        character.Weapon = WeaponType.Wand;
                        break;
                }
                character.Region = RegionType.Village; //初始地图为村庄
                character.X = 1; //设置初始坐标
                character.Y = 2;
                character.Z = 3;

                //存储数据
                switch(characterSeat)
                {
                    case 1:
                        userInfo.CharacterID1 = character.Id;
                        break;
                    case 2:
                        userInfo.CharacterID2 = character.Id;
                        break;
                    case 3:
                        userInfo.CharacterID3 = character.Id;
                        break;
                    default:
                        throw new Exception($"创建新角色错误：{userInfo.Id}");
                }
                await dbProxy.Save(character);
                await dbProxy.Save(userInfo);

                Log.Debug($"新增一个角色{character.Id}");
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
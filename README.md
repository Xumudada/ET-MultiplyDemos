# ET-MultiplyDemos
ET小游戏集合
目前包含游戏项目：
Moba5V5
斗地主
MMO

Sekia_Avater：用于测试组装自定义模型 更换武器/外观

ET主群：474643097
希望参与完善游戏/推荐代码可联系咲夜詩
QQ449224404

ET版本升级指南：

♦Unity部分，同时打开旧的客户端和全新的ET客户端，按顺序将文件夹拖入新客户端：
Res/Config/* （自定义配置）
Bundles/Sekia（删除Bundles目录下原有文件）
Editor/Sekia （删除ExcelExporterEditor文件夹）
Hotfix/Sekia
Model/Sekia
Scenes/* （删除默认场景）
覆盖Model/Module/Message/ErrorCode.cs （和服务端共享ErrorCode）
覆盖Model/Entity/Scene.cs （自定义场景名）
编译Protoc一次
Tools/导出配置/导出客户端配置一次
修复缺失partial关键字

♦Server部分，同时打开旧的Server目录和全新的Server目录，复制粘贴文件或文件夹：
Server/App/Program.cs
Server/Hotfix/Sekia
Server/Model/Sekia
Server/otfix/Module/Message/OuterMessageDispatcher.cs


后续操作：

使用3dmax等工具拆解出地图上的防御塔
技能特效表
ILRuntime使用比较器的适配
创建血条 和 伤害数字
给士兵添加AI

士兵AI原理：
while(true)循环中处理逻辑
当前攻击目标为空时 根据默认导航线路寻路前进
每隔1秒钟确认警戒范围内是否有可攻击单位
发现目标时导航向目标
接近目标进入可攻击范围后使用普攻技能
目标脱离警戒范围后返回默认导航线路
脱离循环条件：自身死亡/游戏结束

血条原理：
使用Slider制作Prefab 移动到目标头上

伤害数字原理
使用Text制作Prefab 有渐隐/小范围随机数效果 移动到目标头上

技能特效表：
包括技能ID 攻击者攻击特效 受击者受击特效

技能特效素材 以下技能因包含物体飞行脚本未移植：
冰女普攻3
敌法师技能1
幻影刺客技能2/技能4
巨魔技能2
赏金猎人技能4
沙王技能2
圣骑士技能3
水人技能2
小黑普攻3/普攻3_1/技能2/技能4
沉默普攻3/普攻3_1
小鹿普攻3
美杜莎技能1/技能2_1
白虎普攻3/技能1

缺少动画控制器单位：
024_斧王
野猪

技能Icon：更新了单位icon/技能icon资源

斗地主项目：
一个bug待修复
某玩家手中牌全部出完后并没有检测到游戏结束

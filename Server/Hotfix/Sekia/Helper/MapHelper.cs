using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ETModel;
using Google.Protobuf.Collections;

namespace ETHotfix
{
    /// <summary>
    /// 容器转换辅助方法
    /// </summary>
    public static class To
    {
        //数组-重复字段
        public static RepeatedField<T> RepeatedField<T>(T[] cards)
        {
            RepeatedField<T> a = new RepeatedField<T>();
            foreach (var b in cards)
            {
                a.Add(b);
            }
            return a;
        }

        //列表-重复字段
        public static RepeatedField<T> RepeatedField<T>(List<T> cards)
        {
            RepeatedField<T> a = new RepeatedField<T>();
            foreach (var b in cards)
            {
                a.Add(b);
            }
            return a;
        }

        //重复字段-数组
        public static T[] Array<T>(RepeatedField<T> cards)
        {
            T[] a = new T[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                a[i] = cards[i];
            }
            return a;
        }

        //重复字段-列表
        public static List<T> List<T>(RepeatedField<T> cards)
        {
            List<T> a = new List<T>();
            foreach (var b in cards)
            {
                a.Add(b);
            }
            return a;
        }
    }

    /// <summary>
    /// 副本相关辅助方法
    /// </summary>
    public static class BattleHelp
    {
        /// <summary>
        /// 随机生成指定数量的英雄ID 1~100
        /// </summary>
        /// <param name="count">要生成ID的数量</param>
        /// <param name="ExceptIds">需要排除ID列表</param>
        /// <returns></returns>
        public static List<int> GetRandomTeam(int count, List<int> ExceptIds = null)
        {
            List<int> randmSeeds = new List<int>();
            List<int> alreadyHave = new List<int>();
            if (ExceptIds != null)
            {
                for (int m = 0; m < ExceptIds.Count; m++)
                {
                    int rSeed = ExceptIds[m];
                    alreadyHave.Add(rSeed);
                }
            }

            for (int i = 0; i < count; i++)
            {
                int seed = RandomSeedNotRepeat(alreadyHave);
                randmSeeds.Add(seed);
            }

            return randmSeeds;
        }

        //从23个英雄中随机
        private static int RandomSeedNotRepeat(List<int> alreadyHave)
        {
            int randomSeed = RandomHelper.RandomNumber(1, 24);
            if (alreadyHave.Contains(randomSeed))
            {
                randomSeed = RandomSeedNotRepeat(alreadyHave);
            }
            else
            {
                alreadyHave.Add(randomSeed);
            }
            return randomSeed;
        }
    }
}

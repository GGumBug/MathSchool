using System.Collections.Generic;
using System;

namespace Data
{
    [Serializable]
    public class Unit
    {
        public int level;
        public string name;
        public int hp;
        public int maxHp;
        public int atk;
        public float atkDelay;
        public int price;
    }

    [Serializable]
    public class Enemy
    {
        public int level;
        public string name;
        public int hp;
        public int maxHp;
        public int atk;
        public float atkDelay;
        public float moveSpeed;
        public int value;
    }

    [Serializable]
    public class UnitStatData<T> : ILoader<int, T> where T : Unit
    {
        public List<T> stats = new List<T>();

        public Dictionary<int, T> MakeDict()
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();

            foreach (T stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }

    [Serializable]
    public class EnemyStatData<T> : ILoader<int, T> where T : Enemy
    {
        public List<T> stats = new List<T>();

        public Dictionary<int, T> MakeDict()
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();

            foreach (T stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
}


using System.Collections.Generic;
using System;

namespace Data
{
    [Serializable]
    public class Stage
    {
        public int stageLevel;
        public int maxEnemyCount;
        public int feverEnemyCount;
        public string nomalEnemy;
        public string highEnemy;
        public string eliteEnemy;
    }
    
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
        public int unlockPrice;
        public int levelUpPrice;
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
    public class StageStatData<T> : ILoader<int, T> where T : Stage
    {
        // JSON 리스트 제목과 리스트명을 일치시켜야 데이터가 담긴다.
        public List<T> stages = new List<T>();

        public Dictionary<int, T> MakeDict()
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();

            foreach (T stage in stages)
                dict.Add(stage.stageLevel, stage);

            return dict;
        }
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


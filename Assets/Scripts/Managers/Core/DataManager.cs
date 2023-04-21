using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public List<string> UnitNames = new List<string>();
    public Dictionary<string, Dictionary<int, Data.Unit>> UnitStatDict { get; private set; } = new Dictionary<string, Dictionary<int, Data.Unit>>();
    public Dictionary<int, Data.Enemy> EnemyStatDict { get; private set; } = new Dictionary<int, Data.Enemy>();


    public void Init()
    {
        UnitStatLoader(typeof(Define.Unit));
        EnemyStatDict = LoaderJson<Data.EnemyStatData<Data.Enemy>, int, Data.Enemy>($"EnemyStatData").MakeDict();
    }

    private void UnitStatLoader(Type type)
    {
        string[] names = Enum.GetNames(type);

        Dictionary<int, Data.Unit> statDict = new Dictionary<int, Data.Unit>();

        for (int i = 0; i < names.Length; i++)
        {
            string name = names[i];
            UnitNames.Add(name);
            statDict = LoaderJson<Data.UnitStatData<Data.Unit>, int, Data.Unit>($"{name}StatData").MakeDict();
            UnitStatDict.Add(statDict[1].name, statDict);
        }
    }

    Loader LoaderJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}

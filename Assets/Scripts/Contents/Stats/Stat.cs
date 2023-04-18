using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [field: SerializeField]
    public int Hp { get; protected set; }
    [field: SerializeField]
    public int MaxHp { get; protected set; }
    [field: SerializeField]
    public int Atk { get; protected set; }
    [field: SerializeField]
    public float AtkDelay { get; protected set; }
}

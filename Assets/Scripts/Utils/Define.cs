using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum Scene
    {
        Unknown,
        Main,
        Game
    }

    public enum State
    {
        Die,
        Idle,
        Skill
    }
}
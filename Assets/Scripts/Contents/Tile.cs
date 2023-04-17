using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsEmpty { get; private set; }

    public bool SetIsEmpty()
    {
        if (IsEmpty)
            return IsEmpty = false;

        return IsEmpty = true;
    }
}

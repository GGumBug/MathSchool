using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform patent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
            throw new System.Exception($"Failed to load prefab: { path }");

        GameObject go   = Object.Instantiate(original, patent);
        go.name         = original.name;

        return go;
    }

    public void Destroy(GameObject go, float time = 0f)
    {
        if (go == null)
            return;

        Object.Destroy(go, time);
    }
}

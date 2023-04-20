using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    private Vector2Int  tileCount = new Vector2Int(10, 3);
    private Vector2     offset = new Vector2(1.7f, 1f);

    public void SpwanTile()
    {
        for (int y = 0; y < tileCount.y; y++)
        {
            for (int x = 0; x < tileCount.x; x++)
            {
                GameObject go = Managers.Resource.Instantiate("Tile", transform);
                SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
                float xSize = sprite.bounds.size.x;
                float ySize = sprite.bounds.size.y;
                float px = (-tileCount.x * 0.5f - offset.x) + x * xSize;
                float py = (tileCount.y * 0.5f + offset.y) - y * ySize;
                Vector3 position = new Vector3(px, py, 0);

                go.transform.position = position;
                go.name = $"{y},{x}";
            }
        }
    }
}

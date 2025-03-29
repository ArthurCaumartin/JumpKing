using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReeder : MonoBehaviour
{
    [SerializeField] Tilemap _tilemap;
    public List<Sprite> SpriteList;
    Sprite sprite;

    public float a = 5;

    void Update()
    {
        sprite = _tilemap.GetSprite(_tilemap.WorldToCell(transform.position - Vector3.up));
        if (sprite == null) return;
        print(sprite.name);
    }

    public bool isGroundIce() => SpriteList.Contains(sprite);
}

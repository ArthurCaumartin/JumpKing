using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReeder : MonoBehaviour
{
    [SerializeField] Tilemap _tilemap;
    public List<Sprite> SpriteList;
    Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite = _tilemap.GetSprite(_tilemap.WorldToCell(transform.position - Vector3.up));
        if (sprite == null) return;
        print(sprite.name);
    }
    public bool isGroundIce() => SpriteList.Contains(sprite);

}

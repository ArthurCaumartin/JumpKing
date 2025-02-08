using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNode : MonoBehaviour
{
    private Vector2 Size;
    public float max;
    public float min;
    // Start is called before the first frame update
    void Start()
    {
        Size = GetNodeSize();
        max = transform.position.y + (Size.y/2);
        min = transform.position.y - (Size.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawCube(transform.position, GetNodeSize());
    }

    private Vector3 GetNodeSize()
    {
        Vector3 size = Vector3.zero;
        Vector2 BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0,0));
        Vector2 BottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth,0));
        size.x = Vector2.Distance(BottomLeft, BottomRight);

        Vector2 TopLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Camera.main.pixelHeight));
        Vector2 LeftCenter = Camera.main.ScreenToWorldPoint(new Vector2(0, Camera.main.pixelHeight/2));
        size.y = Vector2.Distance(TopLeft, LeftCenter);

        return size;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNode : MonoBehaviour
{
    private CameraController _cameraController;

    private void Start()
    {
        _cameraController = GetComponentInParent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _cameraController.SetTarget(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }


    [ContextMenu("Set Collider Size")]
    public void SetColliderSize()
    {
        GetComponent<BoxCollider2D>().size = GetCameraView();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .3f);

        Vector3 size = GetCameraView();
        Gizmos.DrawCube(transform.position, size);
    }

    private Vector2 GetCameraView()
    {
        Vector2 size = new Vector2();
        size.x = Vector3.Distance(Camera.main.ScreenToWorldPoint(Vector3.zero)
                                , Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)));

        size.y = Vector3.Distance(Camera.main.ScreenToWorldPoint(Vector3.zero)
                                , Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight)));
        size.y /= 2;
        return size;
    }
}

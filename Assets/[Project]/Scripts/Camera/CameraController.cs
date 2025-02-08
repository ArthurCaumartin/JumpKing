using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float vitesse;
    [SerializeField] Vector3 target;
    [SerializeField] List<CameraNode> CameraNodesList;
    [SerializeField] Transform Player;
    [SerializeField] Transform InterfaceCameraTarget;
    CameraNode cameranode;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update() 
    {
        cameranode = GetCurrentNode();
        target = cameranode.transform.position;
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, target + new Vector3(0, -GetOffSet(),-10), Time.deltaTime * vitesse);
    }

    /*CameraNode GetNearestNode()
    {
        CameraNode nodetoreturn = null;
        float minDistance = Mathf.Infinity;
        foreach (var item in CameraNodesList)
        {
           float Distance = Vector2.Distance(item.transform.position, Player.position);
           if(Distance < minDistance)
            {
                minDistance = Distance;
                nodetoreturn = item;
            }
        }
        return nodetoreturn;
    }*/

    private CameraNode GetCurrentNode()
    {
        foreach (CameraNode node in CameraNodesList)
        {
            if (Player.transform.position.y > node.min && Player.transform.position.y < node.max)
            {
                return node;
            }
        }
        return null;
    }

    public float GetOffSet()
    {
        Vector3 worldScreenCenter = _camera.ScreenToWorldPoint(new Vector2(_camera.pixelWidth, _camera.pixelHeight)/2);
        return Vector2.Distance(_camera.ScreenToWorldPoint(InterfaceCameraTarget.position), worldScreenCenter);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Lerp = Vector3.Lerp(transform.position, target.position, Time.deltaTime * _speed);
        Vector3 newPosition = new Vector3(0, Lerp.y, -10);
        transform.position = newPosition;
    }
}

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
        Vector3 targetPosition = new Vector3(-0.5f, target.position.y + 3f, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperNotPhysic : MonoBehaviour
{
    [SerializeField] Transform target;
    PlayerController Player;
    public float speed;
    private float _travelTime;
    Vector3 StartPoint;
    float distance;
    Rigidbody2D PlayerRB;
    private void Start()
    {
        distance = Vector2.Distance(transform.position, target.position);
    }
    private void Update()
    {
        if (Player != null)
        {
            _travelTime += Time.deltaTime * speed / distance;
            Player.enabled = false;
            Player.transform.position = Vector2.Lerp(StartPoint, target.position, _travelTime);
            if (_travelTime >= 1)
            {
                Player.enabled = true;
                _travelTime = 0;
                PlayerRB = Player.GetComponent<Rigidbody2D>();
                PlayerRB.velocity = new Vector3(0,0);
                Player = null;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p)
        {
            Player = p;
            StartPoint = p.transform.position;
        }
    }
}

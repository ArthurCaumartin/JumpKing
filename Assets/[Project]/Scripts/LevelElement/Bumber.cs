using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumber : MonoBehaviour
{
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        player?.PushPlayer(transform.right, force);
    }
}

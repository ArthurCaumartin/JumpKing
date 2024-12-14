using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] float _boxSize;
    [SerializeField] float _boxDistance;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] RaycastHit2D _hit;
    private void FixedUpdate()     
    {
        _hit = Physics2D.BoxCast(transform.position
                                , Vector2.one * _boxSize
                                , 0f
                                , Vector2.down
                                , _boxDistance
                                , _layerMask);
    }
    public bool IsGrounded()
    {
        return _hit.collider;
    }
}

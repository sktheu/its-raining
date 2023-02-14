using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Unity Access Fields
    [SerializeField] private float speed;

    [HideInInspector] public Vector2 Direction;
    
    // Components
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        _rb.velocity = Direction * speed;
    }
}

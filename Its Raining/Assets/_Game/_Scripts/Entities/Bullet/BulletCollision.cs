using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    // Unity Access Fields
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Invoke("SelfDestroy", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SelfDestroy();
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}

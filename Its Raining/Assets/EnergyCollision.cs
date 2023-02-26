using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollision : MonoBehaviour
{
    // Unity Access Fields
    [Header("Energy:")]
    [SerializeField] private float energyIncrement;

    // References
    private static PlayerShoot playerShoot;

    private void Awake()
    {
        playerShoot = GameObject.FindObjectOfType<PlayerShoot>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<BulletMovement>(out var bullet))
        {
            playerShoot.ChangeCurrentEnergy(energyIncrement);
        }
    }
}

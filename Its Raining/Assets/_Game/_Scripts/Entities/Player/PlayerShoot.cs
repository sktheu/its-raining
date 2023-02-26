using System.Collections;
using Unity.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Unity Access Fields
    [Header("Bullet:")]
    [SerializeField] private float shootInterval;
    [SerializeField, ReadOnly] private bool canShoot = true;
    [SerializeField] private BulletMovement _bulletPrefab;

    [Header("Laser:")]
    [SerializeField] private float defaultEnergy;
    [SerializeField] private float maxEnergy;
    [SerializeField, ReadOnly] private float curEnergy = 0; 
    // TODO: Laser Prefab

    // References
    private MouseCursor _mouseCursor;
    private PlayerStateMachine.States _curState;
    
    private void Start()
    {
        _mouseCursor = GameObject.FindObjectOfType<MouseCursor>();
        ChangeCurrentEnergy(defaultEnergy);
    }

    private void Update()
    {
        if (_curState != PlayerStateMachine.States.Dashing)
        {
            ShootInput();
            LaserInput();
        }
    }

    private void ShootInput()
    {
        if (canShoot && Input.GetMouseButton(0))
        {
            canShoot = false;
            SpawnBullet();
            StartCoroutine(ShootInterval(shootInterval));
        }
    }

    private void SpawnBullet()
    {
        var dir = (_mouseCursor.WorldPosition - (Vector2)transform.position).normalized;
        
        var dirRotation  = ((Vector2)transform.position - _mouseCursor.WorldPosition).normalized;
        var rotationZ = Mathf.Atan2(dirRotation.y, dirRotation.x) * Mathf.Rad2Deg;
        
        var spawnPos = transform.position + (Vector3)dir;

        var bullet = Instantiate(_bulletPrefab, spawnPos, Quaternion.Euler(0, 0, rotationZ));
        bullet.Direction = dir;
    }

    private IEnumerator ShootInterval(float time)
    { 
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    public void ChangeCurrentEnergy(float increment)
    {
        curEnergy = Mathf.Clamp(curEnergy + increment, 0, maxEnergy);
    }

    private void LaserInput()
    {
        if (curEnergy == maxEnergy && Input.GetMouseButtonDown(1))
        {
            ChangeCurrentEnergy(-maxEnergy);
            SpawnLaser();        
        }
    }

    private void SpawnLaser()
    {
        // TODO: Laser Instantiate
        print("PEW!");
    }
}

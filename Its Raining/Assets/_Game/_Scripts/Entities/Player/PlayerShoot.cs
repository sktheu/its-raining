using System.Collections;
using Unity.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Unity Access Fields
    [SerializeField] private float shootInterval;
    [SerializeField, ReadOnly] private bool canShoot = true;
    
    // References
    [SerializeField] private BulletMovement _bulletPrefab;
    private MouseCursor _mouseCursor;
    private PlayerStateMachine.States _curState;
    
    private void Start()
    {
        _mouseCursor = GameObject.FindObjectOfType<MouseCursor>();
    }

    private void Update()
    {
        if (_curState != PlayerStateMachine.States.Dashing && _curState != PlayerStateMachine.States.Dashing)
        {
            ShootInput();
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
}

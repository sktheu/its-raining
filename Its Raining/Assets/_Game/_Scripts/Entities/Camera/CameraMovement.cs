using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Unity Access Fields
    [SerializeField] private bool following;
    [SerializeField] private float lerpSpeed;

    // References
    private Transform _playerTransf;
    
    // Components
    private MouseCursor _mouseCursor;

    private void Start()
    {
        _playerTransf = GameObject.FindObjectOfType<PlayerMovement>().transform;
        _mouseCursor = GetComponent<MouseCursor>();
    }

    private void LateUpdate()
    {
        if (following)
        {
            FollowPlayer();
        }       
    }

    private void FollowPlayer()
    {
        var targetPos = _playerTransf.position + (Vector3.forward * -10);
        var newPos = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}

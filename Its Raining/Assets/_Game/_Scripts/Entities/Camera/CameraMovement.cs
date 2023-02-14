using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Unity Access Fields
    [SerializeField] private bool following;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float cursorOffset;
    [SerializeField] private float cursorClamp;
    
    // References
    private Transform _playerTransf;
    
    // Components
    private MouseCursor _mouseCursor;
    
    // Centralize Camera
    private Vector3 _cursorLastPos;
    
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
        var cursorPos = (Vector3)_mouseCursor.WorldPosition.normalized * cursorOffset;
        if (Vector3.Distance(cursorPos, _cursorLastPos) < cursorClamp)
        {
            cursorPos = Vector3.zero;
        }
        var targetPos = _playerTransf.position + cursorPos + (Vector3.forward * -10);
        var newPos = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        _cursorLastPos = cursorPos;
        transform.position = newPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Vector2 _worldPosition;
    
    [HideInInspector] public Vector2 WorldPosition
    {
        get { return _worldPosition; }
        private set { _worldPosition = value; }
    }
    
    private void Start()
    {
        SetCursor();
    }

    private void Update()
    {
        _worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SetCursor()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("spr_mouse_crosshair"), Vector2.zero, CursorMode.ForceSoftware);
    }
}

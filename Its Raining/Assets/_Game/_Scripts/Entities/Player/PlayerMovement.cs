using System.Collections;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Unity Access Fields
    [Header("Movement:")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    [Header("Dash:")] 
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashInterval;
    [SerializeField, ReadOnly] private bool canDash = true;
    [SerializeField] private float dashEffectTime;

    [Header("References:")] 
    [SerializeField] private SpriteRenderer dashEffect;
    
    // Components
    private Rigidbody2D _rb;
    private SpriteRenderer _spr;
    
    // Move
    private Vector2 _moveInput;
    
    // State
    private PlayerStateMachine.States _curState;
    
    // Dash Effect
    private bool _playDashEffect = false;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _curState = PlayerStateMachine.StateManager.GetState();
        
        GetMovementInput();
        GetDashInput();
        FlipSprite();
        
        if (_playDashEffect) SpawnDashEffect();
    }

    private void FixedUpdate()
    {
        if (_curState == PlayerStateMachine.States.NotPlaying) return;
        else if (_curState == PlayerStateMachine.States.Moving) ApplyMovement();
        else if (_curState == PlayerStateMachine.States.Dashing) ApplyDash();
    }

    private void GetMovementInput()
    {
        if (_curState != PlayerStateMachine.States.Dashing)
        {
            _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (_moveInput != Vector2.zero) PlayerStateMachine.StateManager.SetState(PlayerStateMachine.States.Moving);
        }
    }

    private Vector2 SetVelocity(Vector2 goal, Vector2 cur, float accel)
    {
        var move = Vector2.zero;
        var speedDiffX = goal.x - cur.x;
        var speedDiffY = goal.y - cur.y;

        if (speedDiffX > accel) move.x = cur.x + accel;
        else if (speedDiffX < -accel) move.x = cur.x - accel;
        else move.x = goal.x;

        if (speedDiffY > accel) move.y = cur.y + accel;
        else if (speedDiffY < -accel) move.y = cur.y - accel;
        else move.y = goal.y;
        
        return move;
    }

    private void ApplyMovement()
    {
        _rb.velocity = SetVelocity(maxSpeed * _moveInput, _rb.velocity, acceleration);
        
        if (_rb.velocity == Vector2.zero) PlayerStateMachine.StateManager.SetState(PlayerStateMachine.States.Playing);
    }

    private void FlipSprite()
    {
        if (_moveInput.x == -1f) _spr.flipX = true;
        else if (_moveInput.x == 1f) _spr.flipX = false;
    }

    private void GetDashInput()
    {
        if (Input.GetButtonDown("Dash") && canDash && _moveInput != Vector2.zero)
        {
            canDash = false;
            _playDashEffect = true;
            PlayerStateMachine.StateManager.SetState(PlayerStateMachine.States.Dashing);
            StartCoroutine(StopDash(dashTime));
            StartCoroutine(DashInterval(dashInterval));
            StartCoroutine(StopDashEffect(dashEffectTime));
        }
    }

    private void ApplyDash()
    {
        _rb.velocity = Vector2.zero;
        _rb.velocity = _moveInput * dashSpeed;
    }

    private IEnumerator StopDash(float time)
    {
        yield return new WaitForSeconds(time);
        //_rb.velocity = Vector2.zero;
        PlayerStateMachine.StateManager.SetState(PlayerStateMachine.States.Playing);
    }

    private IEnumerator DashInterval(float time)
    {
        yield return new WaitForSeconds(time);
        canDash = true;
    }

    private void SpawnDashEffect()
    {
        var dash = Instantiate(dashEffect, transform.position, Quaternion.identity);
        dash.sprite = _spr.sprite;
        dash.transform.localScale = this.transform.localScale;
    }

    private IEnumerator StopDashEffect(float time)
    {
        yield return new WaitForSeconds(time);
        _playDashEffect = false;
    }
}

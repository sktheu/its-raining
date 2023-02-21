using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine StateManager;
    
    [SerializeField, ReadOnly] private States currentState = States.Playing;
    
    public enum States
    {
        NotPlaying,
        Playing,
        Moving,
        Dashing,
    };

    private void Awake()
    {
        StateManager = this;
    }

    public States GetState()
    {
        return StateManager.currentState;
    }

    public void SetState(States state)
    {
        StateManager.currentState = state;
    }

    public bool CompareState(States state)
    {
        return (StateManager.currentState == state);
    }
}

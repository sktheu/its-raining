using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine stateManager;
    
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
        stateManager = this;
    }

    public static States GetState()
    {
        return stateManager.currentState;
    }

    public static void SetState(States state)
    {
        stateManager.currentState = state;
    }

    public static bool CompareState(States state)
    {
        return (stateManager.currentState == state);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public IState<T> currentState { get; set; }
    private T t;
    public StateMachine(T t)
    {
        this.t = t;
    }
    public void ChangeState(IState<T> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(t);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(t);
        }

    }
    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(t);
        }
    }
}
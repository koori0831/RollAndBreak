using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public EntityState currentState { get; set; }

    private Dictionary<string, EntityState> _stateDictionary;

    public StateMachine(EntityStatesSO entityFSM, Entity owner)
    {
        _stateDictionary = new Dictionary<string, EntityState>();
        
        foreach (StateSO state in entityFSM.states)
        {
            try
            {
                Type type = Type.GetType(state.className);
                var playerState = Activator.CreateInstance(type, owner, state.stateAnim) as EntityState;

                _stateDictionary.Add(state.stateName, playerState);
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>{state.className}</color> loading error, Message : {ex.Message}");
            }
        }
    }

    public EntityState GetState(string stateName)
    {
        return _stateDictionary.GetValueOrDefault(stateName);
    }

    public void UpdateStateMachine()
    {
        currentState.Update();
    }

    public void Initialize(string stateName)
    {
        EntityState startState = GetState(stateName);
        Debug.Assert(startState != null, $"Start state {stateName} is null");

        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(string newState)
    {
        currentState.Exit();
        EntityState nextState = GetState(newState);
        Debug.Assert(nextState != null, $"State : {newState} not Found");

        currentState = nextState;
        currentState.Enter();
    }
}

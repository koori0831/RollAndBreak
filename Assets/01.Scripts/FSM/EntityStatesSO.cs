using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStatesSO", menuName = "SO/FSM/EntityStatesSO")]
public class EntityStatesSO : ScriptableObject
{
    public List<StateSO> states;

    private Dictionary<string, StateSO> _stateDictionary;

    //public StateSO this[string stateName] 
    //    => _stateDictionary.GetValueOrDefault(stateName);

    //private void OnEnable()
    //{
    //    if (states == null) return;

    //    _stateDictionary = new Dictionary<string, StateSO>();
    //    foreach (StateSO state in states)
    //        _stateDictionary.Add(state.stateName, state);
    //}
}

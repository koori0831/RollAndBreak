using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "SO/FSM/State")]
public class StateSO : ScriptableObject
{
    public string stateName;
    public string className;
    public AnimParamSO stateAnim;
}

using UnityEngine;

[CreateAssetMenu(fileName = "AnimParamSO", menuName = "SO/Animator/Param")]
public class AnimParamSO : ScriptableObject
{
    public enum ParamType
    {
        Boolean, Float, Integer, Trigger
    }

    public string paramName;
    public ParamType paramType;
    public int hashValue;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(paramName)) return;
        hashValue = Animator.StringToHash(paramName);
    }
}

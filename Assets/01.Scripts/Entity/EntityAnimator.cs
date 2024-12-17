using System;
using UnityEngine;

public class EntityAnimator : MonoBehaviour, IEntityComponent
{
    public event Action OnAnimationEnd;
    public event Action OnAttackTryEvent;
    protected Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    protected virtual void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }

    protected virtual void AttackTry()
    {
        OnAttackTryEvent?.Invoke();   
    }
}

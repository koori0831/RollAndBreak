using System;
using System.Collections;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IEntityComponent, IAttackable
{
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _knockbackTime = 0.5f;
    
    private Entity _entity;
    private EntityMover _mover; //넉백을 위해서 가져와야해

    public event Action<Entity> OnHitEvent;
    public event Action OnDeathEvent;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
        _mover = entity.GetCompo<EntityMover>();
        _currentHealth = _maxHealth;
    }

    public void ApplyAttack(float damage, Vector2 direction, Vector2 knockBack, Entity dealer)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);
        StartCoroutine(ApplyKnockBack(knockBack));
        OnHitEvent?.Invoke(dealer);
        
        if(_currentHealth <= 0)
            OnDeathEvent?.Invoke();
    }

    private IEnumerator ApplyKnockBack(Vector2 knockBack)
    {
        _mover.CanManualMove = false;
        _mover.StopImmediately(true);
        _mover.AddForceToEntity(knockBack);
        yield return new WaitForSeconds(_knockbackTime);
        _mover.CanManualMove = true;
    }
}

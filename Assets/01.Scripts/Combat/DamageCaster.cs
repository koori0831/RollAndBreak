using UnityEngine;

public abstract class DamageCaster : MonoBehaviour
{
    [SerializeField] protected int _maxAvailableCount = 4;
    [SerializeField] protected float _damage = 5f;
    [SerializeField] protected Vector3 _knockbackForce; // Changed from Vector2 to Vector3
    protected Entity _owner;

    public virtual void InitCaster(Entity owner)
    {
        _owner = owner;
    }

    public abstract void CastDamage();
}
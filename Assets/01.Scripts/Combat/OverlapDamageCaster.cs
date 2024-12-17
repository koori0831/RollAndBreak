using UnityEngine;

public class OverlapDamageCaster : DamageCaster
{
    [SerializeField] private Vector3 _castSize;
    [SerializeField] private LayerMask _layerMask; // Define which layers to check
    [SerializeField] private Transform center;
    private Collider[] _colliders;

    public override void InitCaster(Entity owner)
    {
        base.InitCaster(owner);
        _colliders = new Collider[_maxAvailableCount];
    }

    public override void CastDamage()
    {
        Vector3 halfExtents = _castSize / 2f;

        int cnt = Physics.OverlapBoxNonAlloc(center.position, halfExtents, _colliders, Quaternion.identity, _layerMask, QueryTriggerInteraction.UseGlobal);

        Vector3 atkDirection = _owner.transform.forward; // Attack direction in 3D
        Vector3 knockbackForce = _knockbackForce;
        knockbackForce.x *= atkDirection.x;
        knockbackForce.z *= atkDirection.z; // Adjust Z component if needed

        for (int i = 0; i < cnt; i++)
        {
            if (_colliders[i].TryGetComponent(out IAttackable target))
            {
                target.ApplyAttack(_damage, atkDirection, knockbackForce, _owner);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center.position, _castSize); // Full size for visualization
    }
#endif
}
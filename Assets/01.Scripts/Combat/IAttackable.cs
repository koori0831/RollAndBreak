using UnityEngine;

public interface IAttackable
{
    public void ApplyAttack(float damage, Vector2 direction, Vector2 knockBack, Entity dealer);
}

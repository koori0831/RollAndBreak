using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] protected LayerMask _whatIsPlayer, _whatIsObstacle;

    public float sightRange;
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}

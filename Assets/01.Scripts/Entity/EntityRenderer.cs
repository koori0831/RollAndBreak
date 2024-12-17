using System;
using UnityEngine;

public class EntityRenderer : AnimatorCompo, IEntityComponent
{
    public float FacingDirection { get; private set; } = 1; //오른쪽이 1, 왼쪽이 -1
    private Entity _entity;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    #region Flip Controller
    public void Flip()
    {
        FacingDirection *= -1;
        _entity.transform.localScale = new Vector3(FacingDirection, 1, 1);
    }

    public void FlipController(float xMove)
    {
        if (Mathf.Abs(FacingDirection + xMove) < 0.5f)
            Flip();
    }

    
    #endregion
}

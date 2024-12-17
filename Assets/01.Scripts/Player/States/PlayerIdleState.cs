using UnityEngine;

public class PlayerIdleState : EntityState
{
    private Player _player;
    private EntityMover _mover;

    public PlayerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
    }

    public override void Update()
    {
        base.Update();
        float xMove = _player.PlayerInput.InputDirection.x;
        float zMove = _player.PlayerInput.InputDirection.z;
        if (Mathf.Abs(xMove) > 0 ||Mathf.Abs(zMove) > 0)
            _player.ChangeState("Move");
    }
}

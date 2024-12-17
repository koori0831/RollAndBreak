using System;
using UnityEngine;

public abstract class PlayerAirState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;

    protected PlayerAirState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.SpeedMultiplier = 0.7f;
    }

    public override void Update()
    {
        base.Update();
        Vector3 input = _player.PlayerInput.InputDirection;
        if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.z) > 0)
            _mover.SetMovement(input);
    }

    public override void Exit()
    {
        _mover.SpeedMultiplier = 1f;
        base.Exit();
    }
}

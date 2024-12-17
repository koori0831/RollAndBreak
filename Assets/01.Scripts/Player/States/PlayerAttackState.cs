using Ami.BroAudio;
using UnityEngine;

public class PlayerAttackState : EntityState
{
    private Player _player;
    private EntityMover _mover;

    public PlayerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
        _mover.CanManualMove = false;
        _mover.AddForceToEntity(new Vector2(_renderer.FacingDirection * 1f, 0));
        BroAudio.Play(_player.AttackSound);
    }

    public override void Exit()
    {
        _mover.CanManualMove = true;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(_isTriggerCall)
        {
            _player.ChangeState("Idle");
        }
    }
}

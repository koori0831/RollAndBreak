using Ami.BroAudio;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _mover.AddForceToEntity(Vector3.up * _player.jumpPower);
        BroAudio.Play(_player.JumpSound);
    }

    public override void Update()
    {
        base.Update();
        if(_mover.Velocity.y < 0)
        {
            _player.ChangeState("Fall");
        }
    }
}


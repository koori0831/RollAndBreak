using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    private float currentGravityScale;
    private float targetGravityScale = 1f;
    private float gravityChangeSpeed = 5f; 

    public PlayerFallState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        currentGravityScale = _player.gravityMultiplier;
        _mover.SetGravityScale(currentGravityScale);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        currentGravityScale = Mathf.Lerp(currentGravityScale, targetGravityScale, gravityChangeSpeed * Time.fixedDeltaTime);
        _mover.SetGravityScale(currentGravityScale);
    }

    public override void Exit()
    {
        base.Exit();
        _mover.SetGravityScale(1f);
    }

    public override void Update()
    {
        base.Update();
        if (_mover.IsGrounded)
        {
            _player.ChangeState("Idle");
        }
    }
}
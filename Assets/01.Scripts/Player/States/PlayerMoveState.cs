using UnityEngine;

public class PlayerMoveState : EntityState
{
    private Player _player;
    private EntityMover _mover;

    public PlayerMoveState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Update()
    {
        base.Update();
        float xMove = _player.PlayerInput.InputDirection.x;
        float zMove = _player.PlayerInput.InputDirection.z;

        Vector3 move = new Vector3(xMove, 0, zMove);

        _mover.SetMovement(move);

        if (Mathf.Approximately(xMove, 0) && Mathf.Approximately(zMove, 0))
            _player.ChangeState("Idle");
    }
}

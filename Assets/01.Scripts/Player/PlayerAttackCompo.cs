using System;
using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float _atkCooldown;
    [SerializeField] private StateSO _atkState;
    [SerializeField] private StateSO _fallState;
    [SerializeField] private StateSO _jumpState;
    [SerializeField] private float _dashCooltime;
    [SerializeField] private StateSO _dashState;
    [SerializeField] private DamageCaster _damageCaster;
    
    private Player _player;
    private float _lastAtkTime;
    private float _lastDashTime;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _damageCaster.InitCaster(_player);
    }

    public bool AttemptDash()
    {
        if (_player.CurrentState == _player.GetState(_dashState)) return false;
        if (_lastDashTime + _dashCooltime > Time.time) return false;

        _lastDashTime = Time.time;
        return true;
    }

    public bool AttemptAttack()
    {
        if (_player.CurrentState == _player.GetState(_atkState)) return false;
        if (_player.CurrentState == _player.GetState(_fallState)) return false;
        if (_player.CurrentState == _player.GetState(_jumpState)) return false;
        if (_lastAtkTime + _atkCooldown > Time.time) return false;

        _lastAtkTime = Time.time;
        return true;
    }

    public void CastAttack()
    {
        _damageCaster.CastDamage();
    }
}

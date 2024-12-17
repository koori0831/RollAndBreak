using Ami.BroAudio;
using UnityEngine;

public class Player : Entity
{
    [Header("FSM")]
    [SerializeField] private EntityStatesSO _playerFSM;
    public SoundID AttackSound;
    public SoundID JumpSound;
    public SoundID DashSound;

    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

    public EntityState CurrentState => _stateMachine.currentState;

    public float jumpPower = 12f;
    public int jumpCount = 2;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float gravityMultiplier = 1.5f;

    private int _currentJumpCount = 0;
    private EntityMover _mover;
    private PlayerAttackCompo _atkCompo; // 1
    private EntityAnimator _animator;

    private StateMachine _stateMachine;

    protected override void AfterInit()
    {
        base.AfterInit();

        _mover = GetCompo<EntityMover>();
        _atkCompo = GetCompo<PlayerAttackCompo>(); //2
        _animator = GetCompo<EntityAnimator>();

        _stateMachine = new StateMachine(_playerFSM, this);

        _mover.OnGroundStateChange += HandleGroundStateChange;
        PlayerInput.JumpEvent += HandleJumpEvent;
        PlayerInput.AttackEvent += HandleAttackEvent;
        PlayerInput.DashEvent += HandleDashEvent;

        _animator.OnAnimationEnd += HandleAnimationEnd;
        _animator.OnAttackTryEvent += HandleAttackTry;

        _mover.SetGravityScale(gravityMultiplier);
    }

    private void HandleDashEvent()
    {
        if(_atkCompo.AttemptDash())
        {
            ChangeState("Dash");
        }
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }

    private void HandleAttackEvent()
    {
        if (_atkCompo.AttemptAttack())
            ChangeState("Attack");
    }

    private void HandleAttackTry()
    {
        _atkCompo.CastAttack();
    }

    private void HandleJumpEvent()
    {
        if(_mover.IsGrounded || _currentJumpCount > 0)
        {
            string nextName = _currentJumpCount == jumpCount ? "Jump" : "DoubleJump";
            _currentJumpCount--;

            ChangeState(nextName);
        }
    }

    private void OnDestroy()
    {
        _mover.OnGroundStateChange -= HandleGroundStateChange;
        PlayerInput.JumpEvent -= HandleJumpEvent;
        PlayerInput.AttackEvent -= HandleAttackEvent;
        PlayerInput.DashEvent -= HandleDashEvent;
        GetCompo<EntityAnimator>().OnAnimationEnd -= HandleAnimationEnd;
    }

    private void Start()
    {
        _stateMachine.Initialize("Idle");
    }

    public EntityState GetState(StateSO state) 
        => _stateMachine.GetState(state.stateName);

    public void ChangeState(string newState) 
        => _stateMachine.ChangeState(newState);
    
    private void HandleGroundStateChange(bool isGrounded)
    {
        if (isGrounded)
            _currentJumpCount = jumpCount;
    }

    private void Update()
    {
        _stateMachine.currentState.Update();
    }
}


// 파생 클래스가 기본 클래스를 대체할 수 있어야 한다. 
// 부모가 있던 곳에 자식을 넣으면 잘 굴러가야해
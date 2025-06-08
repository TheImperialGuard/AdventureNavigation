using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, INavigationMovable, IDirectionRotatable, IDamagable
{
    private readonly int _takeDamageKey = Animator.StringToHash("TakeDamage");

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;

    [SerializeField] private int _maxHealth;

    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private Animator _animator;

    private NavMeshAgent _agent;

    private AgentMover _mover;
    private AgentJumper _jumper;

    private DirectionalRotator _rotator;

    private Health _health;

    public Quaternion CurrentRotation => transform.rotation;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public int MaxHealth => _health.MaxHealth;

    public int CurrentHealth => _health.CurrentHealth;

    public bool IsJumping => _jumper.inProcess;

    public bool IsAlive => _health.CurrentHealth > 0;

    public bool CanMove => IsAlive && IsJumping == false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
        _jumper = new AgentJumper(_jumpSpeed, _agent, this, _jumpCurve);
        _health = new Health(_maxHealth);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void SetRotateDirection(Vector3 rotateDirection) => _rotator.SetDirection(rotateDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath path) => NavMeshUtils.TryGetPath(_agent, targetPosition, path);

    public void StopMove() => _mover.StopMove();

    public void ResumeMove() => _mover.ResumeMove();

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        if (CurrentHealth != 0)
            _animator.SetTrigger(_takeDamageKey);
    }

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }
}

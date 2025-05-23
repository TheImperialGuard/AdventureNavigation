using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private const float CharacterMinVelocity = 0.05f;
    private const float MaxHealthFractionForInjure = 0.3f;

    private const int InjuredLayerIndex = 1;

    private readonly int _runningKey = Animator.StringToHash("IsRunning");
    private readonly int _currentHealthKey = Animator.StringToHash("CurrentHealth");

    [SerializeField] private AgentCharacter _character;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetInteger(_currentHealthKey, _character.CurrentHealth);

        if (_character.CurrentVelocity.magnitude > CharacterMinVelocity)
            StartMove();
        else
            StopMove();

        if (_character.CurrentHealth < _character.MaxHealth * MaxHealthFractionForInjure)
            _animator.SetLayerWeight(InjuredLayerIndex, 1);
        else
            _animator.SetLayerWeight(InjuredLayerIndex, 0);
    }

    private void StopMove()
    {
        _animator.SetBool(_runningKey, false);
    }

    private void StartMove()
    {
        _animator.SetBool(_runningKey, true);
    }
}

using System.Collections;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private const float CharacterMinVelocity = 0.05f;
    private const float MaxHealthFractionForInjure = 0.3f;

    private const int InjuredLayerIndex = 1;

    private const string EdgeKey = "_Edge";

    private readonly int _runningKey = Animator.StringToHash("IsRunning");
    private readonly int _jumpingKey = Animator.StringToHash("IsJumping");
    private readonly int _currentHealthKey = Animator.StringToHash("CurrentHealth");

    [SerializeField] private AgentCharacter _character;

    [SerializeField] private float _timeToDissolve;

    private Animator _animator;

    private SkinnedMeshRenderer[] _renderers;

    private Coroutine _dieEffectProcess;

    private float _time;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        _animator.SetInteger(_currentHealthKey, _character.CurrentHealth);

        if (_character.IsAlive == false)
        {
            if (_dieEffectProcess == null)
                _dieEffectProcess = StartCoroutine(DieEffectProcess());
        }

        if (_character.IsJumping)
        {
            _animator.SetBool(_jumpingKey, true);
            return;
        }
        else
        {
            _animator.SetBool(_jumpingKey, false);
        }

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

    private void SetFloatFor(SkinnedMeshRenderer[] renderers, string key, float param)
    {
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.material.SetFloat(key, param);
        }
    }

    private IEnumerator DieEffectProcess()
    {
        _time = 0;

        while (_time < _timeToDissolve)
        {
            _time += Time.deltaTime;

            if (_time > _timeToDissolve)
                _time = _timeToDissolve;

            SetFloatFor(_renderers, EdgeKey, _time / _timeToDissolve);

            yield return null;
        }
    }
}

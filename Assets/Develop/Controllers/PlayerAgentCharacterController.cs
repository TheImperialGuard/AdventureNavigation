using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerAgentCharacterController : Controller
{
    private const float MinDIstanceToTarget = 0.005f;

    private INavigationMovable _movable;

    private ParticleSystem _flagEffectPrefab;

    private LayerMask _mask;

    private Vector3 _targetPosition;

    private Vector3 _startJumpPosition;
    private Vector3 _endJumpPosition;

    private NavMeshPath _path = new NavMeshPath();

    public PlayerAgentCharacterController(INavigationMovable movable, ParticleSystem flagEffect, LayerMask mask)
    {
        _movable = movable;
        _flagEffectPrefab = flagEffect;
        _mask = mask;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
            if (Input.GetKeyDown(KeyCode.Mouse0))
                SetTargetPosition();

        if (_movable.IsJumping)
            Debug.Log("ffjf");

        if (_movable.IsJumping)
            _movable.SetRotateDirection(_endJumpPosition - _startJumpPosition);

        if (_movable.CanMove)
        {
            if (_movable.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
            {
                if (_movable.IsJumping == false)
                {
                    _startJumpPosition = offMeshLinkData.startPos;
                    _endJumpPosition = offMeshLinkData.endPos;

                    _movable.Jump(offMeshLinkData);
                }

                return;
            }
        }

        if (_movable.TryGetPath(_targetPosition, _path) && TargetReached(_path) == false && _movable.CanMove)
        {
            _movable.ResumeMove();
            _movable.SetDestination(_targetPosition);
            return;
        }

        _movable.StopMove();
    }

    private void PlayFlagEffect()
    {
        UnityEngine.Object.Instantiate(_flagEffectPrefab, _targetPosition, Quaternion.identity);
    }

    private void SetTargetPosition()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, _mask))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1f, NavMesh.AllAreas))
            {
                _targetPosition = navHit.position;

                PlayFlagEffect();
            }
        }
    }

    private bool TargetReached(NavMeshPath path) => NavMeshUtils.GetPathLength(path) <= MinDIstanceToTarget;
}

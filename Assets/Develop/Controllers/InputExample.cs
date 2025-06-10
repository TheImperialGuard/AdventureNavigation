using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    [SerializeField] private ParticleSystem _pathFlagEffectPrefab;

    [SerializeField] private LayerMask _navMeshLayer;

    private Controller _characterController;

    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();

        _characterController = new CompositeController
        (
            new PlayerAgentCharacterController(_character, _pathFlagEffectPrefab, _navMeshLayer),
            new AlongMovableRotatableController(_character, _character)
        );

        _characterController.Enable();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);

        if (_character.IsAlive == false)
            _characterController.Disable();
    }

    private void OnDrawGizmos()
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 targetPosition;

        if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1f, NavMesh.AllAreas))
            {
                targetPosition = navHit.position;

                NavMesh.CalculatePath(_character.transform.position, targetPosition, queryFilter, _path);

                Gizmos.color = Color.green;

                if (_path.status != NavMeshPathStatus.PathInvalid)
                    foreach (Vector3 corner in _path.corners)
                        Gizmos.DrawSphere(corner, 0.3f);
            }
        }

    }
}

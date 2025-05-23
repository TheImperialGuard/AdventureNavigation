using UnityEngine;

public class InputExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    [SerializeField] private ParticleSystem _pathFlagEffectPrefab;

    [SerializeField] private LayerMask _navMeshLayer;

    private Controller _characterController;

    private void Awake()
    {
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
}

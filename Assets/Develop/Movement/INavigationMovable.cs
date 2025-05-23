using UnityEngine;
using UnityEngine.AI;

public interface INavigationMovable : IMovable
{
    bool CanMove { get; }

    void SetDestination(Vector3 position);

    bool TryGetPath(Vector3 targetPosition, NavMeshPath path);

    void StopMove();

    void ResumeMove();
}

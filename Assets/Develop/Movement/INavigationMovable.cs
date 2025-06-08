using UnityEngine;
using UnityEngine.AI;

public interface INavigationMovable : IMovable
{
    bool CanMove { get; }

    bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData);

    void SetDestination(Vector3 position);

    void SetRotateDirection(Vector3 rotateDirection);

    bool TryGetPath(Vector3 targetPosition, NavMeshPath path);

    void StopMove();

    void ResumeMove();

    void Jump(OffMeshLinkData offMeshLinkData);
}

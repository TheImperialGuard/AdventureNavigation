using UnityEngine;
using UnityEngine.AI;

public class AgentMover
{
    private NavMeshAgent _agent;

    public AgentMover(NavMeshAgent agent, float moveSpeed)
    {
        _agent = agent;
        _agent.speed = moveSpeed;
        _agent.acceleration = 999;
        _agent.updateRotation = false;
    }

    public Vector3 CurrentVelocity => _agent.desiredVelocity;

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);

    public void StopMove() => _agent.isStopped = true;

    public void ResumeMove() => _agent.isStopped = false;
}

using UnityEngine;

public interface IDirectionRotatable
{
    Quaternion CurrentRotation { get; }

    void SetRotateDirection(Vector3 rotateDirection);
}

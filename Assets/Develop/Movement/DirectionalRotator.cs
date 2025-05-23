using UnityEngine;

public class DirectionalRotator
{
    private Transform _transform;

    private float _rotationSpeed;

    public Vector3 CurrentDirectionToRotate {  get; private set; }

    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public void SetDirection(Vector3 direction) => CurrentDirectionToRotate = direction;

    public void Update(float deltaTime)
    {
        if (CurrentDirectionToRotate.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(CurrentDirectionToRotate);

        float step = _rotationSpeed * Time.deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }
}

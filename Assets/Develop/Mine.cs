using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;

    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    private SphereCollider _collider;

    [field: SerializeField] public int Damage { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();

        _collider.radius = _explosionRadius;
    }

    public void Explose()
    {
        Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        int segments = 36;

        float angleStep = 360f / segments;

        Vector3 center = transform.position;

        Vector3 prevPoint = center + new Vector3(_explosionRadius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep;

            Vector3 nextPoint = center + new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * angle) * _explosionRadius,
                0,
                Mathf.Sin(Mathf.Deg2Rad * angle) * _explosionRadius);

            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}

using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _timeBeforeExplose;

    private SphereCollider _collider;

    private Coroutine _explodeProcess;

    private IExploseReaction _explodeReaction;

    [field: SerializeField] public int Damage { get; private set; }

    public bool IsExplosing => _explodeProcess != null;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();

        _collider.radius = _explosionRadius;

        _explodeReaction = GetComponentInChildren<IExploseReaction>();
    }

    public void StartExploseProcess()
    {
        StartCoroutine(ExplodeProcess());
    }

    private IEnumerator ExplodeProcess()
    {
        yield return new WaitForSeconds(_timeBeforeExplose);

        Explode();
    }

    private void Explode()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider target in targets)
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable != null)
                damagable.TakeDamage(Damage);
        }

        _explodeReaction.ExplosionReact();

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

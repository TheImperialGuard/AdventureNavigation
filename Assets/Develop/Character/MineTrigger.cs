using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    private IDamagable _damagable;

    private void Awake()
    {
        _damagable = GetComponent<IDamagable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Mine mine = other.GetComponent<Mine>();

        if (mine != null)
        {
            _damagable.TakeDamage(mine.Damage);

            mine.Explose();
        }
    }
}

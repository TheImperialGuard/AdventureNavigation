using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mine mine = other.GetComponent<Mine>();

        if (mine != null)
        {
            mine.StartExploseProcess();
        }
    }
}

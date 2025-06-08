using UnityEngine;

public class MineView : MonoBehaviour, IExploseReaction
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    [SerializeField] private AudioClip _explosionSoundClip;


    public void ExplosionReact()
    {
        ParticleSystem explosionEffect = Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);

        AudioSource explosionSound = explosionEffect.GetComponent<AudioSource>();

        explosionSound.pitch = Random.Range(1f, 1.2f);
        explosionSound.PlayOneShot(_explosionSoundClip);
    }
}

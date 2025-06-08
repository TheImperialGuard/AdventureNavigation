using UnityEngine;
using UnityEngine.Audio;

public class AudioHandlerVIew : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private AudioHandler _handler;

    private void Awake()
    {
        _handler = new AudioHandler(_mixer);
    }

    public void SwitchMusic() => _handler.SwitchMusic();

    public void SwitchSounds() => _handler.SwitchSounds();
}

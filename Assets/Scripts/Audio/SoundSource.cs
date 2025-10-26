using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSource : MonoBehaviour
{
    // [SerializeField] AudioClip hoverButtonSound, clickButtonSound;
    AudioSource audioSource;
    public static SoundSource instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        
    }
}

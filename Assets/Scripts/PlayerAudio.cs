using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip itemSound;
    public AudioClip jumpSoud;
    public AudioClip hitSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip sfx)
    {
        audioSource.PlayOneShot(sfx);
    }
}

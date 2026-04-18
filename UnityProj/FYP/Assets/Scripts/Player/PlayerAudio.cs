using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [Space]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip walkSound;
    [Space]
    [SerializeField] private float walkSoundVolume = 0.5f;
    [SerializeField] private float jumpSoundVolume = 0.5f;
    private void Awake() {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }
    public void PlayJumpSound() {
        audioSource.PlayOneShot(jumpSound, jumpSoundVolume);
    }
    public void PlayWalkSound() {
        audioSource.PlayOneShot(walkSound, walkSoundVolume);
    }
}

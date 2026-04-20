using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource talkSource;

    [Space]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip talkSound;

    [Space]
    [SerializeField] private float walkSoundVolume = 0.5f;
    [SerializeField] private float jumpSoundVolume = 0.5f;
    [SerializeField] private float talkSoundVolume = 0.5f;

    [Space]
    [SerializeField] private float talkPitch = 0.75f;
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
    public void PlayTalkSound() {
        talkSource.PlayOneShot(talkSound, talkSoundVolume);
    }
}

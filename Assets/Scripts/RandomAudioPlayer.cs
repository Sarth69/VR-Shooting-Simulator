using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    public void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    public void playRandomAudio(AudioClip[] clips)
    {
        m_MyAudioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioMan : MonoBehaviour
{
  
    public AudioClip[] audios;
    public AudioClip currentClip;
    private AudioSource audiosource;
    void PlayNewClip()
    {
        currentClip = audios[Random.Range(0, audios.Length)];
        audiosource.clip = currentClip;
        audiosource.Play();
        StartCoroutine(WaitForSound(currentClip));
    }
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        currentClip = audios[0];
        audiosource.clip = currentClip;
        audiosource.Play();
        StartCoroutine(WaitForSound(currentClip));
    }

    public IEnumerator WaitForSound(AudioClip Sound)
    {
        yield return new WaitUntil(() => audiosource.isPlaying == false);
        // or yield return new WaitWhile(() => audiosource.isPlaying == true);
       
            PlayNewClip();
    }
}

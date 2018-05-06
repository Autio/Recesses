using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public GameObject obj;
    public AudioClip[] audios;
    private AudioSource audiosource;

    void PlayNewClip()
    {
        audiosource.clip = audios[Random.Range(0, audios.Length)];
    }

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = audios[0];
        audiosource.Play();
        StartCoroutine(WaitForSound(audio));
    }

    public IEnumerator WaitForSound(AudioClip Sound)
    {
        yield return new WaitUntil(() => audiosource.isPlaying == false);
        // or yield return new WaitWhile(() => audiosource.isPlaying == true);
        if (obj != null)
            PlayNewClip(); //Do something
    }
}

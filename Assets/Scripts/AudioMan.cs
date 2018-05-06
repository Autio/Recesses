using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioMan : MonoBehaviour
{
    public GameObject obj;
    public AudioClip audio;
    private AudioSource audiosource;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = audio;
        audiosource.Play();
        StartCoroutine(WaitForSound(audio));
    }

    public IEnumerator WaitForSound(AudioClip Sound)
    {
        yield return new WaitUntil(() => audiosource.isPlaying == false);
        // or yield return new WaitWhile(() => audiosource.isPlaying == true);
        if (obj != null)
            obj.SetActive(true); //Do something
    }
}

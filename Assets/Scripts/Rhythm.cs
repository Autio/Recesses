using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rhythm : MonoBehaviour {
    public int bpm = 120;
    private float originalScale;
    public float beat;
    public int nthBeat;
    public float intensity = 0.5f;
    private float starter;
	// Use this for initialization
	void Start () {
        originalScale = transform.localScale.x;
        beat = 60f / (float)bpm / 2.0f;
        nthBeat -= 1;
        starter = Random.Range(0, 0.8f);
        intensity *= Random.Range(0.8f, 1.2f);
	}
	
	// Update is called once per frame
	void Update () {
        starter -= Time.deltaTime;
        if(starter < 0)
        {
            Pulsate();
            starter = 1000000;
        }
	}

    void Pulsate()
    {
        Sequence pulse = DOTween.Sequence();
        pulse.Append(transform.DOScale(originalScale + intensity, beat));
        pulse.Append(transform.DOScale(originalScale, beat));
        pulse.Append(transform.DOScale(originalScale, beat * 2 * nthBeat)); // nthBeat 1, every beat. nthBeat 2, every other 
        pulse.SetLoops(-1, LoopType.Restart);
    }


}

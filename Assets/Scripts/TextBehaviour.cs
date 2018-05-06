using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextBehaviour : MonoBehaviour {

    void Birth()
    {
        Sequence seq = DOTween.Sequence();
        float adjustment = 1f;
        // Appearance
        seq.Append(this.transform.DOScale(0.013f / adjustment, 0.3f));
        seq.Append(this.transform.DOScale(0.008f / adjustment, 0.4f));
        seq.Append(this.transform.DOScale(0.01f / adjustment, 0.4f));
        seq.Append(this.transform.DOScale(0.0001f / adjustment, 9f));
        Destroy(this, 9.0f);
    }
	// Use this for initialization
	void Start () {
        Birth();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
} 

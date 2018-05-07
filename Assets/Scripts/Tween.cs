using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tween : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Sequence seq = DOTween.Sequence();        // Appearance
        seq.Append(this.transform.DOLocalMoveY(0, 3.5f));
        seq.Append(this.transform.DOLocalMoveY(50, 5.5f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

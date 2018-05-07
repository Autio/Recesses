using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EndingTween : MonoBehaviour {

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Move()
    {
        Sequence seq = DOTween.Sequence();        // Appearance
        seq.Append(this.transform.DOLocalMoveY(0, 3.5f));
    }
}

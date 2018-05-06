using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NudgeBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Pull();
        Birth();
	}
    void Birth()
    {
        Sequence seq = DOTween.Sequence();
        float adjustment = 1f;
        // Appearance
        seq.Append(this.transform.DOScale(4f / adjustment, 0.5f));
        seq.Append(this.transform.DOScale(0.5f / adjustment, 0.8f));
        seq.Append(this.transform.DOScale(2f / adjustment, 0.4f));
        seq.Append(this.transform.DOScale(0.0001f / adjustment, 2f));
        Destroy(this, 9.0f);
    }
    // Update is called once per frame
    void Update () {
		
	}

    // bring persons near towards the centre roughly
    void Pull()
    {
        Collider2D[] nearbyPersons = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach(Collider2D c in nearbyPersons)
        {
            if (c.transform.tag == "Person")
            {
                Vector2 dir = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f) - c.transform.position.x, transform.position.y + Random.Range(-0.5f, 0.5f) - c.transform.position.y) * 10;
                c.transform.GetComponent<Rigidbody2D>().AddForce(dir);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
    public float speed = 2.0f;
    private float impulseCounter = 1.0f;

    private GameController gameController;

    // attributes
    public string badThing;


    public GameObject textObj;
	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
        // pick bad thing from options
        badThing = gameController.badThoughts[Random.Range(0, gameController.badThoughts.Length)];
        Debug.Log(badThing);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        impulseCounter -= Time.deltaTime;
        if(impulseCounter < 0)
        {
            impulseCounter = Random.Range(0.6f, 1.6f);
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f) * speed));
        }
    }

    private void CreateText(string s)
    {
        GameObject g = Instantiate(textObj, transform.position, Quaternion.identity);
        g.GetComponent<TextMesh>().text = s;
    }

    // collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CreateText("Wow");

    }
}

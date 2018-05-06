using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Person : MonoBehaviour {

    public bool autonomous = true;

    public float speed = 2.0f;
    private float impulseCounter = 1.0f;

    private GameController gameController;

    public Color[] outerCircleColours;
    public Color[] innerCircleColours;

    // attributes
    public string badThing;


    public GameObject textObj;
	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
        // pick bad thing from options
        badThing = gameController.badThoughts[Random.Range(0, gameController.badThoughts.Length)];
        // colour circles
        this.transform.Find("OuterCircle").GetComponent<SpriteRenderer>().color = outerCircleColours[Random.Range(0, outerCircleColours.Length)];
        this.transform.Find("InnerCircle").GetComponent<SpriteRenderer>().color = innerCircleColours[Random.Range(0, innerCircleColours.Length)];

    }

    // Update is called once per frame
    void Update () {
		
	}

    public Vector2 queuedForce = new Vector2(0,0);

    private void FixedUpdate()
    {
        
        impulseCounter -= Time.deltaTime;
        if (autonomous)
        {
            if (impulseCounter < 0)
            {
                impulseCounter = Random.Range(0.6f, 1.6f);
                Vector2 dir = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f) * speed);
                transform.GetComponent<Rigidbody2D>().AddForce(dir);

               
                    // find index of this unit
                    int index = 0;
                    foreach (GameObject g in gameController.personList)
                    {

                        if (g == this.gameObject)
                        {
                            break;
                        }
                        else
                        {
                            index++;
                        }
                                            
                    }
                    // Only copy the move if the player has never intervened yet
                    if (!gameController.interacted)
                    {
                        // Move the simulated copy the same - until an interaction has been made
                        gameController.simulatedPersonList[index].GetComponent<Person>().queuedForce = dir;
                    } else
                    {
                        Vector2 dir2 = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f) * speed);
                        gameController.simulatedPersonList[index].GetComponent<Person>().queuedForce = dir2;

                }
            } 
            
        } else
        {
            if(queuedForce != new Vector2(0,0))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(queuedForce);
                queuedForce = new Vector2(0,0);
            }
        }

    }

    private void CreateText(string s)
    {
        GameObject g = Instantiate(textObj, transform.position + new Vector3(0, Random.Range(-0.5f, 0.5f),-2), Quaternion.identity);
        g.GetComponent<TextMesh>().text = s;
    }

    // collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Positive collision
        // There is a 1/5 chance of saying something
        if (collision.transform.tag == "Person")
        {
            if (Random.Range(0, 10) < 1)
            {
                CreateText(gameController.interactionThoughts[Random.Range(0, gameController.interactionThoughts.Length)]);
            }
        }
    }
}

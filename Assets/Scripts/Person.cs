using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Person : MonoBehaviour {

    public bool autonomous = true;
    public int interactions = 0;
    public bool bully = false;
    private float talkCooldown = 2.0f;

    public float speed = 2.0f;
    private float impulseCounter = 1.0f;

    private GameController gameController;

    public Color[] outerCircleColours;
    public Color[] innerCircleColours;

    // attributes
    public string badThing;
    [Range(0,200)]
    public int happiness = 200;

    public GameObject textObj;
	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
        // pick bad thing from options
        badThing = gameController.badThoughts[Random.Range(0, gameController.badThoughts.Length)];
        // colour circles
        this.transform.Find("OuterCircle").GetComponent<SpriteRenderer>().color = outerCircleColours[Random.Range(0, outerCircleColours.Length)];
        this.transform.Find("InnerCircle").GetComponent<SpriteRenderer>().color = innerCircleColours[1];

        if(bully)
        {
            this.transform.Find("OuterCircle").GetComponent<SpriteRenderer>().color = Color.red;
            this.transform.Find("InnerCircle").GetComponent<SpriteRenderer>().color = Color.gray;
            speed = speed * 1.5f;

        }



    }

    // Update is called once per frame
    void Update () {
		
	}

    public Vector2 queuedForce = new Vector2(0,0);
    int happinessIncrement = 8;
    void Sadder()
    {
        if(!bully)
        { 
            happiness -= happinessIncrement;
            if(!autonomous)
            {
                happiness -= 4;
            }
            float lerpo = Mathf.Round(happiness) / 200.0f;
            Debug.Log(lerpo);
            this.transform.Find("InnerCircle").GetComponent<SpriteRenderer>().color = Color.Lerp(innerCircleColours[0], innerCircleColours[1], lerpo );
        }
    }

    void Happier()
    {
        if (!bully)
        {
            happiness += 50;
            if (happiness >= 200)
            {
                happiness = 200;
            }
            float lerpo = Mathf.Round(happiness) / 200.0f;
            Debug.Log(lerpo);
            this.transform.Find("InnerCircle").GetComponent<SpriteRenderer>().color = Color.Lerp(innerCircleColours[0], innerCircleColours[1], lerpo);
        }
    }

    private void FixedUpdate()
    {
        talkCooldown -= Time.deltaTime;
        impulseCounter -= Time.deltaTime;
        bool sadder = Random.Range(0, 10) < 4;

        if (autonomous)
        {
            if (impulseCounter < 0)
            {

                if (sadder)
                {
                    // remove happiness
                    Sadder();
                }
                impulseCounter = Random.Range(0.6f, 1.6f);

                if (happiness > 35)
                {
                   
                    Vector2 dir = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f) * speed);
                    
                    transform.GetComponent<Rigidbody2D>().AddForce(dir);

                        if (Random.Range(0, 100) < 1)
                        {
                            if (happiness > 50)
                            {
                            if (!bully)
                            {
                                CreateText(gameController.idleThoughts[Random.Range(0, gameController.idleThoughts.Length)], Color.white);
                            }
                            }
                            else
                            {
                                CreateText(badThing, Color.gray);
                            }
                        }


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

                    }
                    else
                    {
                        Vector2 dir2 = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f) * speed);
                        gameController.simulatedPersonList[index].GetComponent<Person>().queuedForce = dir2;

                    }
                    if (sadder)
                    {
                        gameController.simulatedPersonList[index].GetComponent<Person>().Sadder();
                    }
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

    private void CreateText(string s, Color textColor, float x = 0, float y = 0)
    {
        if (gameController.state == GameController.gameStates.playing)
        {
            Vector3 pos;
            if (x == 0 && y == 0)
            {
                float offset = Random.Range(-0.6f, -0.3f);
                if (Random.Range(1, 10) < 5)
                {
                    offset = -offset;
                }
                pos = transform.position + new Vector3(0, offset, -2);
            }
            else
            {
                pos = new Vector3(x, y, 0);
            }

            GameObject g = Instantiate(textObj, pos, Quaternion.identity);
            g.GetComponent<TextMesh>().text = s;
            g.GetComponent<TextMesh>().color = textColor;
        }
    }

    // collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameController.state == GameController.gameStates.playing)
        {
            // Positive collision
            // increase happiness

            // There is a 1/5 chance of saying something
            if (collision.transform.tag == "Person")
            {

                if (autonomous)
                {
                    gameController.personInteractions += 1;
                }
                else
                {
                    gameController.simulatedPersonInteractions += 1;
                }

                if (collision.transform.GetComponent<Person>().bully)
                {
                    Sadder();
                    if (talkCooldown < 0)
                    {
                        if (Random.Range(0, 10) < 7)
                        {
                            CreateText(gameController.bullyThoughts[Random.Range(0, gameController.bullyThoughts.Length)], Color.black, collision.transform.position.x, collision.transform.position.y);
                        }
                        talkCooldown = 2.0f;
                    }

                }
                else
                {
                    Happier();
                    if (!bully)
                    {
                        if (talkCooldown < 0)
                        {
                            if (Random.Range(0, 20) < 3)
                            {

                                if (happiness > 75)
                                {
                                    Color c = Color.blue;
                                    if(Random.Range(1,10) < 5)
                                    {
                                        c = Color.blue;
                                    }
                                    CreateText(gameController.interactionThoughts[Random.Range(0, gameController.interactionThoughts.Length)], c);
                                }
                                else
                                {
                                    CreateText(badThing, Color.gray);
                                }
                            }
                            talkCooldown = 2.0f;
                        }
                    }

                }
            }
        }
    }
    }


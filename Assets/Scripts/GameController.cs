using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float gametime = 100;
    public int bullies = 2;

    public enum gameStates {starting, playing, ending };
    public gameStates state = gameStates.starting;

    public int persons;
    public GameObject person;
    public GameObject nudge;

    private int interactionCount = 10;
    private int bumpCount;

    public string[] interactionThoughts;
    public string[] badThoughts;
    public string[] bullyThoughts;

    public int personInteractions;
    public int simulatedPersonInteractions;

    public bool interacted = false;

    public List<GameObject> personList = new List<GameObject>();
    // Simulate a non-intervention scenario
    public List<GameObject> simulatedPersonList = new List<GameObject>();

    public int depressedPersons = 0;
    public int simulatedDepressedPersons = 0;

    public void countDepressed()
    {
        GameObject.Find("Time").GetComponent<TextMesh>().text = "Time: " + Mathf.FloorToInt(gametime).ToString();

        depressedPersons = 0;
        simulatedDepressedPersons = 0;
        foreach(GameObject g in personList)
        {
            if(g.GetComponent<Person>().happiness < 75)
            {
                depressedPersons += 1;
            }
        }
        foreach(GameObject g in simulatedPersonList)
        {
            if(g.GetComponent<Person>().happiness < 75)
            {
                simulatedDepressedPersons += 1;
            }
        }

        Debug.Log("Calculating");

        GameObject.Find("Interactions").GetComponent<TextMesh>().text = "Interactions between people: " + personInteractions.ToString();
        GameObject.Find("DepressedPeople").GetComponent<TextMesh>().text = "Depressed people: " + depressedPersons.ToString();
        GameObject.Find("Interventions").GetComponent<TextMesh>().text = "Interventions left: " + interactionCount.ToString();

        GameObject.Find("SimulatedInteractions").GetComponent<TextMesh>().text = "Interactions in simulation: " + simulatedPersonInteractions.ToString();
        GameObject.Find("SimulatedDepressedPeople").GetComponent<TextMesh>().text = "Depressed people in simulation: " + simulatedDepressedPersons.ToString();
        //GameObject.Find("Sim").GetComponent<TextMesh>().text = "Interventions: " + interactionCount.ToString();

        Debug.Log("Depresseds: " + depressedPersons.ToString());
        Debug.Log("Simulated Depresseds: " + simulatedDepressedPersons.ToString());

    }

    float ticker = 5.0f;
    // Use this for initialization
    void Start () {


		for(int i = 0; i < persons; i++)
        {

            Vector2 pos = new Vector2(Random.Range(-7, 7), Random.Range(-4, 4));
            GameObject p = Instantiate(person, pos, Quaternion.identity);
            personList.Add(p);
            

            GameObject simP = Instantiate(person, pos + new Vector2(25, 0), Quaternion.identity);
            simulatedPersonList.Add(simP);
            simP.GetComponent<Person>().autonomous = false;

            if (bullies > 0)
            {
                p.GetComponent<Person>().bully = true;
                simP.GetComponent<Person>().bully = true;
                bullies -= 1;
            }
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (state == gameStates.starting)
        {
            ticker -= Time.deltaTime;
            if(ticker < 0)
            {
                state = gameStates.playing;
            }

            // Bring intro sheet up

        }

        if (state == gameStates.playing)
        {
            gametime -= Time.deltaTime;
            if (gametime < 0)
            {
                state = gameStates.ending;
            }

            ticker -= Time.deltaTime;
            if(ticker<0)
            {
                countDepressed();
                ticker = 1.0f;
            }


      
        float distance = 50f;
            if (interactionCount > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    interactionCount -= 1;
                    interacted = true;
                    //create a ray cast and set it to the mouses cursor position in game
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        //draw invisible ray cast/vector
                        Debug.DrawLine(ray.origin, hit.point);
                        //log hit area to the console
                        Debug.Log(hit.point);
                        Instantiate(nudge, hit.point, Quaternion.identity);



                    }

                }
            }
		if(Input.GetKeyDown(KeyCode.I))
        {
            interacted = true;
        }
        }

        if (state == gameStates.ending)
        {
            // show results sheet
            GameObject.Find("EndTitle").GetComponent<EndingTween>().Move();
            // ask to restart
        }
    }

}

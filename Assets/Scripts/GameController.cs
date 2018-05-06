using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int persons;
    public GameObject person;
    public GameObject nudge;

    private int interactionCount = 0;
    private int bumpCount;

    public string[] interactionThoughts;
    public string[] badThoughts;

    public bool interacted = false;

    public List<GameObject> personList = new List<GameObject>();
    // Simulate a non-intervention scenario
    public List<GameObject> simulatedPersonList = new List<GameObject>();

    // Use this for initialization
    void Start () {


		for(int i = 0; i < persons; i++)
        {

            Vector2 pos = new Vector2(Random.Range(-6, 6), Random.Range(-4, 4));
            GameObject p = Instantiate(person, pos, Quaternion.identity);
            personList.Add(p);

            GameObject simP = Instantiate(person, pos + new Vector2(25, 0), Quaternion.identity);
            simulatedPersonList.Add(simP);
            simP.GetComponent<Person>().autonomous = false;
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float distance = 50f;
        if(Input.GetMouseButtonDown(0))
        {

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
		if(Input.GetKeyDown(KeyCode.I))
        {
            interacted = true;
        }
	}

    void Nudge()
    {
        // Create nudge in the clicked location

    }
}

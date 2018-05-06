using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int persons;
    public GameObject person;

    public string[] interactionThoughts;
    public string[] badThoughts;

    List<GameObject> personList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		for(int i = 0; i < persons; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-6, 6), Random.Range(-4, 4));
            GameObject p = Instantiate(person, pos, Quaternion.identity);
            personList.Add(p);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

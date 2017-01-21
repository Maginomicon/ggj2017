using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
    public GameObject[] obstacles;
    public int count = 10;

	// Use this for initialization
	void Start () {
        BoxCollider2D bounds = GetComponent<BoxCollider2D>();
        
        for (int i = 0; i < count; i++)
        {
            
            Instantiate(obstacles[Random.Range(0, obstacles.Length-1)],
                new Vector3(Random.Range(-bounds.size.x/4, bounds.size.x/4),
                Random.Range(-bounds.size.y/2, bounds.size.y/2),
                this.transform.position.z), Quaternion.Euler(0,0,Random.Range(0f, 360f)));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

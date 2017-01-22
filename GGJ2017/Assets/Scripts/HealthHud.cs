using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHud : MonoBehaviour {

    public GameObject[] playerHealth;

	// Use this for initialization
	void Start () {
		
	}

    public void takeDamage(int player, int health)
    {
        for(int i = 0; i < health; i++)
        {
            playerHealth[player - 1].GetComponent<Lives>().disableLife();
        }
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    takeDamage(Random.Range(1,5), 1);
        //}
	}
}

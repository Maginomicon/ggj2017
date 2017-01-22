using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour {

    public PlayerAssigner pAss;
	// Use this for initialization
	void Start () {
        pAss = GameObject.Find("GameController").GetComponent<PlayerAssigner>();
        if(pAss != null)
        {
            for(int i = 0; i < pAss.players.Length; i++)
            {
                if (pAss.players[i].hasJoined)
                {
                    GameObject new_player = Instantiate<GameObject>(pAss.Player_Prefab, new Vector3(0,0,0), Quaternion.identity);
                    new_player.GetComponent<Movement>().playerNum = pAss.players[i].joyStick;
                    new_player.GetComponent<SpriteRenderer>().color = pAss.players[i].color;
                    new_player.GetComponent<Movement>().my_color = pAss.players[i].color;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

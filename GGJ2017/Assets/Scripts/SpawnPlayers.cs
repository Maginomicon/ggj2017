using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour {

    public PlayerAssigner pAss;
    private PlayerManager player_manager_;

    public GameObject[] spawnPoints;
    private ArrayList spawnsUsed = new ArrayList();

	// Use this for initialization
	void Start () {
        pAss = GameObject.Find("GameController").GetComponent<PlayerAssigner>();
        player_manager_ = GameObject.Find("GameController").GetComponent<PlayerManager>();

        if(player_manager_ == null)
        {
            Debug.Log("I have no reference to a player manager!");
        }

        if (pAss != null)
            if (pAss != null)
        {
                List<Movement> player_movs = new List<Movement>();
                for(int i = 0; i < pAss.players.Length; i++)
                {
                    int r;
                    while (spawnsUsed.Contains(r = Random.Range(0, 9)))
                    {

                    }



                    if (pAss.players[i].hasJoined)
                    {
                        GameObject new_player = Instantiate<GameObject>(pAss.Player_Prefab, spawnPoints[r].transform.position, Quaternion.identity);
                        new_player.GetComponent<Movement>().playerNum = pAss.players[i].joyStick;
                        new_player.GetComponent<SpriteRenderer>().color = pAss.players[i].color;
                        new_player.GetComponent<Movement>().my_color = pAss.players[i].color;
                        new_player.GetComponent<Movement>().playerClass = pAss.players[i];

                        player_movs.Add(new_player.GetComponent<Movement>());
                    
                    }

                    
                }
                player_manager_.SetPlayerReferences(player_movs);
            }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

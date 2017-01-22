using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAssigner : MonoBehaviour {

    public GameObject Player_Prefab;
    public Player[] players;
    public SelectCard[] cards;

    private int player_count_;
    private PlayerManager player_manager_;

    bool state_player_select = true;

    bool canStart = false;
	// Use this for initialization
	void Start () {
        List<Player> li = new List<Player> ();
        for(int i = 1; i <= 11; i++)
        {
            Player p = gameObject.AddComponent<Player>();
            p.joyStick = i;
            li.Add(p);
        }
        players = li.ToArray();

        player_manager_ = gameObject.GetComponentInChildren<PlayerManager>();
        if(player_manager_ == null)
        {
            Debug.Log("I have no reference to the player manager.");
        }
        DontDestroyOnLoad(gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
        if (state_player_select)
        {
            for (int i = 0; i <= 10; i++)
            {

                if (Input.GetButtonDown("A_" + (i + 1)) && players[i].selectCard == null)
                {
                    for (int j = 0; j < cards.Length; j++)
                    {

                        if (cards[j].owner == null)
                        {
                            Debug.Log("Assigning Card " + j);
                            cards[j].owner = players[i];
                            players[i].selectCard = cards[j];
                            break;
                        }
                        else
                        {
                            Debug.Log(cards[j].owner.joyStick);
                        }
                    }
                }
                else if (Input.GetButtonUp("Start_" + (i + 1)) && canStart)
                {
                    if (player_manager_ == null)
                    {
                        player_manager_ = gameObject.GetComponentInChildren<PlayerManager>();
                    }
                    if (player_manager_ == null)
                    {
                        Debug.Log("I still have no reference to the player manager.");
                    }
                    // Communicate with player manager before loading the core game
                    player_manager_.SetNumPlayers(player_count_);
                    state_player_select = false;
                    SceneManager.LoadScene("Playground");
                }

            }

            player_count_ = countPlayers();

            if (player_count_ >= 2)
            {
                canStart = true;
            }
        }
        
    }

    int countPlayers()
    {
        int count = 0;
        
        for (int j = 0; j < cards.Length; j++)
        {
            
            if (state_player_select && cards[j]
                .owner != null)
            {
                count++;
            }
        }

        return count;
    }
}

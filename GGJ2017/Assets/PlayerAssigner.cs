using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAssigner : MonoBehaviour {

    public GameObject Player_Prefab;
    public Player[] players;
    public SelectCard[] cards;

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
        DontDestroyOnLoad(gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i <= 10; i++)
        {
            
            if(Input.GetButtonDown("A_" + (i+1)) && players[i].selectCard == null)
            {
                Debug.Log("A_"+(i+1));
                for (int j = 0; j < cards.Length; j++)
                {
                    
                    if(cards[j].owner == null)
                    {
                        Debug.Log("Assigning Card " + j);
                        cards[j].owner = players[i];
                        players[i].selectCard = cards[j];
                        break;
                    }else
                    {
                        Debug.Log(cards[j].owner.joyStick);
                    }
                }
            }else  if(Input.GetButtonUp("Start_" + (i+1)))
            {
                SceneManager.LoadScene("Playground");
            }

        }
        int count = 0;

        for (int j = 0; j < cards.Length; j++)
        {

            if (cards[j].owner != null)
            {
                count++;
            }
        }
        if(count >= 2)
        {
            canStart = true;
        }
    }
}

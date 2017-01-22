using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    private int num_players_;
    private List<Movement> players_ = new List<Movement>();

    // Use this for initialization
    void Start()
    {

    }

    public void SetNumPlayers(int num)
    {
        num_players_ = num;
    }

    public void SetPlayerReferences(List<Movement> players)
    {
        players_ = players;

        if(players_.Count != num_players_)
        {
            Debug.Log("I believe I have the wrong amount of players...: expexted " + num_players_ + ", got " + players_.Count );
        }

        foreach(Movement player in players_)
        {
            player.RegisterPlayerManager(this);
        }
    }

    public void PlayerHitCallBack()
    {
        if (NumSurvivors() <= 1)
        {
            SceneManager.LoadScene("Win Screen");
        }

    }

    public int NumSurvivors()
    {
        int num_survivors = 0;
        foreach (Movement player in players_)
        {
            if (player != null && player.GetHealth() > 0)
            {
                num_survivors++;
            }
        }
        return num_survivors;
    }

	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    private int num_players_;
    private List<Movement> players_ = new List<Movement>();

    private Color winning_color_;
    private int winning_player_num_;

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
            SetWinningPlayerDetails();
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

    public void SetWinningPlayerDetails()
    {
        if (NumSurvivors() != 1)
        {
            Debug.Log("Attention: there are " + NumSurvivors() + " survivers");
            return;
        }

        foreach (Movement player in players_)
        {
            if (player != null && player.GetHealth() > 0)
            {
                winning_player_num_ = player.GetPlayerClass().playerNum;
                winning_color_ = player.my_color;
                return;
            }
        }

        Debug.Log("Could not set the details needed by the player win screen");

        return;
    }

    public int GetWinningPlayerNum()
    {
        return winning_player_num_;
    }

    public Color GetWinningPlayerColor()
    {
        return winning_color_;
    }

	// Update is called once per frame
	void Update () {
		
	}
}

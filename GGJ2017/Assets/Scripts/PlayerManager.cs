using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    private int num_players_;
    private List<Movement> players_ = new List<Movement>();

    private Color winning_color_;
    private int winning_player_num_;

    private bool end_game_;
    private float switchToEndGameScreenTime;
    public float end_game_delay = 3f;

    // Use this for initialization
    void Start()
    {
        end_game_ = false;
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
            end_game_ = true;
            switchToEndGameScreenTime = Time.time + end_game_delay;

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


	void FixedUpdate () {
		if(end_game_ && Time.time >= switchToEndGameScreenTime)
        {
            end_game_ = false;
            SceneManager.LoadScene("Win Screen");
        }
	}
}

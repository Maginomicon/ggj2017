using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetWinText : MonoBehaviour {
    PlayerManager player_manager_;
    Text my_gui_text_;
    RawImage player_image_;
    public SpriteRenderer win_Sprite;

	// Use this for initialization
	void Start () {
        player_manager_ = GameObject.FindObjectOfType<PlayerManager>();
        if (player_manager_ == null)
        {
            Debug.Log(" I could not find the player manager");
        }

        my_gui_text_ = gameObject.transform.parent.GetComponentInChildren<Text>();
        if(my_gui_text_ == null)
        {
            Debug.Log("I cannot find my own text");
        }

        player_image_ = gameObject.transform.parent.GetComponentInChildren<RawImage>();
        if (player_image_ == null)
        {
            Debug.Log("I cannot find my image");
        }
    }
	
	void FixedUpdate () {
        SetText();
        for(int i = 1; i < 12; i++)
        {
            if(Input.GetButtonUp("Start_" + i))
            {
                Destroy(GameObject.Find("GameController"));
                SceneManager.LoadScene("Hud");
            }
        }
	}

    void SetText()
    {
        if(player_manager_ == null)
        {
            Debug.Log("could not find winning player!!");
            return;
        }

        my_gui_text_.text = "Player " + player_manager_.GetWinningPlayerNum() + " won the game.";
        win_Sprite.color = player_manager_.GetWinningPlayerColor();
        //player_image_.color = player_manager_.GetWinningPlayerColor();
    }
}

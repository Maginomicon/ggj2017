using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
public class Player : MonoBehaviour{

    public bool hasJoined;
    public int joyStick;
    public SelectCard selectCard;
    bool filling = false;
    public Color color;

    float timeElapsed = 0f;

    public void clearSelectCard()
    {
        this.selectCard = null;
    }

    void Start()
    {

    }

    void Update()
    {
        if(selectCard != null)
        {
            if (Input.GetButton("A_" + joyStick))
            {
                timeElapsed += Time.deltaTime*2;
                selectCard.drawFill(Mathf.Clamp01(timeElapsed));
                if (timeElapsed >= 1)
                {
                    color = selectCard.gameObject.GetComponent<Image>().color;

                    hasJoined = true;
                    //selectCard.gameObject.SetActive(false);
                }
            }
            else if (Input.GetButtonUp("A_" + joyStick))
            {
                if (timeElapsed >= 1)
                {
                    Debug.Log("Player " + selectCard.player + " is Joystick " + joyStick);
                    color = selectCard.gameObject.GetComponent<Image>().color;
                    
                    hasJoined = true;
                    //selectCard.gameObject.SetActive(false);
                }
                else
                {
                    timeElapsed = 0;
                    selectCard.drawFill(0);
                    selectCard.owner = null;
                    selectCard = null;
                }
            }
        }
        
        
    }
}

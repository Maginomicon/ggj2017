using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{

    public GameObject playerCard;
    public GameObject playerSelectArea;

    public List<GameObject> cards = new List<GameObject>();
    List<Player> players = new List<Player>();

    // Use this for initialization
    void Start()
    {
        for(int i = 1; i < 12; i++)
        {
            players.Add(new Player(i));
        }
        //cards = new List<GameObject>();

        //Add First Card on Start
        //GameObject new_card = Instantiate(playerCard);
        //new_card.transform.SetParent(playerSelectArea.transform);
        //cards.Add(new_card);

        //resizePlayerCards();
    }

    //void addPlayer(int joystick)
    //{
    //    GameObject new_card = Instantiate(playerCard);
    //    new_card.transform.SetParent(playerSelectArea.transform);

    //    cards.Add(new_card);

    //    resizePlayerCards();
    //}

    //void resizePlayerCards()
    //{
    //    for (int c = 0; c < cards.Count; c++)
    //    {
    //        RectTransform rt = cards[c].GetComponent<RectTransform>();
    //        rt.anchorMin = new Vector2(0.05f, (c + spacing) / (float)cards.Count);
    //        rt.anchorMax = new Vector2(0.95f, (c + 1 - spacing) / (float)cards.Count);
    //        rt.offsetMax = new Vector2(0, 0);
    //        rt.offsetMin = new Vector2(0, 0);
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        //tm += Time.deltaTime;
        //if(tm >= 2.0f)
        //{
        //    tm -= 2.0f;
        //    addPlayer(cards.Count + 1);
        //}
        for (int i = 1; i <= 11; i++)
        {
            //string[] names = Input.GetJoystickNames();
            //for(int j = 0; j < names.Length; j++)
            //{
            //    Debug.Log("Jostick " + j + ": " + names[j]);
            //}
            if (Input.GetAxis("A_" + i) != 0)
            {
                Debug.Log("A_" + i + " " + Input.GetAxis("A_" + i));
                if(players[i].selectCard == null)
                {
                    Debug.Log("Assigning Card for " + i);
                    for (int j = 0; j < cards.Count; j++)
                    {
                        SelectCard card = cards[j].GetComponent<SelectCard>();
                        if(card == null)
                        {
                            Debug.Log("No card select on " + j);
                        }
                        else if (card.owner == null)
                        {
                            Debug.Log("Card " + j + " to " + players[i].joyStick);
                            card.owner = players[i];
                            players[i].selectCard = card;
                            break;
                        }else
                        {
                            Debug.Log(card.owner.joyStick);
                        }
                    }
                }
                
            }

        }
    }
}

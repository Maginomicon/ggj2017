using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{

    public GameObject playerCard;
    public GameObject playerSelectArea;

    public List<GameObject> cards = new List<GameObject>();
    public PlayerAssigner playerAssigner;
    public Player[] players;

    // Use this for initialization
    void Start()
    {
       
        players = playerAssigner.players;
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
       
        
    }
}

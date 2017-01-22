using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour {

    public Player owner;
    public int player;

    RectTransform fillBar;

    // Use this for initialization
    void Start () {
        fillBar = gameObject.transform.FindChild("FillBar").GetComponent<RectTransform>();

    }
	
    public void drawFill(float percent)
    {
        fillBar.anchorMin = new Vector2(percent, 0);
    }

	// Update is called once per frame
	void Update () {
        
	}
}

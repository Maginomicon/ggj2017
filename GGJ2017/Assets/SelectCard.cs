using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour {

    public Player owner;

    RectTransform fillBar;

    float elapsedTime = 0f;
    // Use this for initialization
    void Start () {
        fillBar = gameObject.transform.FindChild("FillBar").GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {
		if(owner != null && Input.GetAxis("A_" + owner.joyStick) != 0)
        {
            elapsedTime += Time.deltaTime;
            fillBar.anchorMin = new Vector2(Mathf.Clamp01(elapsedTime), 0);

        }
        else
        {
            if(elapsedTime >= 1)
            {
                
            }else if (elapsedTime > 0 && elapsedTime < 1)
            {
                fillBar.anchorMin = Vector2.zero;
                owner.selectCard = null;
                owner = null;
                elapsedTime = 0f;
            }
        } 
        
	}
}

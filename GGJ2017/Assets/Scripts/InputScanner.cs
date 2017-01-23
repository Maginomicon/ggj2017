using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScanner : MonoBehaviour {

    Text t;
	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = "";
		for(int i = 1; i < 12; i++)
        {
            if(Input.GetAxis("TriggersL_" + i) != 0)
            {
                t.text += "\nJoyStick " + i + " Left Trigger";
            }
            if(Input. GetAxis("TriggersR_" + i) != 0)
            {
                t.text += "\nJoyStick " + i + " Right Trigger";
            }
        }
	}
}

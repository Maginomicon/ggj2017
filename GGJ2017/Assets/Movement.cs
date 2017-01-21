using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 1.0f;
    public string playerNum;

    private Rigidbody2D rg2D;
    // Use this for initialization
    void Start () {
        rg2D = GetComponent<Rigidbody2D>();
        transform.Rotate(Vector3.forward);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float move_h = Input.GetAxis(playerNum + "Horizontal");
        float move_v = Input.GetAxis(playerNum + "Vertical");

        if(Input.GetAxis(playerNum + "Vert_Right") != 0 || Input.GetAxis(playerNum + "Horiz_Right") != 0)
        {
            float angle = Mathf.Atan2(Input.GetAxis(playerNum + "Vert_Right"), Input.GetAxis(playerNum + "Horiz_Right")) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10.0f);
        }


        rg2D.velocity = new Vector2(Mathf.Lerp(0, move_h*speed, 0.8f),
            Mathf.Lerp(0, move_v * speed, 0.8f));
        
    }
}

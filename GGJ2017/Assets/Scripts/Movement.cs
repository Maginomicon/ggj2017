using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 1.0f;

    //Controller
    public int playerNum = 1;

    private Rigidbody2D rg2D;
    // Use this for initialization
    void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
        transform.Rotate(Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move_h = Input.GetAxis("L_XAxis_" + playerNum);
        float move_v = Input.GetAxis("L_YAxis_" + playerNum);
        if (Input.GetAxis("R_YAxis_" + playerNum) != 0 ||
            Input.GetAxis("R_XAxis_" + playerNum) != 0)
        {
            float angle = Mathf.Atan2(Input.GetAxis("R_YAxis_" + playerNum),
                Input.GetAxis("R_XAxis_" + playerNum)) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward), 10.0f);
        }


        rg2D.velocity = new Vector2(Mathf.Lerp(0, move_h * speed, 0.8f),
            Mathf.Lerp(0, move_v * speed, 0.8f));

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 1.0f;
    public float maxSpeed = 20f;
    public float fire_cooldown_time = 2f;

    public GameObject wave_locater_obj;

    //Controller
    public int playerNum = 1;

    private Rigidbody2D rg2D;
    private float time_last_fired_;
    private GameObject spawn_point_;

    // Use this for initialization
    void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
       // transform.Rotate(Vector3.forward);
        time_last_fired_ = 0f;

        // Find spawnpoint
        Component[] children = GetComponentsInChildren<Transform>();
        foreach(Component child in children)
        {
            if (child.gameObject.CompareTag("SpawnPoint"))
            {
                spawn_point_ = child.gameObject;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Inputs from left stick
        float move_h = Input.GetAxis("L_XAxis_" + playerNum);
        float move_v = Input.GetAxis("L_YAxis_" + playerNum);

        //Inputs from right stick
        float rot_x = Input.GetAxis("R_XAxis_" + playerNum);
        float rot_y = Input.GetAxis("R_YAxis_" + playerNum);

        bool trigger_pulled = Input.GetAxis("TriggersR_1") > 0;

        if (trigger_pulled && Time.time >= time_last_fired_ + fire_cooldown_time)
        {
            time_last_fired_ = Time.time;
            GameObject wave = Instantiate(wave_locater_obj);

            //Debug.Log("SpawnPoint at " + spawn_point_.transform.position);
            WaveMovement wave_mv_script = wave.GetComponent<WaveMovement>();
            wave_mv_script.SetPosition(spawn_point_.transform.position);
            wave_mv_script.SetEulerAngles(spawn_point_.transform.eulerAngles);
            wave_mv_script.SetSpawnerName(gameObject.name);
        }

        //Rotation
        if (rot_x!= 0 ||
            rot_y != 0)
        {
            float angle = Mathf.Atan2(rot_y,
                rot_x) * Mathf.Rad2Deg -90f;

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward), 180f);
        }

        //Movement
        if(move_h != 0 ||
            move_v != 0)
        {
            //Physics movement
            
            rg2D.AddForce(new Vector2(move_h * speed, move_v * speed));
            if(rg2D.velocity.magnitude >= maxSpeed)
            {
                rg2D.velocity = rg2D.velocity.normalized * maxSpeed;
            }

            //Direct control Movement
            //rg2D.velocity = new Vector2(Mathf.Lerp(0, move_h * speed, 0.8f),
            //Mathf.Lerp(0, move_v * speed, 0.8f));
        }else
        {
            //rg2D.velocity = rg2D.velocity.normalized* speed;
        }
        

    }
}

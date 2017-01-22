using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {
    public float speed = 5f;
    public GameObject segment_object;
    public int segment_obj_degrees = 5;
    public int start_arc_length = 30;

    private Vector3 direction_;
    List<GameObject> segments_ = new List<GameObject>();

    // Use this for initialization
    void Start () {
        direction_ = new Vector3(1.0f, 1.0f, 0f);

        if(segment_object == null)
        {
            Debug.Log("Warning: no hider object found");
        }

       // segment_object.transform.transform.Rotate(new Vector3(0f, 0f, 180- start_arc_length));
        for (int i=0; i < 360; i += segment_obj_degrees)
        {
            GameObject new_segment_obj = Instantiate(segment_object);
            new_segment_obj.transform.Rotate(new Vector3(0f, 0f, start_arc_length/2 + i));
            new_segment_obj.transform.localScale = gameObject.transform.localScale;
            new_segment_obj.transform.parent = this.gameObject.transform;

            segments_.Add(new_segment_obj);
        }

        for (int i = 0; i < (360-start_arc_length)/segment_obj_degrees; i++)
        {
            DisableSegment(i);
        }
	}

    void DisableSegment(int idx)
    {
        segments_[idx].active = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newpos = transform.position + direction_ * speed * Time.deltaTime;
        //transform.position = newpos;
        transform.localScale = transform.localScale * 1.01f;
	}
}

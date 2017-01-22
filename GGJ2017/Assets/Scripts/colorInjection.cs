using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorInjection : MonoBehaviour {
    public float bright_seconds = 0.5f;
    public float fade_seconds = 0.5f;

    private float lights_time_out_;
    private SpriteRenderer renderer_;
	// Use this for initialization
	void Start () {
        renderer_ = transform.parent.GetComponentInChildren<SpriteRenderer>();
        if(renderer_ == null)
        {
            Debug.Log("No SpriteRenderer found!");
        }
        renderer_.enabled = false;
    }
	
	void FixedUpdate () {
	    if (Time.time > lights_time_out_)
        {
            Color col = renderer_.color;
            col.a = Mathf.Lerp(1f, 0f, (Time.time - lights_time_out_) / fade_seconds);
            renderer_.color = col;
        }	
	}

    public void setColorForTime(Color col)
    {
        Color col_opaque = col;
        col_opaque.a = 1f;
        renderer_.color = col_opaque;
        renderer_.enabled = true;

        lights_time_out_ = Time.time + bright_seconds;
    }
}

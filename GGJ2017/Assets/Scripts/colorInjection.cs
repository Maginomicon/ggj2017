using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorInjection : MonoBehaviour {
    public float bright_seconds = 0.5f;
    public float fade_seconds = 0.5f;

    private float lights_time_out_;
    private SpriteRenderer renderer_;
    private Color enforced_default_color_;

    private void Awake()
    {
        // XXX: Note -- this implies that we don't allow black colored objects
        // Color cannot be set to null, that's why we need another invalid value;
        enforced_default_color_ = Color.black;
    }
    
    public void SetEnforcedColor(Color col)
    {
        enforced_default_color_ = col;
    }
    
	// Use this for initialization
	void Start () {
        // Some objects have the renderer as a sibling
        renderer_ = gameObject.GetComponentInChildren<SpriteRenderer>();
        
        // Others have it as a direct child
        if(renderer_ == null)
        {
            renderer_ = transform.parent.GetComponentInChildren<SpriteRenderer>();
        }

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
        Color eventual_col = enforced_default_color_;

        if (eventual_col == null)
        {
            eventual_col = col;
        }

        eventual_col.a = 1f;
        renderer_.color = eventual_col;
        renderer_.enabled = true;

        lights_time_out_ = Time.time + bright_seconds;
    }
}

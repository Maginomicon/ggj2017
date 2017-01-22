using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCollision : MonoBehaviour {
    public float lifetime = 3f;

    Rigidbody2D rb_;
    private float destructionTimer_;

    private WaveMovement wave_mov_script_;

    // Use this for initialization
    void Awake () {
        rb_ = GetComponent<Rigidbody2D>();
        destructionTimer_ = Time.time + lifetime;

        wave_mov_script_ = gameObject.GetComponentInParent<WaveMovement>();
	}

    private void FixedUpdate()
    {
        if(Time.time > destructionTimer_)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("WaveSegment"))
        {
            return;
        }

        Destroy(gameObject);
                
        colorInjection color_script = collision.transform.parent.gameObject.GetComponentInChildren<colorInjection>();
        if (color_script != null)
        {
            color_script.setColorForTime(Color.red);
        }
    }

    // Are we hitting the spaceship that spawned the Wave?
    // Does not work: the name of wave_mov_script stays unset...
    bool myOwnDaddy(GameObject obj)
    {

        if (wave_mov_script_ == null)
        {
            wave_mov_script_ = gameObject.GetComponentInParent<WaveMovement>();
        }

        if(wave_mov_script_ != null)
        {
            Debug.Log("Comparing " + wave_mov_script_.GetSpawnerName() + " with " + obj.name);
            return (wave_mov_script_.GetSpawnerName() == obj.name);
        }
        else
        {
            Debug.Log("No movement script");
        }

        return false;
    }
}

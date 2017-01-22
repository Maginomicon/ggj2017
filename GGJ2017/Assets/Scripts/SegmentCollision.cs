using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCollision : MonoBehaviour {
    public float lifetime = 3f;

    Rigidbody2D rb_;
    private float destructionTimer_;

    private WaveMovement wave_mov_script_;

    // Use this for initialization
    void Start () {
        rb_ = GetComponent<Rigidbody2D>();
        destructionTimer_ = Time.time + lifetime;

        GameObject parent = gameObject.transform.parent.gameObject;
        if (parent != null)
        {
            wave_mov_script_ = parent.GetComponentInChildren<WaveMovement>();
        }
  
        if(wave_mov_script_ == null)
        {
            Debug.Log("Parent  WaveMovement script not found.");
        }
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

        attemptLightUp(collision.gameObject);

        if(wave_mov_script_ == null)
        {
            Debug.Log("Warning: I have not WaveMove Script reference");
            return;
        }

        // If this is a destructive wave, the colliding object is a player that was not hit before, damage it!
        bool destructive = wave_mov_script_.IsDestructive();
        bool player_tag = collision.gameObject.CompareTag("Player");
        bool not_yet_hit = !wave_mov_script_.wasObjectAlreadyHit(collision.gameObject.GetInstanceID());
        if (destructive && player_tag && not_yet_hit)
        {
            Debug.Log("I am destroying " + collision.gameObject);
            Movement player = collision.gameObject.GetComponent<Movement>();
            player.takeDamage(1);
            wave_mov_script_.setObjectHit(collision.gameObject.GetInstanceID());

        }

        Destroy(gameObject, 0.2f);
    }

    void attemptLightUp(GameObject colider_obj)
    {
        GameObject go = null;
        if (colider_obj.transform.parent != null)
        {
            go = colider_obj.transform.parent.gameObject;
        }
        colorInjection color_script = null;

        if (go != null)
        {
            color_script = colider_obj.transform.parent.gameObject.GetComponentInChildren<colorInjection>();
        }

        if (color_script != null)
        {
            color_script.setColorForTime(wave_mov_script_.GetColor());
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

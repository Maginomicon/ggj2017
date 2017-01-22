using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {
    public GameObject segment_object;
    public int segment_obj_degrees = 5;
    public int start_arc_length = 30;
    public float wave_speed = 10f;

    private string spawner_name_;
    private Color color_;
    private bool destructive_;
    List<GameObject> segments_ = new List<GameObject>();
    List<int> hit_list = new List<int>();

    // Gets called right after Instantiate (not Start() !!!!!)
    private void Awake()
    {
        spawner_name_ = "unnamed";
        destructive_ = false;

        if (segment_object == null)
        {
            Debug.Log("Warning: no segment object found");
        }

        for (int i = 0; i < 360; i += segment_obj_degrees)
        {
            GameObject new_segment_obj = Instantiate(segment_object);
            new_segment_obj.transform.Rotate(new Vector3(0f, 0f, start_arc_length / 2 + i));
            new_segment_obj.transform.localScale = gameObject.transform.localScale;
            new_segment_obj.transform.parent = this.gameObject.transform;
            Rigidbody2D rb = new_segment_obj.GetComponent<Rigidbody2D>();
            rb.velocity.Set(0f, 0f);

            segments_.Add(new_segment_obj);
        }

        // Simuate an arc by destroying the unneeded part from the circle.
        for (int i = 0; i < (360 - start_arc_length) / segment_obj_degrees; i++)
        {
            DisableSegment(i);
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetEulerAngles(Vector3 eulerAngles)
    {
        transform.eulerAngles = eulerAngles;
    }

    public void SetSpawnerName(string name)
    {
        spawner_name_ = name;
    }

    public string GetSpawnerName() { return spawner_name_; }

    public void setObjectHit(int obj_id)
    {
        hit_list.Add(obj_id);
    }

    public bool wasObjectAlreadyHit(int obj_id)
    {
        return hit_list.Contains(obj_id);
    }

    public void SetColor(Color col)
    {
        color_ = col;
        foreach(GameObject segment in segments_)
        {
            if (segment != null)
            {
                SpriteRenderer renderer = segment.GetComponentInChildren<SpriteRenderer>();
                renderer.color = col;
            }
        }
    }

    public Color GetColor()
    {
        return color_;
    }

    public void SetDestructive(bool destructive)
    {
        destructive_ = destructive;
    }
    public bool IsDestructive()
    {
        return destructive_;
    }

    void DisableSegment(int idx)
    {
        Destroy(segments_[idx]);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.localScale = transform.localScale * (1f + wave_speed);
        if (checkAllSegmentsDead())
        {
            Destroy(gameObject);
        }
	}

    bool checkAllSegmentsDead()
    {
        foreach(GameObject segment in segments_)
        {
            if(segment != null)
            {
                return false;
            }
        }
        return true;
    }
}

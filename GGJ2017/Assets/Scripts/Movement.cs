using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 1.0f;
    public float maxSpeed = 20f;
    public float locater_wave_cooldown_time = 2f;
    public float shot_wave_cooldown_time = 3f;

    public GameObject wave_locater_obj;
    public GameObject wave_shot_obj;
    public GameObject explosion;

    //Controller
    public int playerNum = 1;

    private Rigidbody2D rg2D;
    private float time_last_locater_wave_;
    private float time_last_shot_wave_;
    private GameObject spawn_point_;
    private PlayerManager player_manager_;

    //Audio
    public AudioSource PlayerShipExplodeLayer01;
    public AudioSource PlayerShipExplodeLayer02;
    public AudioSource DamageWaveShoot;
    public AudioSource LocateWaveShoot;
    public AudioSource PlayerShipDamaged;
    public AudioSource AbilityFizzle;
    public AudioSource Thrust;

    public Color my_color = Color.blue; // TBD: Should be decided on player selection screen
    Color my_location_color;
    private float location_color_darkener = 0.8f;

    private int max_health_ = 2;
    private int health = 2;
    public HealthHud healthHud;
    public Player playerClass;
    // Color injector script in case we get hit by an enemy
    private colorInjection my_color_injection_;

    private bool L_prev_pulled_;
    private bool R_prev_pulled_;

    public void RegisterPlayerManager(PlayerManager pm)
    {
        player_manager_ = pm;
    }

    // Use this for initialization
    void Start()
    {
        healthHud = GameObject.Find("Lives HUD").GetComponent<HealthHud>();

        rg2D = GetComponent<Rigidbody2D>();
        // transform.Rotate(Vector3.forward);
        time_last_locater_wave_ = 0f;
        time_last_shot_wave_ = 0f;

        my_color_injection_ = gameObject.GetComponent<colorInjection>();
        if (my_color_injection_ == null)
        {
            Debug.Log("Cannot find color injection script. I will not light up when hit by another player.");
        } else
        {
            my_color_injection_.SetEnforcedColor(my_color);
        }

        // Find spawnpoint
        Component[] children = GetComponentsInChildren<Transform>();
        foreach (Component child in children)
        {
            if (child.gameObject.CompareTag("SpawnPoint"))
            {
                spawn_point_ = child.gameObject;
            }
        }

        my_location_color = my_color;

        my_location_color.r *= location_color_darkener;
        my_location_color.g *= location_color_darkener;
        my_location_color.b *= location_color_darkener;

    }

    public void takeDamage(int power)
    {
        health -= power;

        healthHud.takeDamage(playerClass.playerNum, power);
        

        if (health <= 0)
        {
            die();
        }else
        {
            //healthHud.takeDamage(playerClass.playerNum, power);

            AudioSource ShipDamaged = Instantiate(PlayerShipDamaged);
            ShipDamaged.Play();
            Destroy(ShipDamaged, 10f);
            time_last_shot_wave_ = Time.time;
        }

        // Inform the PlayerManager that we've taken damage
        player_manager_.PlayerHitCallBack();

    }

    public int GetMaxHealth()
    {
        return max_health_;
    }

    public int GetHealth()
    {
        return health;
    }

    public Player GetPlayerClass()
    {
        return playerClass;
    }

    void die()
    {
        if(PlayerShipExplodeLayer01 == null)
        {
            Debug.Log("Warning Audio Source not found.");
        }


        AudioSource explode_1 = Instantiate(PlayerShipExplodeLayer01);
        AudioSource explode_2 = Instantiate(PlayerShipExplodeLayer02);
        explode_1.Play();
        explode_2.Play();
        Destroy(explode_1, 10f);
        Destroy(explode_2, 10f);


        //PlayerShipExplodeLayer01.Play();
        //PlayerShipExplodeLayer02.Play();
        //AudioSource.PlayClipAtPoint(PlayerShipExplodeLayer01.clip, new Vector3(0f, 0f, 0f));
        //AudioSource.PlayClipAtPoint(PlayerShipExplodeLayer02.clip, new Vector3(0f, 0f, 0f));
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.enabled = false;   
        this.enabled = false;
        Destroy(gameObject,2f);

        Instantiate<GameObject>(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }

    void SpawnWave(Color col, GameObject wave_obj, bool destructive)
    {
        GameObject wave = Instantiate(wave_obj);

        WaveMovement wave_mv_script = wave.GetComponent<WaveMovement>();
        wave_mv_script.SetPosition(spawn_point_.transform.position);
        wave_mv_script.SetEulerAngles(spawn_point_.transform.eulerAngles);
        wave_mv_script.SetSpawnerName(gameObject.name);
        wave_mv_script.SetColor(col);
        wave_mv_script.SetDestructive(destructive);
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

        bool R_trigger_pulled = Input.GetAxis("TriggersR_" + playerNum) != 0;
        bool L_trigger_pulled = Input.GetAxis("TriggersL_" + playerNum) != 0;

        bool L_constantly_pulled = L_trigger_pulled && L_prev_pulled_;
        bool R_constantly_pulled = R_trigger_pulled && R_prev_pulled_;

        L_prev_pulled_ = L_trigger_pulled;
        R_prev_pulled_ = R_trigger_pulled;

        if (L_constantly_pulled && Time.time >= time_last_locater_wave_ + locater_wave_cooldown_time)
        {
            time_last_locater_wave_ = Time.time;

            AudioSource LocateShoot1 = Instantiate(LocateWaveShoot);
            LocateShoot1.Play();
            Destroy(LocateShoot1, 10f);

            SpawnWave(my_location_color, wave_locater_obj, false);
        }


        if (!L_constantly_pulled && L_trigger_pulled && Time.time < time_last_locater_wave_ + locater_wave_cooldown_time)
        {

            AudioSource Fizzle = Instantiate(AbilityFizzle);
            Fizzle.Play();
            Destroy(Fizzle, 10f);
        }


        if (R_constantly_pulled && Time.time >= time_last_shot_wave_ + shot_wave_cooldown_time)
        {
            AudioSource DamageShoot1 = Instantiate(DamageWaveShoot);
            DamageShoot1.Play();
            Destroy(DamageShoot1, 10f);
            time_last_shot_wave_ = Time.time;

            SpawnWave(my_color, wave_shot_obj, true);
}
        if (!R_constantly_pulled && R_trigger_pulled && Time.time < time_last_shot_wave_ + shot_wave_cooldown_time)
        {
            AudioSource Fizzle = Instantiate(AbilityFizzle);
            Fizzle.Play();
            Destroy(Fizzle, 10f);
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

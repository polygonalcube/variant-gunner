using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanShield : MonoBehaviour
{
    public ShootComponent gun;
    public HealthComponent hp;
    public MoveComponent mov;

    int state; //0=enter, 1=dying, 2=attacking, 3=positioning
    public float entranceSpeed;
    public Vector3 finalPos; //0,0,0
    public Animator anim;
    public float timer;
    public float timerSet;
    public float bulletCount;

    public Transform player;

    public Collider[] shields;

    void Start()
    {
        state = 0;
        GameObject goPlayer = GameObject.Find("Player");
        if (goPlayer != null)
        {
            player = GameObject.Find("Player").transform;
        }
    }
    
    void Update()
    {
        if (state == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, entranceSpeed * Time.deltaTime);
            if (transform.position == finalPos)
            {
                timer = timerSet;
                state = 3;
            }
        }

        if (state == 2)
        {
            anim.Play("attack");
            timer -= Time.deltaTime;
            foreach (Collider shield in shields)
            {
                shield.enabled = false;
            }
            if (gun.shotTimer <= 0)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    gun.shotAngle = 360f/bulletCount*(float)i;
                    gun.ShootAng(gun.bulletSpeedAng);
                    gun.shotTimer = 0f;
                }
                gun.shotTimer = gun.shotTimerSet;
                timer = timerSet;
            }
            if (timer <= 0)
            {
                timer = timerSet;
                state = 3;
            }
            if (hp.health <= 0)
            {
                state = 1;
                timer = timerSet;
            }
        }

        if (state == 3)
        {
            anim.Play("defend");
            timer -= Time.deltaTime;
            foreach (Collider shield in shields)
            {
                shield.enabled = true;
            }
            if (player != null)
            {
                if (player.position.x < transform.position.x)
                {
                    mov.xSpeed = mov.Accelerate(mov.xSpeed, true);
                }
                else if (player.position.x > transform.position.x)
                {
                    mov.xSpeed = mov.Accelerate(mov.xSpeed, false);
                }
                mov.xSpeed = mov.Cap(mov.xSpeed);
                mov.Move();
            }
            if (timer <= 0)
            {
                timer = timerSet;
                state = 2;
            }
            if (hp.health <= 0)
            {
                state = 1;
                timer = timerSet;
            }
        }

        if (state == 1)
        {
            timer -= Time.deltaTime;
            transform.eulerAngles += new Vector3(8f, 8f, 8f) * Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

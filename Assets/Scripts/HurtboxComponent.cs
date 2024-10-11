using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxComponent : MonoBehaviour
{
    public HealthComponent hp;
    public ScoreComponent score;
    public LayerMask layers;

    public int damageOverride;
    public bool immediateDeath = true;

    public bool hasIFrames = false;
    public float iSeconds = 0f;
    public float iSecondsSet;
    public GameObject mr;

    void Update()
    {
        if (hasIFrames)
        {
            iSeconds -= Time.deltaTime;
            if (iSeconds > 0f)
            {
                if (mr.activeSelf)
                {
                    mr.SetActive(false);
                }
                else
                {
                    mr.SetActive(true);
                }
            }
            else
            {
                mr.SetActive(true);
            }
        }
        else
        {
            if (mr != null)
            {
                if (!mr.activeSelf)
                {
                    StartCoroutine(BecomeVisible());
                }
            }
        }
    }

    IEnumerator BecomeVisible()
    {
        yield return new WaitForSeconds(1f/30f);
        mr.SetActive(true);
    }
    
    //Needs a trigger collider to be present on the game object.
    void OnTriggerEnter(Collider col)
    {
        if((layers.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer)
        {
            //DamageComponent hitbox = col.gameObject.TryGetComponent<DamageComponent>(out DamageComponent hitbox);
            if (hp != null)
            {
                if (hasIFrames && iSeconds > 0f)
                {
                    //Do nothing
                }
                else if (damageOverride != 0)
                {
                    hp.health -= damageOverride;
                    if (hasIFrames)
                    {
                        iSeconds = iSecondsSet;
                    }
                }
                else if (col.gameObject.TryGetComponent<DamageComponent>(out DamageComponent hitbox))
                {
                    hp.health -= hitbox.damage;
                    if (hasIFrames)
                    {
                        iSeconds = iSecondsSet;
                    }
                }

                if (score != null)
                {
                    GameManager.gm.score += score.damagePoints;
                }
                /*
                if (col.gameObject.tag != "Player")
                {
                    Destroy(col.gameObject);
                }
                */
                if (hp.health <= 0)
                {
                    if (score != null)
                    {
                        GameManager.gm.score += score.deathPoints;
                    }
                    if (immediateDeath)
                        Destroy(this.gameObject);
                }

                if (mr != null && !hasIFrames)
                {
                    mr.SetActive(false);
                }
            }
        }
    }
}

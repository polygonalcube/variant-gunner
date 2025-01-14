using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTwoBackground : MonoBehaviour
{
    public float scrollSpd;
    public bool newSectionSpawned = false;
    
    void Update()
    {
        transform.position += new Vector3(0f, scrollSpd * Time.deltaTime, 0f);
        if (!newSectionSpawned && transform.position.y > 0f)
        {
            Instantiate(this.gameObject, new Vector3(0f, -40f + transform.position.y, 0f), Quaternion.identity);
            newSectionSpawned = true;
        }
        if (transform.position.y > 33f)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public ShootComponent vulcan;
    
    void Update()
    {
        vulcan.Shoot(new Vector2(vulcan.bulletSpeed.x, vulcan.bulletSpeed.y));
    }
}

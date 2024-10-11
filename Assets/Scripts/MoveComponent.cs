using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public Vector3 moveDir;
    public float xSpeed;
    public float ySpeed;
    public float maxSpeed;
    public float accel;
    public float decel;

    public float Accelerate(float speedVar, bool isNegative)
    {
        if (isNegative)
            return speedVar - accel;
        else
            return speedVar + accel;
    }

    public float Decelerate(float speedVar, bool isNegative)
    {
        if (isNegative)
            return speedVar - accel;
        else
            return speedVar + accel;
    }

    public float CheckNearZero(float speedVar)
    {
        if(Mathf.Abs(speedVar) < 0.01)
        {
            return 0;
        }
        else
        {
            return speedVar;
        }
    }

    public float Cap(float speedVar)
    {
        if(speedVar > maxSpeed)
        {
            return maxSpeed;
        }
        else if(speedVar < -maxSpeed)
        {
            return -maxSpeed;
        }
        else
        {
            return speedVar;
        }
    }

    public void BoundXY(float xBound, float yBound)
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
        else if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
    }

    public void ResetZ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public void Move()
    {
        moveDir = new Vector3(xSpeed, ySpeed, 0f);
        transform.position += moveDir * Time.deltaTime;
    }

    public void MoveAng(Vector3 dir)
    {
        transform.position += transform.TransformDirection(dir) * maxSpeed * Time.deltaTime;
    }
}

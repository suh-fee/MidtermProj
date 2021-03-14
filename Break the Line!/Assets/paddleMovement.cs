using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleMovement : MonoBehaviour
{
    bool up = false;
    bool down = false;
    public Vector3 acc = new Vector3(0f,0f,5f);
    Vector3 negativeAcc;
    public Vector3 speed;
    public float max = 5f;

    // Start is called before the first frame update
    void Start()
    {
        negativeAcc = -1 * acc;
        speed = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            up = true;
            
            
        } else
        {
            up = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            down = true;
            
        }
        else
        {
            down = false;
        }

        Movement();
    }

    void Movement()
    {
        Debug.Log(speed);
        if (up)
        {
            speed += acc;
        } else if (down)
        {
            speed += negativeAcc;
        } else
        {
            speed = new Vector3(0f, 0f, 0f);
        }

        if (speed.z >= max){
            speed.z = max;
        } else if (speed.z <= max * -1)
        {
            speed.z = max * -1;
        }

        transform.Translate(speed * Time.deltaTime);
    }
}

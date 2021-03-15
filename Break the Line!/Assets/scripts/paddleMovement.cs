using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleMovement : MonoBehaviour
{
    bool up = false;
    bool down = false;
    Vector3 acc = new Vector3(0f,0f,2f);
    Vector3 negativeAcc;
    Vector3 speed;
    float max = 20f; //maximum speed of paddle
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        negativeAcc = -1 * acc;
        speed = new Vector3(0f, 0f, 0f);
        rb = GetComponent<Rigidbody>();
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
        rb.velocity = new Vector3(0f, 0f, 0f); //ensures the ball won't cause movement in the paddle

        //again, has to be a neater way to do this
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
        
        //standard collisions broke the paddle, so i hardcoded the z-axis boundaries
        while(transform.position.z > 11){
            transform.Translate(new Vector3(0f, 0f, -.1f) * Time.deltaTime);
        }

        while (transform.position.z < -11)
        {
            transform.Translate(new Vector3(0f, 0f, .1f) * Time.deltaTime);
        }
    }
}

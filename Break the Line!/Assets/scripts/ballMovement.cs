using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
    //general variables
    public GameObject paddle;
    bool start = false;

    //used for collision detection
    public Rigidbody rb;
    private Vector3 lastFrameVelocity; 
    private float minVelocity = 20f;

    //needed to decrement HP
    public GameObject scoreManager;
    scoreManager sm;

    //variables needed for keeping collision more stable, tuned to allow for cooldown to not interfere with regular bouncing
    float cooldown = 0f;
    float cooldownMax = .1f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sm = scoreManager.GetComponent<scoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

        cooldown += 1 * Time.deltaTime;

        if (!start)  //start is an inaccurate name, as this state can be entered upon manual reset
        {
            this.transform.position = paddle.transform.position + new Vector3(2f, 0f, 0f);

            if (Input.GetKeyDown("space"))
            {
                start = true;
                rb.AddForce(20f, 0f, 20f, ForceMode.Impulse);
            }
        } else
        {
            lastFrameVelocity = rb.velocity; //must keep track for bouncing!
        }

        checkPos(); //needed function :(


    }

    //cannot figure out why my collision keeps breaking, so added this function to ensure game is playable if ball breaks out of bounds
    //also allows for manual reset if ball get stuck in any position
    private void checkPos()
    {
        Vector3 pos = transform.position;
        bool reset = false;

        if(pos.x > 40 | pos.x < -40)
        {
            reset = true;
        } else if(pos.z > 40 | pos.z < -40)
        {
            reset = true;
        } else
        {
            reset = false;
        }

        if (Input.GetKey(KeyCode.R))
        {
            reset = true;
        }

        if (reset)
        {
            start = false;
            rb.velocity = new Vector3(0f, 0f, 0f); //makes sure ball launches are consistent!
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            Death();
        } else
        {
            if (cooldown >= cooldownMax) //ensures bouncing doesn't happen if collision happens frame after bounce
            {
                Bounce(collision.contacts[0].normal); 
            }
        }
        
    }

    //uses the normal vector from collision data to reflect the ball
    //i intially used the built in physics engine w/ unity. it did NOT work as expected, so I 'faked' the physics instead
    private void Bounce(Vector3 collisionNormal)
    {
        cooldown = 0; //reset the cooldown

        var speed = lastFrameVelocity.magnitude; //keeps speed the same
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal); //how to get more accurate reflection? paddle bounces aren't entirely accurate
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

    //decrements health and ensures next ball launch is consistent
    private void Death()
    {
        start = false;
        rb.velocity = new Vector3(0f, 0f, 0f);
        sm.decHP();
    }

}

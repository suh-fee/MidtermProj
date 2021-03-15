using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{

    public GameObject paddle;
    bool start = false;
    public Rigidbody rb;
    private Vector3 lastFrameVelocity;
    private float minVelocity = 20f;

    public GameObject scoreManager;
    scoreManager sm;
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
        if (!start)
        {
            this.transform.position = paddle.transform.position + new Vector3(2f, 0f, 0f);

            if (Input.GetKeyDown("space"))
            {
                start = true;
                rb.AddForce(20f, 0f, 20f, ForceMode.Impulse);
            }
        } else
        {
            lastFrameVelocity = rb.velocity;
            
        }

        checkPos();


    }

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
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            Death();
        } else
        {
            if (cooldown >= cooldownMax)
            {
                Bounce(collision.contacts[0].normal);
            }
        }
        
    }

    private void Bounce(Vector3 collisionNormal)
    {
        cooldown = 0;

        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

    private void Death()
    {
        start = false;
        rb.velocity = new Vector3(0f, 0f, 0f);
        sm.decHP();
    }

}

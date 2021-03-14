using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{

    public GameObject paddle;
    bool start = false;
    public Rigidbody rb;
    private Vector3 lastFrameVelocity;
    private float minVelocity = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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


    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    //different enemy types have different HP values
    public int maxHealth; //also changes how score is incremented
    public int currHealth;

    //involved in changing enemy color depending on hp
    Color color;
    private Renderer rn;

    //used for case switching between linear and sin movement
    public int movementType;
    Vector3 linearMovement;

    // used for sin movement
    int timeScale = 4;
    float amplitude = 5;

    //management variables
    public GameObject scoreManager;
    scoreManager sm;
    GameObject[] obj; 




    // start is never called, but kept here for debugging purposes
    void Start()
    {
        rn = GetComponent<Renderer>();
        sm = scoreManager.GetComponent<scoreManager>();
        linearMovement = new Vector3(-.5f, 0f, 0f);
    }

    //called whenever an enemy is instantiated
    void Awake()
    {
        //initialize all needed variables for a given enemy
        obj = GameObject.FindGameObjectsWithTag("GameController");
        scoreManager = obj[0];

        rn = GetComponent<Renderer>();
        sm = scoreManager.GetComponent<scoreManager>();
        linearMovement = new Vector3(-.5f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        //cleaned up, added to functions
        updateColor();
        updatePos();
        checkPos();

        if (currHealth <= 0)
        {
            death();
        }
    }

    private void death()
    {
        sm.incScore(maxHealth * 5);
        Destroy(gameObject);
    }

    private void updatePos()
    {
        switch (movementType)
        {
            case 1: //linear movement towards goal
                transform.Translate(linearMovement * Time.deltaTime);
                break;
            case 2: //sin movement along the z axis, linear movement towards enemy goal
                transform.Translate(new Vector3(-.5f * Time.deltaTime, 0f, Mathf.Sin(Time.time * timeScale) * amplitude* Time.deltaTime));
                break;
            default:
                Debug.Log("Enemy Case broken.");
                break;
        }
    }

    //when an enemy hits the paddle line, dec the HP as needed
    private void checkPos()
    {
        if(transform.position.x <= -20)
        {
            sm.decHP();
            Destroy(gameObject);
        }
    }


    private void updateColor()
    {
        //there HAS to be a neater way to do this...
        if (currHealth == 4)
        {
            color = Color.blue;
        }
        else if (currHealth == 3)
        {
            color = Color.green;
        }
        else if (currHealth == 2)
        {
            color = Color.yellow;
        }
        else
        {
            color = Color.red;
        }

        rn.material.SetColor("_Color", color);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball") //ensures paddle/wall don't decrement HP
        {
            currHealth -= 1;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public Vector3 tarPos;
    public int maxHealth;
    public int currHealth;
    public Color color;
    private Renderer rn;
    public int movementType;
    public Vector3 linearMovement;
    public GameObject scoreManager;
    public scoreManager sm;

    // used for sin movement
    int timeScale = 4;
    float amplitude = 5;

    GameObject[] obj;




    // Start is called before the first frame update
    void Start()
    {
        rn = GetComponent<Renderer>();
        sm = scoreManager.GetComponent<scoreManager>();
        linearMovement = new Vector3(-.5f, 0f, 0f);
    }

    void Awake()
    {
        obj = GameObject.FindGameObjectsWithTag("GameController");
        scoreManager = obj[0];


        rn = GetComponent<Renderer>();
        sm = scoreManager.GetComponent<scoreManager>();
        linearMovement = new Vector3(-.5f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

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
            case 2: //sin movement towards goal along z axis
                transform.Translate(new Vector3(-.5f * Time.deltaTime, 0f, Mathf.Sin(Time.time * timeScale) * amplitude* Time.deltaTime));
                break;
            default:
                Debug.Log("Enemy Case broken.");
                break;
        }
    }

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
        if (collision.gameObject.tag == "Ball")
        {
            currHealth -= 1;
        }

    }
}

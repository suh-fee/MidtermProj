using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    //could've made these public, wanted to try out serialized variables
    [SerializeField]
    GameObject linMed;
    [SerializeField]
    GameObject sinMed;
    [SerializeField]
    GameObject linEasy;
    [SerializeField]
    GameObject sinEasy;

    GameObject[] enemies;

    public bool start; //allows for a 'break' state between rounds
    float timer = 0;
    float maxTime = 10;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            timer += 1 * Time.deltaTime;

            if ((enemies.Length == 0) | (timer > maxTime)) //spawns new enemies on the timer or if there are none on screen
            {
                timer = 0;
                spawnEnemies();
            }
        } else
        {
            if (Input.GetKeyDown("space"))
            {
                start = true;
            }
        }
        

    }
    
    void spawnEnemies()
    {
        GameObject next;

        //is there a better way to do random generation? i'm just using a range and random to simulate percentage chance
        float rand = Random.Range(0f, 100f);
        if(rand <= 15) //least likely (15%)
        {
            next = sinMed;
        } else if(rand <= 35) //(20%)
        {
            next = linMed;
        } else if(rand <= 60) //(25%)
        {
            next = sinEasy;
        } else //most likely (40%)
        {
            next = linEasy;
        }

        if(maxTime >= 5) // slowly makes gameplay more stressful. is there even a tangible effect from this???
        {
            maxTime -= .25f;
        }

        Instantiate(next, new Vector3(20f, 0.25f, 0f), transform.rotation);
    }

    public void clearEnemies()
    {

        //used during round loss
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        start = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject linMed;
    [SerializeField]
    GameObject sinMed;
    [SerializeField]
    GameObject linEasy;
    [SerializeField]
    GameObject sinEasy;

    GameObject[] enemies;

    public bool start;
    float timer = 0;
    float maxTime = 10;

    scoreManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<scoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            timer += 1 * Time.deltaTime;

            if ((enemies.Length == 0) | (timer > maxTime))
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
        float rand = Random.Range(0f, 100f);
        if(rand <= 15)
        {
            next = sinMed;
        } else if(rand <= 35)
        {
            next = linMed;
        } else if(rand <= 60)
        {
            next = sinEasy;
        } else
        {
            next = linEasy;
        }

        if(maxTime >= 5)
        {
            maxTime -= .5f;
        }

        Instantiate(next, new Vector3(20f, 0.25f, 0f), transform.rotation);
    }

    public void clearEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        start = false;
    }
}

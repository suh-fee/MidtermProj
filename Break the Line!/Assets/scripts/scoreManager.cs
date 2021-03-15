using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    float score; //overall score of the current round
    int health = 5;
    
    GUIStyle style = new GUIStyle(); 

    List<float> scores = new List<float>(); //needed for sorting all scores and keeping track between rounds
    string scoreString = "Scores: ";

    //housekeeping variables
    enemyManager em;
    bool start = false; //used to allow breaks, similar to enemy manager

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        style.fontSize = 25;
        em = GetComponent<enemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            incScore(1 * Time.deltaTime); //passive point generation during a round, pause if not
        } else
        {
            if (Input.GetKeyDown("space"))
            {
                start = true;
            }
        }
    }

    public void startGame()
    {
        start = true;
    }

    public void incScore(float value) //public, called in enemyBehavior > Death()
    {
        score += value;
    }

    public void decHP() //called in enemyBehavior and ballMovement
    {
        health -= 1;
        if(health <= 0)
        {
            reset();
        }
    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Score: " + score.ToString("#.") + "   Health: " + health.ToString(), style);

        GUI.Label(new Rect(Screen.width - 200, 0, Screen.width, Screen.height), scoreString, style);
    }

    //reset the game state between round losses
    //also keeps track of all scores + sorts them!
    private void reset()
    {

        start = false;
        em.clearEnemies();
        scores.Add(score);
        quickSort(ref scores, 0, scores.Count - 1);

        //updates the GUI to reflect latest scores
        scoreString = "Scores: \n";
        for (int i = 0; i < scores.Count; i++)
        {
            scoreString += "#" + (scores.Count-i).ToString() + ": " + scores[i].ToString("#.") + "\n";
        }

        health = 5;
        score = 0;
    }

    // same implementation from my expressive algorithm
    void quickSort(ref List<float> list, int low, int high)
    {
        if (low < high)
        {
            int part = partition(ref list, low, high);

            quickSort(ref list, low, part - 1);
            quickSort(ref list, part + 1, high);

        }
    }

    int partition(ref List<float> list, int low, int high)
    {
        float pivotVal = list[high];
        int smallerIndex = low - 1;
        float temp;

        for (int i = low; i <= high - 1; i++)
        {
            if (list[i] < pivotVal)
            {
                smallerIndex++;
                temp = list[i];
                list[i] = list[smallerIndex];
                list[smallerIndex] = temp;

            }

        }
        temp = list[smallerIndex + 1];
        list[smallerIndex + 1] = list[high];
        list[high] = temp;

        return (smallerIndex + 1);
    }
}

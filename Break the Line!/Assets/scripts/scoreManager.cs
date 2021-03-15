using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public float score;
    GUIStyle style = new GUIStyle();

    List<float> scores = new List<float>();
    string scoreString = "Scores: ";
    int health = 5;
    enemyManager em;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        style.fontSize= 25;
        em = GetComponent<enemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            incScore(1 * Time.deltaTime);
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

    public void incScore(float value)
    {

        score += value;
    }

    public void decHP()
    {
        health -= 1;

        if(health <= 0)
        {
            reset();
        }
    }

    public void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Score: " + score.ToString("#.") + "   Health: " + health.ToString(), style);

        GUI.Label(new Rect(Screen.width - 200, 0, Screen.width, Screen.height), scoreString, style);
    }

    public void reset()
    {
        start = false;
        em.clearEnemies();
        scores.Add(score);
        scoreString = "Scores: \n";
        quickSort(ref scores, 0, scores.Count - 1);

        for(int i = 0; i < scores.Count; i++)
        {
            scoreString += "#" + (scores.Count-i).ToString() + ": " + scores[i].ToString("#.") + "\n";
        }

        health = 5;
        score = 0;
    }

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

        // is tuple swapping a thing in C#?
        temp = list[smallerIndex + 1];
        list[smallerIndex + 1] = list[high];
        list[high] = temp;

        return (smallerIndex + 1);
    }
}

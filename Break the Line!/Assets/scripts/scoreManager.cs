using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public float score;
    GUIStyle style = new GUIStyle();

    int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        style.fontSize= 20;
    }

    // Update is called once per frame
    void Update()
    {
        incScore(1 * Time.deltaTime);
    }

    public void incScore(float value)
    {
        score += value;
    }

    public void decHP()
    {
        health -= 1;
    }

    public void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Score: " + score.ToString("#.") + "   Health: " + health.ToString(), style);
    }
}

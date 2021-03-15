using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    float score;
    GUIStyle style = new GUIStyle();

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

    public void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), score.ToString("#."), style);
    }
}

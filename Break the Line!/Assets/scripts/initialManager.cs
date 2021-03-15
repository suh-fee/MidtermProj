using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialManager : MonoBehaviour
{
    GUIStyle style = new GUIStyle();
    bool show = true;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown("space"))
        {
            show = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGUI()
    {
        if (show)
        {
            GUI.backgroundColor = Color.yellow;
            GUI.Label(new Rect(100, 100, Screen.width, Screen.height), "Press space to launch ball \nW and S to move paddle\nR to reset ball", style);
        }
        
    }
}

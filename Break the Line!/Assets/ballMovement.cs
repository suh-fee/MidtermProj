using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{

    public GameObject paddle;
    bool start;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            this.transform.position = paddle.transform.position + new Vector3(2f, 0f, 0f); 
        }
    }
}

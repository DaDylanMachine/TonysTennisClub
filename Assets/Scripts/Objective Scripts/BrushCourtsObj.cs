using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushCourtsObj : MonoBehaviour
{
    public int brushedCourts = 0;
    public bool taskComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!taskComplete)
        {
            if (brushedCourts == 2)
            {
                taskComplete = true;
                Debug.Log("Court Task Complete");
            }

           
        }
        
    }
}

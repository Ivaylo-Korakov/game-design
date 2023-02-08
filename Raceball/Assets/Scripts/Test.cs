using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{  
    private int _testInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World!");
        Debug.Log("TestInt = " + _testInt);
    }

    // Update is called once per frame
    void Update()
    {
        _testInt++;
        Debug.Log("TestInt = " + _testInt);        
    }
}

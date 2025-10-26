using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Debugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public XPSystem system;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("]")) {
            system.AddXPPoints(5);
        }
    }
}

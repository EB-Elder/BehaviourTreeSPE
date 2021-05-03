using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{

    public states printHi()
    {
        return states.Success;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Nodes myAction = new Nodes(printHi, baseNodeType.Action);


        if (myAction.Execute() == states.Success)
            print("SUCCES !");

    }
}

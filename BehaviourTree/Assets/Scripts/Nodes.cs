using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum states
{
    NotExecuted,
    Running,
    Failure,
    Success
}


public class Nodes
{
    //This is the actual state of the node
    private states _state = states.NotExecuted;
}

public class Repeat : Nodes
{
    
}

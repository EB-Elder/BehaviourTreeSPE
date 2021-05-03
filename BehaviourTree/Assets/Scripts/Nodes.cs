using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum states
{
    NotExecuted,
    Running,
    Failure,
    Success
}

public enum baseNodeType
{
    Condition,
    Action
}


public class Nodes
{
    //This is the actual state of the node
    
    private Func<states> _action;
    private baseNodeType _nodeType;
    
    public states state = states.NotExecuted;


    protected Nodes()
    {
        
    }
    public Nodes(Func<states> f, baseNodeType newNodeType)
    {
        _action = f;
        _nodeType = newNodeType;
    }
    
    public virtual states Execute()
    {
        state = _action();
        return state;
    }
    
}

public class Selector : Nodes
{
    
    //INCOMPLET
    private List<Nodes> nodeList = new List<Nodes>();

    public void AddNode(ref Nodes newNode)
    {
        nodeList.Add(newNode);
    }

    public void ClearList()
    {
        nodeList.Clear();
    }
    
    public override states Execute()
    {
        state = states.Failure;
        foreach (Nodes node in nodeList)
        {
            state = node.Execute();
            if (state == states.Success)
            {
                break;
            }
        }
        return state;
    }

}

public class Sequence : Nodes
{
    private List<Nodes> nodeList;
}

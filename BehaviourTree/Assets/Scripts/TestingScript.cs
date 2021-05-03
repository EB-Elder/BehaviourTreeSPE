using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    
    [SerializeField] private bool _motivated = false;
    [SerializeField] private bool _gotTime = false;
    
    private List<Nodes> behaviourTree = new List<Nodes>(8);
    
    private states OrderFood()
    {
        print("I order food");
        return states.Success;
    }

    private states CookFood()
    {
        print("I cook food");
        return states.Success;
    }

    private states isMotivated()
    {
        if (_motivated)
            return states.Success;
        return states.Failure;
    }

    private states haveTime()
    {
        if (_gotTime)
            return states.Success;
        return states.Failure;   
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Nodes motivationCondition = new Nodes(isMotivated, baseNodeType.Condition);
        Nodes timeCondition = new Nodes(haveTime, baseNodeType.Condition);
        Nodes cookAction = new Nodes(CookFood, baseNodeType.Action);
        
        Sequence cookingSequence = new Sequence();
        cookingSequence.AddNode(motivationCondition);
        cookingSequence.AddNode(timeCondition);
        cookingSequence.AddNode(cookAction);
        
        Nodes orderAction = new Nodes(OrderFood, baseNodeType.Action);
        
        Selector orderSelector = new Selector();
        
        orderSelector.AddNode(cookingSequence);
        orderSelector.AddNode(orderAction);
        
        behaviourTree.Add(orderSelector);

        
        states result = behaviourTree[0].Execute();
        
    }
}

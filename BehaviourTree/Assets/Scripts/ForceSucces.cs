using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSucces : Nodes
{
    public virtual states Execute()
    {
        state = states.Running;
        state = states.Success;
        return state;
    }

    //Methode virtuel de la réinitialisation lié au Noeud
    public virtual states Initialize()
    {
        state = states.NotExecuted;
        return state;
    }
}

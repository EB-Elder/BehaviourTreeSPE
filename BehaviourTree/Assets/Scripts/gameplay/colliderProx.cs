using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderProx : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<infiltration_player_controler>().LoosePV(40);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderDistance : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<infiltration_player_controler>().LoosePV(1);
        }
    }
}

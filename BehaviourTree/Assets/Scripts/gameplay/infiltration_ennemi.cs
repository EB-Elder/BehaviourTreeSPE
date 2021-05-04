using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiltration_ennemi : MonoBehaviour
{
    public int degat_arme = 25;
    public infiltration_player_controler joueur;
    public float maxDistance;
    public float maxAngle;
    public float speedRotate;
    public LineRenderer lineDrawer1;
    public LineRenderer lineDrawer2;


    [SerializeField] private float executeTime = 1.0f;
    [SerializeField] private float executeCounter = 0.0f;

    private bool canShoot = true;

    private readonly Sequence behaviourTree = new Sequence();

    private void Start()
    {
        // set the color of the line
        lineDrawer1.startColor = Color.red;
        lineDrawer1.endColor = Color.red;

        lineDrawer2.startColor = Color.red;
        lineDrawer2.endColor = Color.red;

        // set width of the renderer
        lineDrawer1.startWidth = 0.3f;
        lineDrawer1.endWidth = 0.3f;

        lineDrawer2.startWidth = 0.3f;
        lineDrawer2.endWidth = 0.3f;


        Nodes tirer = new Nodes(Shoot, baseNodeType.Action);
        Nodes rotate = new Nodes(RotateEnnemi, baseNodeType.Action);
        Nodes detection = new Nodes(isPlayerInVisionCone, baseNodeType.Condition);

        Sequence channelingFayah = new Sequence();
        channelingFayah.AddNode(detection);
        channelingFayah.AddNode(tirer);


        Selector ennemieSelector = new Selector();

        ennemieSelector.AddNode(channelingFayah);
        ennemieSelector.AddNode(rotate);

        behaviourTree.AddNode(ennemieSelector);


    }

    states Shoot()
    {
        if(canShoot)
        {
            Debug.Log("shoot");
            joueur.LoosePV(degat_arme);
            canShoot = false;
            StartCoroutine(reloading());

            
        }
        print("failshoot");
        return states.Success;
    }

    states isPlayerInVisionCone()
    {
        float distance = Vector3.Distance(transform.position, joueur.transform.position);
        float angle = Vector3.Angle(transform.forward, joueur.transform.position - transform.position);

        if (distance > maxDistance) return states.Failure;
        else
        {
            if (Mathf.Abs(angle) > Mathf.Abs(maxAngle)) return states.Failure;
            else return states.Success;
        }

    }

    private states RotateEnnemi()
    {
        transform.Rotate(new Vector3(0.0f, speedRotate * Time.deltaTime, 0.0f));

        Debug.Log("rotating");

        return states.Success;
    }

    public IEnumerator reloading()
    {
        yield return new WaitForSeconds(1.0f);
        canShoot = true;
    }

    public void DrawConeOfSight()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = transform.position + Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxDistance;
        Vector3 pos3 = transform.position + Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxDistance;

        lineDrawer1.SetPosition(0, pos1);
        lineDrawer1.SetPosition(1, pos2);
        lineDrawer2.SetPosition(0, pos1);
        lineDrawer2.SetPosition(1, pos3);
    }

    private void Update()
    {

        DrawConeOfSight();

        states result = behaviourTree.Execute();
        behaviourTree.Initialize();

        /*executeCounter += Time.deltaTime;
        if (executeCounter >= executeTime)
        {
            
            executeCounter = 0.0f;
        }*/

    }
}

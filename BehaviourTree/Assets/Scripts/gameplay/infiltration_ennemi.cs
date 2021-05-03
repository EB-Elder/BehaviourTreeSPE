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

    private bool canShoot = true;

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
    }

    void Shoot()
    {
        if(canShoot)
        {
            Debug.Log("shoot");
            joueur.LoosePV(degat_arme);
            canShoot = false;
            StartCoroutine(reloading());
        }
    }

    bool isPlayerInVisionCone()
    {
        float distance = Vector3.Distance(transform.position, joueur.transform.position);
        float angle = Vector3.Angle(transform.forward, joueur.transform.position - transform.position);

        if (distance > maxDistance) return false;
        else
        {
            if (Mathf.Abs(angle) > Mathf.Abs(maxAngle)) return false;
            else return true;
        }
    }

    private void RotateEnnemi()
    {
        transform.Rotate(new Vector3(0.0f, speedRotate * Time.deltaTime, 0.0f));
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
        RotateEnnemi();
        DrawConeOfSight();
    }
}

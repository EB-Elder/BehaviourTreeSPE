using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat_boss : MonoBehaviour
{
    public int pv = 200;
    public AudioSource bossplayer;
    public GameObject attaqueDistance;
    public GameObject attaqueProx;

    public Material attaqueDistMat;
    public Material attaqueProxMat;

    public BoxCollider colliderDistance;
    public List<SphereCollider> colliderProx;

    bool isAttacking = false;
    bool isdead = false;
    public bool invul = false;

    //subir des dégâts
    public void LoosePV(int dmg)
    {
        if(!invul)
        {
            pv -= dmg;
            bossplayer.Play();

            invul = true;

            StartCoroutine(invulTime());

            if (pv <= 0)
            {
                //dead
                isdead = true;
                gameObject.SetActive(false);
            }
        }
        
    }

    public IEnumerator invulTime()
    {
        yield return new WaitForSeconds(1.0f);
        invul = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") LoosePV(25);
    }

    public void AttaqueDistance()
    {
        StartCoroutine(PerformAttaqueDistance());
    }

    public void AttaqueProx()
    {
        StartCoroutine(PerformAttaqueProx());
    }

    private IEnumerator PerformAttaqueDistance()
    {
        isAttacking = true;
        attaqueDistance.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        colliderDistance.enabled = true;
        attaqueDistMat.color = new Color(attaqueDistMat.color.r, attaqueDistMat.color.g, attaqueDistMat.color.b, 1.0f);

        yield return new WaitForSeconds(0.5f);

        colliderDistance.enabled = false;
        attaqueDistMat.color = new Color(attaqueDistMat.color.r, attaqueDistMat.color.g, attaqueDistMat.color.b, 0.3f);

        attaqueDistance.SetActive(false);

        yield return new WaitForSeconds(2.0f);

        isAttacking = false;
    }

    private IEnumerator PerformAttaqueProx()
    {
        isAttacking = true;
        attaqueProx.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < colliderProx.Count; i++)
        {
            colliderProx[i].enabled = true;
        }

        attaqueProxMat.color = new Color(attaqueProxMat.color.r, attaqueProxMat.color.g, attaqueProxMat.color.b, 1.0f);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < colliderProx.Count; i++)
        {
            colliderProx[i].enabled = false;
        }
        attaqueProxMat.color = new Color(attaqueProxMat.color.r, attaqueProxMat.color.g, attaqueProxMat.color.b, 0.3f);

        attaqueProx.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        isAttacking = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isAttacking) AttaqueDistance();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(!isAttacking) AttaqueProx();
        }
    }
}

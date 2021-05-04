using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat_boss : MonoBehaviour
{
    public int pv = 200;
    public AudioSource bossplayer;
    public GameObject attaqueDistance;
    public GameObject attaqueProx;

    [SerializeField]
    private GameObject joueur;

    public Material attaqueDistMat;
    public Material attaqueProxMat;

    public BoxCollider colliderDistance;
    public List<SphereCollider> colliderProx;

    private readonly Sequence behaviourTree = new Sequence();

    public bool isAttacking = true;
    bool isdead = false;
    public bool invul = false;


    private void Start()
    {

        Selector globalBossSelector = new Selector();


        Nodes nodeTestCac = new Nodes(testCaC, baseNodeType.Condition);
        Nodes nodeTestCD = new Nodes(testCD, baseNodeType.Condition);
        Nodes nodeCloseAttack = new Nodes(AttaqueProx, baseNodeType.Action);
        Nodes nodeRangedAttack = new Nodes(AttaqueDistance, baseNodeType.Action);

        Sequence closeAttackSequence = new Sequence();
        closeAttackSequence.AddNode(nodeTestCac);
        closeAttackSequence.AddNode(nodeTestCD);
        closeAttackSequence.AddNode(nodeCloseAttack);


        Sequence rangedAttackSequence = new Sequence();
        rangedAttackSequence.AddNode(nodeTestCD);
        rangedAttackSequence.AddNode(nodeRangedAttack);

        globalBossSelector.AddNode(closeAttackSequence);
        globalBossSelector.AddNode(rangedAttackSequence);

        behaviourTree.AddNode(globalBossSelector);
    }

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

    public states AttaqueDistance()
    {
        StartCoroutine(PerformAttaqueDistance());

        return states.Success;
    }

    public states AttaqueProx()
    {
        
        StartCoroutine(PerformAttaqueProx());

        return states.Success;

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

    private states testCaC()
    {
        float distance = Vector3.Distance(transform.position, joueur.transform.position);

        if (distance < 10)
        {

            print("le boss est au corps à corps");
            return states.Success;

        }

        print("le boss est à distance");
        return states.Failure;
    }

    private states testCD()
    {
        if (!isAttacking)
        {
            print("une attaque peut être lancée par le boss");
            return states.Success;
        }

        print("le boss est en CD");
        return states.Failure;
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

        //transform.LookAt(joueur.transform.position);
        states result = behaviourTree.Execute();
        behaviourTree.Initialize();
    }
}

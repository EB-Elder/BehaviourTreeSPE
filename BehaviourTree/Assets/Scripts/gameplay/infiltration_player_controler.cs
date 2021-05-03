using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiltration_player_controler : MonoBehaviour
{
    private Transform playerTransform;
    private MeshRenderer meshrenderer;
    public AudioSource soundplayer;
    public int pv = 100;
    public float speed = 20.0f;
    public float speedRotate = 10.0f;
    public Material dead;

    bool upDown = false;
    bool LR = false;
    public bool isdead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.transform;
        meshrenderer = gameObject.GetComponent<MeshRenderer>();
    }

    //subir des dégâts
    public void LoosePV(int dmg)
    {
        pv -= dmg;
        soundplayer.Play();

        if(pv <= 0)
        {
            //dead
            meshrenderer.material = dead;
            isdead = true;
        }
    }

    // gérer les déplacements
    void Update()
    {
        if(pv > 0)
        {
            Vector3 move = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                move += playerTransform.forward * speed * Time.deltaTime;

                if (!upDown) upDown = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                move += -playerTransform.right * speed * Time.deltaTime;

                if (!LR) LR = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                move += playerTransform.right * speed * Time.deltaTime;

                if (!LR) LR = true;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                move += -playerTransform.forward * speed * Time.deltaTime;

                if (!upDown) upDown = true;
            }

            if (LR && upDown)
            {
                playerTransform.position = playerTransform.position += (move / 2.0f);
            }
            else
            {
                playerTransform.position = playerTransform.position += move;
            }

            LR = false;
            upDown = false;

            float axisX = Input.GetAxis("Mouse X");
            float axisY = Input.GetAxis("Mouse Y");

            playerTransform.Rotate(new Vector3(0.0f, speedRotate * axisX * Time.deltaTime, 0.0f));
        }       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Player : MonoBehaviourPunCallbacks
{
    public float speed = 2.0f;
    public Animator anim;
    public PhotonView pv;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            bool bmove = UpdateMove();
        }
    }

    bool UpdateMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool bPress = h > 0.01f || h < -0.01f || v > 0.01 || v < -0.01f;

        if (bPress)
        {
            Vector3 dir = h * Vector3.right + v * Vector3.forward;

            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }


        return bPress;
    }
    
}

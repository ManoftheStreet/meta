using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class Player : MonoBehaviourPunCallbacks
{
    public float speed = 4.0f;
    public Animator anim;
    public PhotonView pv;

    public Text textName;


    // Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine)
        {
            textName.text = PhotonNetwork.NickName;
        }
        else
        {
            textName.text = pv.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            bool bMove = UpdateMove();
            UpdateAnimation(bMove);
            UpdateAttack();

            Camera.main.GetComponent<FollowCamera>().p = transform;
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

        }
        


        return bPress;
    }

    void UpdateAnimation(bool bMove)
    {
        anim.SetBool("Walk", bMove);
    }

    void UpdateAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 pos = transform.position + new Vector3(0, 1.6f, 0) + transform.forward * 2.2f;

        GameObject o = PhotonNetwork.Instantiate("Bullet", pos, Quaternion.identity);

        o.GetComponent<PhotonView>().RPC("SetDirection", RpcTarget.All, transform.forward);
    }
    public void DestroyPlayer()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}

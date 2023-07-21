using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{
    public PhotonView pv;

    Vector3 vDirection;




    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vDirection * 5.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PhotonView>().IsMine && !pv.IsMine)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Player>().DestroyPlayer();
            }

            pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    void SetDirection(Vector3 d)
    {
        vDirection = d;
    }

    [PunRPC]
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}

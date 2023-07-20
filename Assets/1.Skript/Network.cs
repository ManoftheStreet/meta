using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
    public InputField inputName;

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();

        PhotonNetwork.LocalPlayer.NickName = inputName.text;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;

        PhotonNetwork.JoinOrCreateRoom("MyGameRoom", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        //Debug.Log("룸 조인 성");
        InitPlayer();
    }

    void InitPlayer()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

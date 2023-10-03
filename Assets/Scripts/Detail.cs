using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Detail : MonoBehaviour
{
    private PhotonView _photonView;

    public void Initialization()
    {
        _photonView = GetComponent<PhotonView>();
    }
}

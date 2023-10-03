using Photon.Pun;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    #region PUN CALLBACKS

    /*
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(Constants.Main);
    }
    */

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(Constants.Main);
    }
    

    #endregion
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(Constants.Lobby);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(Constants.Main);
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene(Constants.Shop);
    }
}
